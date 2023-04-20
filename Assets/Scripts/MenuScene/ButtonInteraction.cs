using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using CK_Tutorial_GameJam_April.CreditScene;
using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.PreloadScene.Save;
using CK_Tutorial_GameJam_April.PreloadScene.Scene;
using CK_Tutorial_GameJam_April.PreloadScene.Alert;

namespace CK_Tutorial_GameJam_April.MenuScene
{
	/// <summary>
	/// 버튼의 상호작용을 처리합니다.
	/// </summary>
	public class ButtonInteraction : MonoBehaviour
	{
		private DefineSaveData saveData;
		
		private void Start()
		{
			saveData = SaveManager.Instance.GetSaveData();
		}
		
		public void OnLoadGameButtonClicked()
		{
			if (saveData == null)
			{
				AlertManager.Instance.Show(AlertType.Single, "알림", "저장된 데이터가 없습니다.", new Dictionary<string, Action>(){{"확인", null}});
				return;
			}

			SceneChange.Instance.ChangeScene("StageScene");
		}

		public void OnNewGameButtonClicked()
		{
			if (saveData != null)
			{
				AlertManager.Instance.Show(AlertType.Double, "알림", "새로 게임을 진행 할 경우, 기존 데이터가 삭제됩니다.\n계속하시겠습니까?", new Dictionary<string, Action>(){{"예",
					                           () =>
					                           {
						                           GameSaveData.Instance.ResetData();
						                           SceneChange.Instance.ChangeScene("StageScene");
					                           }}, {"아니요", null}});
				return;
			}
			
			GameSaveData.Instance.ResetData();
			SceneChange.Instance.ChangeScene("StageScene");
		}

		public void OnSettingsButtonClicked()
		{
			CreditParams.Instance.isControllable = true;
			SceneChange.Instance.Add("SettingsScene");
		}

		public void OnCreditButtonClicked()
		{
			SceneChange.Instance.Add("CreditScene");
		}

		public void OnExitButtonClicked()
		{
			#if UNITY_EDITOR
			EditorApplication.ExecuteMenuItem("Edit/Play");
			#else
				Application.Quit();
			#endif
		}
	}
}
