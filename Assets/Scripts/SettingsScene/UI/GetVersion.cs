using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CK_Tutorial_GameJam_April.SettingsScene.UI
{
	/// <summary>
	/// 현재 게임 버전을 표기합니다.
	/// </summary>
	public class GetVersion : MonoBehaviour
	{
		private void Awake()
		{
			GetComponent<Text>().text = "Ver. " + Application.version;
			Destroy(this);
		}
	}
}
