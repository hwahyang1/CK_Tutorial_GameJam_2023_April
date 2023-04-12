using System.Collections;
using System.Collections.Generic;
using TimeSpan = System.TimeSpan;

using UnityEngine;

using Cysharp.Threading.Tasks;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 일정 시간 이후 이 스크립트가 붙은 GameObject를 Destroy 합니다.
	/// </summary>
	public class DestroyThis : MonoBehaviour
	{
		[SerializeField]
		private float timeoutSecond;

		private void Start()
		{
			Timeout().Forget();
		}

		private async UniTaskVoid Timeout()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(timeoutSecond));
			Destroy(gameObject);
		}
	}
}
