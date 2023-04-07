using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Slots;
using UnityEditor;
using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// Description
	/// </summary>
	public class CharacterManager : MonoBehaviour
	{
		private SlotsManager slotsManager;
		[SerializeField]
		private float speed;
		[SerializeField]
		private float maxSpeed;
		[SerializeField]
		private float jumpSpeed;

		private float time = 0f; 
		
		private Rigidbody2D rd;

		private Vector2 movement;

		private bool isJumpable;

		private bool isTrigger;

		private void Awake()
		{
			rd = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.A)&&rd.velocity.x>maxSpeed * (-1))
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

			if (Input.GetMouseButton(0))
			{
				time += Time.deltaTime;
			}
			
			if (Input.GetMouseButtonUp(0))
			{
				time = 0f;
			}

			if (time >= 2f && isTrigger)
			{
				time = 0f;
				Debug.Log("eat");
				slotsManager.SetTabActive(true);
			}
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.tag == "Ground")
			{
				isJumpable = true;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.tag == "Ground")
			{
				isJumpable = false;
			}	
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Item")
			{
				isTrigger = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.tag == "Item")
			{
				isTrigger = true;
			}
		}
	}
	
}
