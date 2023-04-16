using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

using CK_Tutorial_GameJam_April.PreloadScene.Item;
using CK_Tutorial_GameJam_April.PreloadScene.MouseCursor;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene.Inventory.Item
{
	/// <summary>
	/// 아이템의 소환과 배치, 상태를 관리합니다.
	/// </summary>
	public class ItemManager : MonoBehaviour
	{
		[SerializeField]
		private List<SlotsManager> slotsManagers;
		
		[SerializeField]
		private Image overlayImage;

		[SerializeField]
		private float overlaySizeMultiply = 100;

		[SerializeField, ReadOnly]
		private int currentItemCode = 0;

		public int CurrentItemCode => currentItemCode;

		private bool[,] currentItemSlots;

		private void Start()
		{
			SetCurrentItem(0);
		}

		private void Update()
		{
			foreach (SlotsManager slotsManager in slotsManagers) slotsManager.ResetAllTiles();

			if (currentItemCode == 0) return;

			overlayImage.rectTransform.anchoredPosition = CanvasCursorPosition.CanvasPosition;
			// 짝수인경우 0.5칸 조정해서 위치시킴
			if (currentItemSlots.GetLength(1) % 2 == 0)
				overlayImage.rectTransform.anchoredPosition -= new Vector2(0.5f * overlaySizeMultiply, 0);
			if (currentItemSlots.GetLength(0) % 2 == 0)
				overlayImage.rectTransform.anchoredPosition += new Vector2(0, 0.5f * overlaySizeMultiply);

			if (GameManager.Instance.status != GameStatus.Playing) return;
			
			foreach (SlotsManager slotsManager in slotsManagers)
			{
				List<List<Slot>> tiles = slotsManager.ExportAllTargetTiles(currentItemSlots);
				KeyValuePair<int, int> validateResult = slotsManager.ValidateTiles(tiles);
				slotsManager.VisualizeTiles(tiles, validateResult.Key);

				if (Input.GetMouseButtonDown(0) && validateResult.Key != 0)
				{
					int itemId =
						slotsManager.PlaceItem(tiles, currentItemCode, validateResult.Key == 2, validateResult.Value);
					SetCurrentItem(itemId);
				}
			}
		}

		/// <summary>
		/// 현재 선택된 아이템을 교체합니다.
		/// </summary>
		/// <param name="code">바꿀 아이템의 ID를 지정합니다.</param>
		/// <remarks>
		/// 아래의 ID는 사전 지정된 ID 입니다.
		/// ItemStorage에 사전 지정된 ID가 존재해도 무시 될 수 있습니다.
		/// -1 -> 사용 안함
		/// 0 -> 빈칸
		/// </remarks>
		public void SetCurrentItem(int code)
		{
			DefineItem currentItem = ItemStorage.Instance.GetItems()[code];

			currentItemCode = code;
			overlayImage.gameObject.SetActive(code != 0);

			if (code == 0) return;
			currentItemSlots = DefineItem.ConvertStringListToBoolArray(currentItem.slotSize);

			overlayImage.sprite = currentItem.sprite;
			overlayImage.rectTransform.sizeDelta =
				new Vector2(currentItemSlots.GetLength(1) * overlaySizeMultiply,
				            currentItemSlots.GetLength(0) * overlaySizeMultiply);
		}
	}
}
