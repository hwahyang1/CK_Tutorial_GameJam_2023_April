using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Item;
using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Slots;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene.Character
{
	/// <summary>
	/// 캐릭터의 상호작용을 관리합니다.
	/// </summary>
	public class CharacterInteraction : MonoBehaviour
	{
		[SerializeField]
		private SlotsManager slotsManager;
		[SerializeField]
		private ItemManager itemManager;
		
		[Header("Status")]
		[SerializeField, ReadOnly]
		private float time = 0f;
		[SerializeField, ReadOnly]
		private bool isTrigger;

		private Item target;
		private bool rollbackInventory;

		private void Update()
		{
			if (itemManager.CurrentItemCode != 0) return;

			if (rollbackInventory)
			{
				slotsManager.SetTabActive(false);
				rollbackInventory = false;
			}

			if (Input.GetMouseButton(0) && isTrigger)
			{
				target?.SetSliderActive(true);
				time += Time.deltaTime;
			}

			if (Input.GetMouseButtonUp(0))
			{
				target?.SetSliderActive(false);
				time = 0f;
			}

			if (time >= 2f)
			{
				time = 0f;
				if (!slotsManager.IsActive) rollbackInventory = true;
				slotsManager.SetTabActive(true);
				itemManager.SetCurrentItem(target.ItemId);
				target.Delete();
				target = null;
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Item"))
			{
				target = collision.gameObject.GetComponent<Item>();
				isTrigger = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Item"))
			{
				if (target != null)
				{
					target.SetSliderActive(false);
				}
				time = 0f;
				isTrigger = false;
				target = null;
			}
		}
	}
}
