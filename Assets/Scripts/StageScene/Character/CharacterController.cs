using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Cysharp.Threading.Tasks;

using CK_Tutorial_GameJam_April.PreloadScene.Item;
using CK_Tutorial_GameJam_April.PreloadScene.Alert;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Item;

namespace CK_Tutorial_GameJam_April.StageScene.Character
{
	/// <summary>
	/// 플레이어를 제어합니다.
	/// </summary>
	public class CharacterController : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		
		[SerializeField]
		private ItemManager itemManager;

		[Header("Eat")]
		[SerializeField]
		private float eatTime = 0.5f;

		[Header("Walk")]
		[SerializeField]
		private float speed;

		[SerializeField]
		private float maxSpeed;

		[Header("Jump")]
		[SerializeField]
		private float jumpSpeed;

		[Header("Run")]
		[SerializeField]
		private float runStamina;

		[SerializeField]
		private float runSpeedMultiply;

		[SerializeField]
		private float runMaxSpeed;

		private bool isJumpable = true;

		public bool IsJumpable
		{
			get => isJumpable;
			set => isJumpable = value;
		}

		private Rigidbody2D rb;
		private Animator animator;
		private LevelManager levelManager;
		private CharacterAnim characterAnim;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			levelManager = GetComponent<LevelManager>();
			characterAnim = GetComponent<CharacterAnim>();
		}

		private void Update()
		{
			if (GameManager.Instance.status != GameStatus.Playing) return;

			if (itemManager.CurrentItemCode != 0)
			{
				DefineItem item = ItemStorage.Instance.GetItems()[itemManager.CurrentItemCode];

				// 아이템 버리기
				if (Input.GetKeyDown(KeyCode.Z) && item.dropable)
				{
					AlertManager.Instance.Show(AlertType.Double, "확인", "정말로 아이템을 버릴까요?",
					                           new Dictionary<string, Action>()
					                           { { "예", () => itemManager.SetCurrentItem(0) }, { "아니요", null } });
				}

				// 아이템 먹기
				if (Input.GetKeyDown(KeyCode.F))
				{
					ToggleEat().Forget();
					levelManager.Stamina += item.stamina;
					levelManager.Exp += item.exp;
					itemManager.SetCurrentItem(0);
				}
			}
		}

		public async UniTaskVoid ToggleEat()
		{
			characterAnim.onEat = true;
			GameManager.Instance.status = GameStatus.Paused;
			await UniTask.Delay(TimeSpan.FromSeconds(eatTime));
			GameManager.Instance.status = GameStatus.Playing;
			characterAnim.onEat = false;
		}

		private void FixedUpdate()
		{
			animator.SetBool("Idle", true);
			characterAnim.onWalk = false;

			if (GameManager.Instance.status != GameStatus.Playing) return;

			// 이동 (걷기 + 점프 + 달리기)
			levelManager.isPlayerStay = isJumpable;

			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				characterAnim.onWalk = true;
			}

			if (Input.GetKey(KeyCode.LeftShift) && levelManager.Stamina > 0)
			{
				if (Input.GetKey(KeyCode.A) && rb.velocity.x > runMaxSpeed * (-1))
				{
					rb.AddForce(new Vector2(speed * runSpeedMultiply * (-1) * Time.deltaTime, 0f));
					spriteRenderer.flipX = true;
					levelManager.Stamina -= runStamina;
					levelManager.isPlayerStay = false;
				}

				if (Input.GetKey(KeyCode.D) && rb.velocity.x < runMaxSpeed)
				{
					rb.AddForce(new Vector2(speed * runSpeedMultiply * Time.deltaTime, 0f));
					spriteRenderer.flipX = false;
					levelManager.Stamina -= runStamina;
					levelManager.isPlayerStay = false;
				}
			}
			else
			{
				if (Input.GetKey(KeyCode.A) && rb.velocity.x > maxSpeed * (-1))
				{
					rb.AddForce(new Vector2(speed * (-1) * Time.deltaTime, 0f));
					spriteRenderer.flipX = true;
					levelManager.isPlayerStay = false;
				}

				if (Input.GetKey(KeyCode.D) && rb.velocity.x < maxSpeed)
				{
					rb.AddForce(new Vector2(speed * Time.deltaTime, 0f));
					spriteRenderer.flipX = false;
					levelManager.isPlayerStay = false;
				}
			}

			if (Input.GetKey(KeyCode.Space) && isJumpable)
			{
				rb.AddForce(new Vector2(0f, jumpSpeed));
				isJumpable = false;
			}

			animator.SetBool("Idle", levelManager.isPlayerStay);
		}

		/*private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				isJumpable = true;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				isJumpable = false;
			}
		}*/
	}
}
