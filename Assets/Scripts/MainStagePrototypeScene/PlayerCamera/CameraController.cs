using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Slots;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene.PlayerCamera
{
	/// <summary>
	/// 카메라의 이동을 관리합니다.
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		private Transform target;

		[SerializeField]
		private float lerpSpeed = 2.5f;

		[SerializeField]
		private Vector3 inventoryOffset;
		
		private Vector3 offset;
		private Vector3 targetPos;

		[SerializeField]
		private SlotsManager slotsManager;

		private void Start()
		{
			target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
			offset = transform.position - target.position;
		}

		private void Update()
		{
			targetPos = target.position + offset + (slotsManager.IsActive ? inventoryOffset : Vector3.zero);
			transform.position = targetPos;
		}
	}
}
