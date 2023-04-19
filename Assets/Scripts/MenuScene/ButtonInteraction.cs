using System.Collections;
using System.Collections.Generic;
using CK_Tutorial_GameJam_April.CreditScene;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.MenuScene
{
	/// <summary>
	/// 버튼의 상호작용을 처리합니다.
	/// </summary>
	public class ButtonInteraction : MonoBehaviour
	{
		public void OnLoadGameButtonClicked()
		{
			
		}

		public void OnNewGameButtonClicked()
		{
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
