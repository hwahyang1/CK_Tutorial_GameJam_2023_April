using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene.Character
{
	/// <summary>
	/// 캐릭터의 이동을 관리합니다.
	/// </summary>
	public class CharacterController : MonoBehaviour
	{
		[Header("Speed")]
		[SerializeField]
		private float speed;

		[SerializeField]
		private float maxSpeed;

		[SerializeField]
		private float jumpSpeed;

		[Header("Status")]
		[SerializeField, ReadOnly]
		private bool isJumpable;

		private Rigidbody2D rb;
		
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.A) && rb.velocity.x > -maxSpeed)
			{
				rb.AddForce(new Vector2(-speed * Time.deltaTime, 0f));
			}

			if (Input.GetKey(KeyCode.D) && rb.velocity.x < maxSpeed)
			{
				rb.AddForce(new Vector2(speed * Time.deltaTime, 0f));
			}
			
			if (Input.GetKey(KeyCode.Space) && isJumpable)
			{
				rb.AddForce(new Vector2(0f, jumpSpeed));
				isJumpable = false;
			}
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag("Ground"))
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
		}
	}
}
