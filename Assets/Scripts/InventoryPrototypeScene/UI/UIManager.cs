using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CK_Tutorial_GameJam_April.InventoryPrototypeScene.Slots;

namespace CK_Tutorial_GameJam_April.InventoryPrototypeScene.UI
{
	/// <summary>
	/// 프로토타입에서 사용되는 UI를 관리합니다.
	/// </summary>
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private Text mousePositionText;
		[SerializeField]
		private Text inventoryPositionText;
		
		private readonly int[,] blankUid = new int[5, 5];

		private SlotsManager slotsManager;
		private CanvasCursorPosition canvasCursorPosition;

		private void Awake()
		{
			slotsManager = GetComponent<SlotsManager>();
			canvasCursorPosition = GetComponent<CanvasCursorPosition>();
		}

		private void Update()
		{
			inventoryPositionText.text = "Inventory Position: " + slotsManager.CurrentInventoryPosition.ToString("F1");
			mousePositionText.text += "Canvas Position: " + canvasCursorPosition.CanvasPosition.ToString("F1");
		}

		/* ==================== Events ==================== */

		public void OnClickInventoryType0()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 }
			                           }, blankUid);
		}

		public void OnClickInventoryType1()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { -1, -1, 0, -1, -1 },
				                           { -1, 0, 0, 0, -1 },
				                           { 0, 0, 0, 0, 0 },
				                           { -1, 0, 0, 0, -1 },
				                           { -1, -1, 0, -1, -1 }
			                           }, blankUid);
		}

		public void OnClickInventoryType2()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, 0, 0, 0 },
				                           { -1, 0, 0, 0, -1 },
				                           { -1, -1, 0, -1, -1 },
				                           { -1, 0, 0, 0, -1 },
				                           { 0, 0, 0, 0, 0 }
			                           }, blankUid);
		}

		public void OnClickInventoryType3()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, -1, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 }
			                           }, blankUid);
		}

		public void OnClickInventoryType4()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, -1, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { -1, 0, 0, 0, -1 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, -1, 0, 0 }
			                           }, blankUid);
		}
	}
}
