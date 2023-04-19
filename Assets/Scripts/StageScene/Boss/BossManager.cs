using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CK_Tutorial_GameJam_April.StageScene.Character;

namespace CK_Tutorial_GameJam_April.StageScene.Boss
{
	/// <summary>
	/// Description
	/// </summary>
	public class BossManager : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer playerIndicator;

		[SerializeField]
		private HideObjectManager hideObjectManager;

		[SerializeField]
		private CharacterAnim characterAnim;
		
		[SerializeField]
		private float down = 0f;
		
		[SerializeField]
		private Sprite[] indicator = new Sprite[2];

		[SerializeField]
		private Sprite[] bossAnim = new Sprite[4];

		private SpriteRenderer spriteRenderer;
		private Camera mainCamera;

		private float time;
		private float maxTime;
		private float animTime;
		private float bossTime;

		private bool isTrigged = false;
		private bool onBoss = false;


		private void Start()
		{
			animTime = 0f;
			maxTime = 10f;
			mainCamera = Camera.main;
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			playerIndicator.sprite = null; // 평소에는 아무것도 띄우지 않음.

			if (time >= maxTime && animTime <= 2) // ?
			{
				playerIndicator.sprite = indicator[0];
			}

			if (time >= maxTime)
			{
				if (!isTrigged)
				{
					StartCoroutine(Up());
					isTrigged = true;
				}
			}

			if (time > maxTime) // 보스 출현
			{
				onBoss = true;
			}

			if (onBoss == true) // 보스 애니메이션
			{
				animTime += Time.deltaTime;

				if (animTime <= 12f)
				{
					if (animTime % 5 > 0f && animTime % 5 <= 2.0f)
					{
						spriteRenderer.sprite = bossAnim[0];
					}
					else if (animTime % 5 > 2.0f && animTime % 5 <= 2.5f)
					{
						spriteRenderer.sprite = bossAnim[1];
					}
					else if (animTime % 5 > 2.5f && animTime % 5 <= 4.5f)
					{
						spriteRenderer.sprite = bossAnim[2];
					}
					else if (animTime % 5 > 4.5f && animTime % 5 <= 5f)
					{
						spriteRenderer.sprite = bossAnim[1];
					}
				}
			}

			if (onBoss && !hideObjectManager.isTrigger && animTime > 12f) // 숨어있지 않으면 죽음
			{
				bossTime += Time.deltaTime; // 보스에게 걸렸을떄의 타이머
				spriteRenderer.sprite = bossAnim[3]; // 발견으로 이미지 변경
				characterAnim.onSuprized = true;
				
				if (bossTime <= 4)
				{
					StartCoroutine(ZoomIn());
					playerIndicator.sprite = indicator[1]; //!를 띄웁니다.
				}
				else
				{
					spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f); // bossTime이 4초 이상이라면 스케일을 다시 줄여줍니다.
					playerIndicator.sprite = null;
				}
			}
			else if (onBoss && animTime > 12f) // 숨어있었다면 보스가 퇴장
			{
				Debug.Log("good");
				StartCoroutine(Down());
				onBoss = false;
				isTrigged = false;
				time = 0f;
				animTime = 0f;
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
			for (float p = 20f; p > 0f; p--)
			{
				down = p;
				yield return null;
			}
		}

		IEnumerator Down()
		{
			for (float p = 0f; p > 80f; p++)
			{
				down = p / 4;
				yield return null;
			}
		}

		IEnumerator ZoomIn()
		{
			// spriteRenderer.transform.localScale = new Vector3(1.6f, 1.6f, 1f); // 4초간 보스의 스케일을 키워줍니다.
			float zoomTime = 0f;
			while (zoomTime <= 4f)
			{
				zoomTime += Time.deltaTime;
				spriteRenderer.transform.localScale =
					Vector3.Lerp(spriteRenderer.transform.localScale, new Vector3(1.6f, 1.6f, 1f), 0.05f);
				yield return null;
			}

		}
	}
}
