using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.StageScene.Items;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Item;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene.Character
{
	/// <summary>
	/// 캐릭터의 판정을 관리합니다.
	/// </summary>
	public class CharacterManager : MonoBehaviour
	{
		[SerializeField]
		private SlotsManager slotsManager;

		[SerializeField]
		private ItemManager itemManager;

		private float time = 0f;

		private Item item;
		private Npc npc;

		private bool isTrigger = false;

		private bool isNpc = false;

		private bool rollbackInventory;

		private void Update()
		{
			if (GameManager.Instance.status != GameStatus.Playing) return;

			if (Input.GetMouseButtonDown(0) && isNpc)
			{
				npc.Interaction();
			}

			if (itemManager.CurrentItemCode != 0) return;

			if (rollbackInventory)
			{
				slotsManager.SetTabActive(false);
				rollbackInventory = false;
			}

			if (Input.GetMouseButton(0) && isTrigger) // 2초간 클릭하면 아이템이 먹어지므로 time에 deltatime을 더해서 구함
			{
				item.ChangeProgressBar(true, time / 2f);
				time += Time.deltaTime;
			}

			if (Input.GetMouseButtonUp(0)) // 마우스 클릭을 그만하면 time을 0으로 초기화
			{
				if (isTrigger) item.ChangeProgressBar(false, 0f);
				time = 0f;
			}

			if (time >= 2f && isTrigger) // 만약 isTrigger상태이고, 2초이상 클릭했다면 아이템을 획득
			{
				time = 0f;
				if (!slotsManager.IsActive) rollbackInventory = true;
				item.ChangeProgressBar(false, 2f);
				slotsManager.SetTabActive(true);
				itemManager.SetCurrentItem(item.ItemId);
				//Destroy(item.gameObject);
				item.gameObject.SetActive(false);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Item"))
			{
				time = 0f;
				item = other.GetComponent<Item>();
				isTrigger = true;
			}

			if (other.gameObject.CompareTag("NPC"))
			{
				npc = other.GetComponent<Npc>();
				isNpc = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Item"))
			{
				time = 0f;
				isTrigger = false;
			}

			if (other.gameObject.CompareTag("NPC"))
			{
				npc.CloseInventory();
				isNpc = false;
			}
		}
	}
}
