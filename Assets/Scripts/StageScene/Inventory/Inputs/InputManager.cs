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
		[SerializeField]
		private SlotsManager slotsManager;
		private ItemManager itemManager;
			
		private void Start()
		{
			itemManager = GetComponent<ItemManager>();
		}

		private void Update()
		{
			if (GameManager.Instance.status != GameStatus.Playing) return;
			
			// 탭 전환
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				slotsManager.SetTabActive(!slotsManager.IsActive);
			}
		}
	}
}
