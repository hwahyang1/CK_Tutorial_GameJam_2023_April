using System.Collections;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

using UnityEngine;

using Cysharp.Threading.Tasks;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.PreloadScene
{
	/// <summary>
	/// Scene의 전반적인 실행을 관리합니다.
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> canvases = new List<GameObject>();

		[SerializeField]
		private SceneChange sceneChange;

		private void Start()
		{
			foreach (GameObject obj in canvases)
			{
				DontDestroyOnLoad(obj);
			}

			DelayedStart().Forget();
		}

		private async UniTaskVoid DelayedStart()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
			sceneChange.ChangeScene("OpeningScene", false, false);
		}
	}
}
