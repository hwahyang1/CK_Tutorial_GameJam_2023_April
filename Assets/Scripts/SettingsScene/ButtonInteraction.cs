using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.SettingsScene
{
	/// <summary>
	/// 버튼의 상호작용을 처리합니다.
	/// </summary>
	public class ButtonInteraction : MonoBehaviour
	{
		public void OnBackButtonClicked()
		{
			SceneChange.Instance.Unload("SettingsScene");
		}
	}
}
