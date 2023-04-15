using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene.PlayerCamera
{
	/// <summary>
	/// 카메라의 이동을 관리합니다.
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		[Header("Deadline")]
		[SerializeField]
		private Vector2 deadlineMin;
		[SerializeField]
		private Vector2 deadlineMax;
		
		[Header("Offset")]
		[SerializeField]
		private Vector3 inventoryOffset;
		[SerializeField]
		private Vector3 offset;
		
		[Header("Slots")]
		[SerializeField]
		private SlotsManager leftSlotsManager;
		[SerializeField]
		private SlotsManager rightSlotsManager;

		private Transform target;
		private Vector3 targetPos;
		
		private void Start()
		{
			target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}

		private void Update()
		{
			targetPos = target.position + offset + (leftSlotsManager.IsActive ? -inventoryOffset : Vector3.zero) + (rightSlotsManager.IsActive ? inventoryOffset : Vector3.zero);

			if (targetPos.x < deadlineMin.x) targetPos.x = deadlineMin.x;
			if (targetPos.y < deadlineMin.y) targetPos.y = deadlineMin.y;
			if (targetPos.x > deadlineMax.x) targetPos.x = deadlineMax.x;
			if (targetPos.y > deadlineMax.y) targetPos.y = deadlineMax.y;
			
			transform.position = targetPos;
		}
	}
}
