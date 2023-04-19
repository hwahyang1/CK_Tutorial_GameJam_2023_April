using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.StageScene.Boss
{
	/// <summary>
	/// Description
	/// </summary>
	public class BossManager : MonoBehaviour
	{
		private float time;
		private float maxTime;
		private SpriteRenderer spriteRenderer;
		private Camera mainCamera;
		
		private void Start()
		{
			maxTime = 300f;
			mainCamera = Camera.main;
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			time += Time.deltaTime;
			spriteRenderer.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0f);

		}

		public void GetItem()
		{
			time += 20;
		}
	}
}
