using System;
using System.Collections;
using System.Collections.Generic;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Item;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;
using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 캐릭터를 관리하는 스크립트
	/// </summary>
	public class CharacterManager : MonoBehaviour
	{
		[SerializeField]
		private SlotsManager slotsManager;
		
		[SerializeField]
		private ItemManager itemManager;
		
		[SerializeField]
		private float speed;
		
		[SerializeField]
		private float maxSpeed;
		
		[SerializeField]
		private float jumpSpeed;

		private float time = 0f; 

		private Rigidbody2D rd;

		private Vector2 movement;

		private ItemCode itemCode;

		private int stamina;

		private int maxStamina;
		
		private int level;

		private bool isJumpable = true;

		private bool isTrigger = false;

		private void Awake()
		{
			rd = GetComponent<Rigidbody2D>();
			stamina = 0;
			level = 1;
		}
		

		private void Update()
		{
			
			if (Input.GetKey(KeyCode.A) && rd.velocity.x>maxSpeed * (-1))
			{
				rd.AddForce(new Vector2(speed * (-1) * Time.deltaTime, 0f));
			}
			if (Input.GetKey(KeyCode.D)&&rd.velocity.x<maxSpeed)
			{
				rd.AddForce(new Vector2(speed * Time.deltaTime, 0f));
			}

			if (Input.GetKey(KeyCode.Space)&&isJumpable)
			{
				rd.AddForce(new Vector2(0f, jumpSpeed * Time.deltaTime));
			}

			if (Input.GetMouseButton(0)) // 2초간 클릭하면 아이템이 먹어지므로 time에 deltatime을 더해서 구함
			{
				time += Time.deltaTime;
				
			}

			if (Input.GetMouseButtonUp(0)) // 마우스 클릭을 그만하면 time을 0으로 초기화
			{
				time = 0f;
			}

			if (time >= 2f && isTrigger) // 만약 isTrigger상태이고, 2초이상 클릭했다면 아이템을 획득
			{
				time = 0f;
				slotsManager.SetTabActive(true);
				itemManager.SetCurrentItem(itemCode.itemID);
				Destroy(itemCode.gameObject);
				stamina += 2;
			}

			if (stamina >= maxStamina)
			{
				Debug.Log("진화");
				level += 1;
			}
		}
		
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "Main Tilemap")
			{
				isJumpable = true;
				Debug.Log("can jump");
			}
		}
		
		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "Main Tilemap")
			{
				isJumpable = false;
				Debug.Log("can't jump");
			}	
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Item")
			{
				isTrigger = true;
				itemCode = other.GetComponent<ItemCode>();
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.tag == "Item")
			{
				isTrigger = false;
			}
		}

	}
}
