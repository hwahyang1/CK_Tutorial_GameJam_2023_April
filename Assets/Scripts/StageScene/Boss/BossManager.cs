using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.StageScene.Audio;
using CK_Tutorial_GameJam_April.StageScene.Items;
using CK_Tutorial_GameJam_April.PreloadScene.Scene;
using CK_Tutorial_GameJam_April.PreloadScene.Audio;
using CK_Tutorial_GameJam_April.StageScene.Character;

namespace CK_Tutorial_GameJam_April.StageScene.Boss
{
	/// <summary>
	/// Description
	/// </summary>
	public class BossManager : MonoBehaviour
	{
		[Header("AudioClips")]
		[SerializeField]
		private AudioClip enterClip;
		[SerializeField]
		private AudioClip watchClip;
		[SerializeField]
		private AudioClip deathClip;
		
		[Header("Scripts")]
		[SerializeField]
		private SpriteRenderer playerIndicator;

		[SerializeField]
		private HideObjectManager hideObjectManager;

		[SerializeField]
		private CharacterAnim characterAnim;
		
		[SerializeField]
		private ItemSpawnManager itemSpawnManager;
		
		[SerializeField]
		private FmodFoundAdjuster fmodFoundAdjuster;
		
		[Header("Settings")]
		[SerializeField]
		private float down = 0f;
		
		[SerializeField]
		private float maxTime = 500f;
		
		[SerializeField]
		private Sprite[] indicator = new Sprite[2];

		[SerializeField]
		private Sprite[] bossAnim = new Sprite[4];

		private SpriteRenderer spriteRenderer;
		private Camera mainCamera;

		private float elapsedTime;
		public float ElapsedTime => elapsedTime;
		private float animTime;
		private float bossTime;

		private bool isTrigged = false;
		private bool onBoss = false;
		private bool deadTriggered = false;

		private void Awake()
		{
			animTime = 0f;
			mainCamera = Camera.main;
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Start()
		{
			DefineSaveData data = GameSaveData.Instance.SaveData;
			if (data == null) return;

			elapsedTime = data.bossTime - 5f;
		}

		private void Update()
		{
			playerIndicator.sprite = null; // 평소에는 아무것도 띄우지 않음.

			if (elapsedTime >= maxTime + 0.5f && animTime <= 5f) // ?
			{
				playerIndicator.sprite = indicator[0];
			}

			if (elapsedTime >= maxTime)
			{
				if (!isTrigged)
				{
					AudioManager.Instance.PlayEffectAudio(enterClip);
					StartCoroutine(Up());
					isTrigged = true;
				}
			}

			if (elapsedTime > maxTime) // 보스 출현
			{
				onBoss = true;
			}

			if (onBoss == true) // 보스 애니메이션
			{
				animTime += Time.deltaTime;

				if (animTime <= 5f)
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

			if (onBoss && !hideObjectManager.isTrigger && animTime > 5f) // 숨어있지 않으면 죽음
			{
				bossTime += Time.deltaTime; // 보스에게 걸렸을떄의 타이머
				spriteRenderer.sprite = bossAnim[3]; // 발견으로 이미지 변경
				characterAnim.onSuprized = true;
				
				if (bossTime <= 4)
				{
					if (!deadTriggered)
					{
						AudioManager.Instance.PlayEffectAudio(watchClip);
						StartCoroutine(ZoomIn());
						deadTriggered = true;
					}
					GameManager.Instance.status = GameStatus.Dead;
					playerIndicator.sprite = indicator[1]; //!를 띄웁니다.
				}
				else
				{
					//spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f); // bossTime이 4초 이상이라면 스케일을 다시 줄여줍니다.
					//playerIndicator.sprite = null;
				}
			}
			else if (onBoss && animTime > 5f) // 숨어있었다면 보스가 퇴장
			{
				StartCoroutine(Down());
				GameSaveData.Instance.ExportData();
				itemSpawnManager.RespawnItem();
				onBoss = false;
				isTrigged = false;
				elapsedTime = 0f;
				animTime = 0f;
			}

			if (GameManager.Instance.status != GameStatus.Playing && GameManager.Instance.status != GameStatus.Eating)
			{
				return;
			}

			elapsedTime += Time.deltaTime;
			spriteRenderer.transform.position =
				new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - down, 0f);
		}

		public void GetItem()
		{
			elapsedTime += 30f;
		}

		IEnumerator Up()
		{
			for (float p = 20f; p >= 0f; p--)
			{
				down = p;
				fmodFoundAdjuster.ChangeValue(-(p - 20f) / 20f);
				yield return null;
			}
		}

		IEnumerator Down()
		{
			for (float p = 0f; p <= 60f; p++)
			{
				down = p/3;
				fmodFoundAdjuster.ChangeValue(1f - (p / 60f));
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
			AudioManager.Instance.PlayEffectAudio(deathClip);
			GameSaveData.Instance.Exit();
			SceneChange.Instance.ChangeScene("BlankScene", true, false, () =>
			                                                            {
				                                                            SceneChange.Instance.ChangeScene("StageScene", false, true);
			                                                            });
		}
	}
}
