using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.StageScene.Character
{
	/// <summary>
	/// Description
	/// </summary>
	public class JumpTrigger : MonoBehaviour
	{
		private CharacterController characterController;

		private void Start()
		{
			characterController = transform.parent.GetComponent<CharacterController>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				characterController.IsJumpable = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				characterController.IsJumpable = false;
			}
		}
	}
}
