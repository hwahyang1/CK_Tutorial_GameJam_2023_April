using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Item;
using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Slots;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Inputs
{
	/// <summary>
	/// 키 입력을 관리합니다.
	/// </summary>
	public class InputManager : MonoBehaviour
	{
		private SlotsManager slotsManager;
		private ItemManager itemManager;
		private ItemStorage itemStorage;
			
		private void Start()
		{
			slotsManager = GetComponent<SlotsManager>();
			itemManager = GetComponent<ItemManager>();
			itemStorage = GetComponent<ItemStorage>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				slotsManager.SetTabActive(!slotsManager.IsActive);
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				if (slotsManager.IsActive && itemManager.CurrentItemCode != 0)
				{
					DefineItem item = itemStorage.GetItems()[itemManager.CurrentItemCode];
					if (item.dropable)
					{
						itemManager.SetCurrentItem(0);
					}
				}
			}
		}
	}
}
