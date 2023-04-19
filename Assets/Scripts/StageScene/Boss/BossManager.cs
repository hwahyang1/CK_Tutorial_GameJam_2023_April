using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CK_Tutorial_GameJam_April.StageScene.Boss
{
	/// <summary>
	/// Description
	/// </summary>
	public class BossManager : MonoBehaviour
	{
		[SerializeField]
		private float down = 0f;

		[SerializeField]
		private SpriteRenderer playerIndicator;

		[SerializeField]
		private Sprite[] indicator = new Sprite[2];

		[SerializeField]
		private Sprite[] bossAnim = new Sprite[3];

		private SpriteRenderer spriteRenderer;
		private Camera mainCamera;

		private float time;
		private float maxTime;
		private float animTime;

		private bool isTrigged = false;
		private bool onBoss = false;


		private void Start()
		{
			animTime = 0f;
			maxTime = 100f;
			mainCamera = Camera.main;
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			playerIndicator.sprite = null;

			if (time >= maxTime - 20) // ?
			{
				playerIndicator.sprite = indicator[0];
			}

			if (time >= maxTime) // !
			{
				playerIndicator.sprite = indicator[1];
				if (!isTrigged)
				{
					StartCoroutine(Up());
					isTrigged = true;
				}
			}

			if (time > maxTime)
			{
				onBoss = true;
			}

			if (onBoss == true)
			{
				animTime += Time.deltaTime;

				if (animTime <= 20)
				{
					if (animTime % 5 > 0f && animTime % 5 <= 0.5f)
					{
						spriteRenderer.sprite = bossAnim[0];
					}
					else if (animTime % 5 > 0.5f && animTime % 5 <= 2.5f)
					{
						spriteRenderer.sprite = bossAnim[1];
					}
					else if (animTime % 5 > 2.5f && animTime % 5 <= 3f)
					{
						spriteRenderer.sprite = bossAnim[2];
					}
					else if (animTime % 5 > 3f && animTime % 5 <= 5f)
					{
						spriteRenderer.sprite = bossAnim[1];
					}
				}
			}


			if (GameManager.Instance.status != GameStatus.Playing)
			{
				return;
			}

			time += Time.deltaTime;
			spriteRenderer.transform.position =
				new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - down, 0f);
		}

		public void GetItem()
		{
			time += 20f;
		}

		IEnumerator Up()
		{
			for (float p = 20f; p > 0; p--)
			{
				down = p;
				yield return null;
			}
		}
	}
}
