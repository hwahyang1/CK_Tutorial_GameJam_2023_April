using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using CK_Tutorial_GameJam_April.StageScene.UI;
using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.PreloadScene.Audio;
using CK_Tutorial_GameJam_April.PreloadScene.Alert;

namespace CK_Tutorial_GameJam_April.StageScene
{
	/// <summary>
	/// 게임의 진행 상태를 정의합니다.
	/// </summary>
	public enum GameStatus
	{
		Playing,
		Eating,
		ItemEarn,
		MessageViewing,
		Paused,
		ExitSaving,
		Dead
	}

	/// <summary>
	/// 게임의 전반적인 실행을 관리합니다.
	/// </summary>
	public class GameManager : SingleTon<GameManager>
	{
		[SerializeField]
		private Sprite sprite;
		
		private List<string> messages;
	
		public GameStatus status;

		public bool viewedTutorial = false;

		private bool toggleWhenAlertDisabled = false;

		private bool toggleFirstSave = false;
		
		private MessageManager messageManager;

		protected override void Awake()
		{
			base.Awake();
			
			toggleFirstSave = false;

			destroyCondition = () => SceneManager.GetActiveScene().name != "StageScene";
			
			messages = new List<string>()
			           {
							"불상한 햄스터…",
							"꼬마의 여섯번째 희생양이 되겠구나.",
							"너 이전에 다섯 마리의 햄스터가 이곳을 탈출하려 했지만…",
							"탈출구의 문은 굳게 닫혀있었지…",
							"작은 햄스터여! 탈출구에는 총 세개의 열쇠가 필요하다네!",
							"유형 햄스터들의 배고픔을 달래주고, 열쇠 세개를 모아 이곳을 탈출하게!",
							"그러지 못한다면 자네는 굶어 죽던가, 잡혀 죽던가 해야한다네!",
							"설명은 이쯤이면 됐네.",
							"자 어서 출발하게!"
			           };
		}

		private void Start()
		{
			messageManager = GameObject.FindAnyObjectByType<MessageManager>();
			
			DefineSaveData data = GameSaveData.Instance.SaveData;
			if (data == null) return;

			viewedTutorial = data.viewedTutorial;
		}

		protected override void Update()
		{
			base.Update();

			if (!viewedTutorial && status == GameStatus.Playing)
			{
				AudioManager.Instance.StopBackgroundAudio();
				messageManager.Show("???", messages, sprite, () =>
				                                        {
					                                        status = GameStatus.Playing;
					                                        viewedTutorial = true;
				                                        });
			}

			if (viewedTutorial && !toggleFirstSave)
			{
				GameSaveData.Instance.ExportData();
				toggleFirstSave = true;
			}

			if (AlertManager.Instance.IsRunning)
			{
				if (status == GameStatus.Playing)
				{
					status = GameStatus.Paused;
					toggleWhenAlertDisabled = true;
				}
			}
			else
			{
				if (toggleWhenAlertDisabled)
				{
					status = GameStatus.Playing;
					toggleWhenAlertDisabled = false;
				}
			}
		}
	}
}
