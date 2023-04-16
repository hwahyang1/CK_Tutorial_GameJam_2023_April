using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;
using CK_Tutorial_GameJam_April.PreloadScene.Settings;

namespace CK_Tutorial_GameJam_April.OpeningScene
{
	/// <summary>
	/// 게임의 전반적인 실행을 관리합니다.
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private float time;
		
		private bool protectInput = true;

		private Coroutine active = null;

		private bool isFirst;
		
		private void Start()
		{
			isFirst = SettingsManager.Instance.GetSettings().isFirst;
			StartCoroutine(ToggleProtectCoroutine());
			active = StartCoroutine(WaitForAnimatorCoroutine());
		}

		private void Update()
		{
			if (!isFirst && !protectInput && Input.anyKeyDown)
			{
				protectInput = true;
				GotoMenu();
			}
		}

		private IEnumerator ToggleProtectCoroutine()
		{
			yield return new WaitForSeconds(0.5f);
			protectInput = false;
		}

		private IEnumerator WaitForAnimatorCoroutine()
		{
			yield return new WaitForSeconds(time);
			if (isFirst)
			{
				DefineSettings oldData = SettingsManager.Instance.GetSettings();
				oldData.isFirst = false;
				SettingsManager.Instance.SetSettings(oldData);
				SettingsManager.Instance.SaveSettings();
			}
			active = null;
			GotoMenu();
		}

		private void GotoMenu()
		{
			if (active != null) StopCoroutine(active);
			SceneChange.Instance.ChangeScene("MenuScene",false, false);
		}
	}
}
