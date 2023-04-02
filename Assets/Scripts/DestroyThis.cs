using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 일정 시간 이후 이 스크립트가 붙은 GameObject를 Destroy 합니다.
	/// </summary>
	public class DestroyThis : MonoBehaviour
	{
		[SerializeField]
		private float timeout;

		private void Start()
		{
			StartCoroutine(nameof(TimeoutCoroutine));
		}

		private IEnumerator TimeoutCoroutine()
		{
			yield return new WaitForSeconds(timeout);
			Destroy(gameObject);
		}
	}
}
