using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Item;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Item;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene.Inventory.Inputs
{
	/// <summary>
	/// 키 입력을 관리합니다.
	/// </summary>
	public class InputManager : MonoBehaviour
	{
		private SlotsManager slotsManager;
		private ItemManager itemManager;
			
		private void Start()
		{
			slotsManager = GetComponent<SlotsManager>();
			itemManager = GetComponent<ItemManager>();
		}

		private void Update()
		{
			// 탭 전환
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				slotsManager.SetTabActive(!slotsManager.IsActive);
			}

			// 아이템 드랍
			if (Input.GetKeyDown(KeyCode.Z))
			{
				// 슬롯 열려 있는지 / 들고 있는 아이템이 있는지 확인
				if (slotsManager.IsActive && itemManager.CurrentItemCode != 0)
				{
					DefineItem item = ItemStorage.Instance.GetItems()[itemManager.CurrentItemCode];
					// 아이템이 버릴 수 있는 경우 -> 버림
					if (item.dropable)
					{
						itemManager.SetCurrentItem(0);
					}
				}
			}
		}
	}
}
