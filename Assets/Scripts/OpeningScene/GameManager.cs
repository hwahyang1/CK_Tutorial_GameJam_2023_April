using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.OpeningScene
{
	/// <summary>
	/// 게임의 전반적인 실행을 관리합니다.
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private float time;
		
		private bool protectInput = false;

		private Coroutine active = null;
		
		private void Start()
		{
			active = StartCoroutine(WaitForAnimatorCoroutine());
		}

		private void Update()
		{
			if (!protectInput && Input.anyKeyDown)
			{
				protectInput = true;
				GotoMenu();
			}
		}

		private IEnumerator WaitForAnimatorCoroutine()
		{
			yield return new WaitForSeconds(time);
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
