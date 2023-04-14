using System.Collections;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

using UnityEngine;

using Cysharp.Threading.Tasks;

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

		private void Start()
		{
			WaitForAnimator().Forget();
		}

		private void Update()
		{
			if (!protectInput && Input.anyKeyDown)
			{
				protectInput = true;
				GotoMenu();
			}
		}

		private async UniTaskVoid WaitForAnimator()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(time));
			GotoMenu();
		}

		private void GotoMenu()
		{
			SceneChange.Instance.ChangeScene("MenuScene",false, false);
		}
	}
}
