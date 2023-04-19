using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// Description
	/// </summary>
	public class BossManager : MonoBehaviour
	{
		private float time;
		private float maxTime;
		private SpriteRenderer spriteRenderer;
		private void Start()
		{
			maxTime = 300f;
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			time += Time.deltaTime;
			spriteRenderer.transform.position = Camera.main.transform.position;

		}

		public void GetItem()
		{
			time += 20;
		}
	}
}
