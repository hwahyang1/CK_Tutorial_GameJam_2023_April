using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using CK_Tutorial_GameJam_April.StageScene.Save;
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
		public GameStatus status;

		private bool toggleWhenAlertDisabled = false;

		private bool toggleFirstSave = false;

		protected override void Awake()
		{
			base.Awake();
			
			toggleFirstSave = false;

			destroyCondition = () => SceneManager.GetActiveScene().name != "StageScene";
		}

		protected override void Update()
		{
			base.Update();

			if (!toggleFirstSave)
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
