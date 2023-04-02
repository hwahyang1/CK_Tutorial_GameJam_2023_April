using System.Collections;
using System.Collections.Generic;
using Array = System.Array;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.InventoryPrototype
{
	/// <summary>
	/// 전체 슬롯을 관리합니다.
	/// </summary>
	public class SlotsManager : MonoBehaviour
	{
		[SerializeField]
		private Text inventoryPositionText;
		
		[SerializeField]
		private Transform slotsParent;
		
		[SerializeField]
		private Transform overlayParent;

		[SerializeField]
		private GameObject imagePrefab;

		[SerializeField]
		private float overlaySizeMultiply = 100;

		[SerializeField]
		private Vector2Int inventoryMaxSize;

		private Slot[][] inventory;

		// uid - Image(GameObject) Pair
		private Dictionary<int, Image> overlayImages = new Dictionary<int, Image>();

		[Header("Status")]
		[SerializeField, ReadOnly]
		private Vector2Int currentInventoryPosition = new Vector2Int(-1, -1);

		[Header("Sprites - Status")]
		[SerializeField]
		private Sprite activeSprite;

		[SerializeField]
		private Sprite inactiveSprite;

		[Header("Sprites - Sign")]
		[SerializeField]
		private Sprite allowSprite;

		[SerializeField]
		private Sprite disallowSprite;

		[SerializeField]
		private Sprite duplicateSprite;
		
		private bool protectModify = false;

		private ItemStorage itemStorage;
		private ItemManager itemManager;

		private void Start()
		{
			itemManager = GetComponent<ItemManager>();
			itemStorage = GetComponent<ItemStorage>();
			Init();
		}

		private void Init()
		{
			inventory = new Slot[inventoryMaxSize.x][];
			for (int i = 0; i < inventory.Length; i++)
				inventory[i] = new Slot[inventoryMaxSize.y];
			overlayImages = new Dictionary<int, Image>();

			for (int i = 0; i < inventoryMaxSize.x; i++)
			{
				Transform horizontalParent = slotsParent.GetChild(i);
				for (int j = 0; j < inventoryMaxSize.y; j++)
				{
					Slot slot = horizontalParent.GetChild(j).gameObject.GetComponent<Slot>();
					slot.Init(0, 0, new Vector2Int(i, j), ProcessMouseOverEvent, ProcessMouseClickEvent, activeSprite, inactiveSprite);
					inventory[i][j] = slot;
				}
			}
		}

		public void InitFromArray(int[,] array, int[,] uidArray)
		{
			inventory = new Slot[array.GetLength(0)][];
			for (int i = 0; i < inventory.Length; i++)
				inventory[i] = new Slot[array.GetLength(1)];
			List<int> usedUid = new List<int>() { 0 };

			foreach (KeyValuePair<int, Image> currentImage in overlayImages)
				Destroy(currentImage.Value.gameObject);
			overlayImages = new Dictionary<int, Image>();

			for (int i = 0; i < inventory.Length; i++)
			{
				Transform horizontalParent = slotsParent.GetChild(i);
				for (int j = 0; j < inventory[i].Length; j++)
				{
					Slot slot = horizontalParent.GetChild(j).gameObject.GetComponent<Slot>();
					slot.Init(array[i, j], uidArray[i, j], new Vector2Int(i, j), ProcessMouseOverEvent, ProcessMouseClickEvent, activeSprite, inactiveSprite);
					inventory[i][j] = slot;

					if (!usedUid.Contains(uidArray[i, j]))
					{
						PlaceOverlay(array[i, j], uidArray[i, j]);
						usedUid.Add(uidArray[i, j]);
					}
				}
			}
		}

		private void ProcessMouseOverEvent(Vector2Int position, bool enter)
		{
			currentInventoryPosition = enter ? position : new Vector2Int(-1, -1);
		}

		private void ProcessMouseClickEvent()
		{
			if (protectModify) return;
			
			if (itemManager.CurrentItemCode != 0) return;
			
			Slot targetSlot = inventory[currentInventoryPosition.x][currentInventoryPosition.y];
			if (targetSlot.ItemId <= 0) return;

			itemManager.SetCurrentItem(targetSlot.ItemId);
			DeleteItem(targetSlot.Uid);

			StartCoroutine(PauseForSeconds());
		}

		private void Update()
		{
			inventoryPositionText.text = "Inventory Position: " + currentInventoryPosition.ToString("F1");
		}

		public void ResetAllTiles()
		{
			for (int i = 0; i < inventory.Length; i++)
			{
				for (int j = 0; j < inventory[i].Length; j++)
				{
					if (!inventory[i][j].IsSlotActive) continue;
					inventory[i][j].SetSprite(activeSprite);
				}
			}
		}

		public List<List<Slot>> ExportAllTargetTiles(bool[,] itemPosition)
		{
			// 대상 칸 전부 추출
			int d0Length = itemPosition.GetLength(0);
			int d1Length = itemPosition.GetLength(1);
			List<List<Slot>> targetSlots = new List<List<Slot>>();

			if (currentInventoryPosition.x < 0 || currentInventoryPosition.y < 0) return targetSlots;

			for (int i = 0; i < d0Length; i++)
			{
				targetSlots.Add(new List<Slot>());
				for (int j = 0; j < d1Length; j++)
				{
					if (!itemPosition[i, j]) continue;

					int targetX = currentInventoryPosition.x + i - d0Length / 2;
					int targetY = currentInventoryPosition.y + j - d1Length / 2;
					if (targetX < 0 || targetY < 0 || targetX >= inventory.Length ||
					    targetY >= inventory[0].Length) targetSlots[i].Add(null);
					else targetSlots[i].Add(inventory[targetX][targetY]);
				}
			}

			return targetSlots;
		}

		// 0 -> 불가능 / 1 -> 가능 / 2 -> 중복
		public KeyValuePair<int, int> ValidateTiles(List<List<Slot>> targetSlots)
		{
			if (targetSlots.Count <= 0 || targetSlots[0].Count <= 0) return new KeyValuePair<int, int>(0, -1);

			int duplicateUid = -1;
			int isAvaliable = 1;
			for (int i = 0; i < targetSlots.Count; i++)
			{
				for (int j = 0; j < targetSlots[i].Count; j++)
				{
					Slot currentSlot = targetSlots[i][j];
					
					if (currentSlot == null)
					{
						isAvaliable = 0;
						break;
					}

					switch (currentSlot.ItemId)
					{
						case -1: // 없는칸
							isAvaliable = 0;
							break;
						case 0: // 빈칸
							break;
						default: // 사용중
							int targetUid = currentSlot.Uid;
							if (duplicateUid != -1 && duplicateUid != targetUid)
							{
								isAvaliable = 0;
							}
							else
							{
								duplicateUid = targetUid;
								isAvaliable = 2;
							}
							break;
					}

					if (isAvaliable == 0) break;
				}

				if (isAvaliable == 0) break;
			}

			return new KeyValuePair<int, int>(isAvaliable, duplicateUid);
		}

		public void VisualizeTiles(List<List<Slot>> targetSlots, int isAvaliable)
		{
			for (int i = 0; i < targetSlots.Count; i++)
			{
				for (int j = 0; j < targetSlots[i].Count; j++)
				{
					Slot currentSlot = targetSlots[i][j];
					
					if (currentSlot == null || currentSlot.ItemId == -1) continue;

					switch (isAvaliable)
					{
						case 0:
							currentSlot.SetSprite(disallowSprite);
							break;
						case 1:
							currentSlot.SetSprite(allowSprite);
							break;
						case 2:
							currentSlot.SetSprite(duplicateSprite);
							break;
					}
				}
			}
		}

		public int PlaceItem(List<List<Slot>> targetSlots, int id, bool replaceSlot, int replaceUid = -1)
		{
			long timestamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
			float random = Random.Range(0.01f, 1.2f);
			int uid = Mathf.RoundToInt(timestamp * random);

			StartCoroutine(PauseForSeconds());

			int res = 0;
			if (replaceSlot)
			{
				res = DeleteItem(replaceUid);
			}

			for (int i = 0; i < targetSlots.Count; i++)
			{
				for (int j = 0; j < targetSlots[i].Count; j++)
				{
					if (targetSlots[i][j] == null) continue;

					targetSlots[i][j].SetItemId(id, uid);
				}
			}

			PlaceOverlay(id, uid);

			return res;
		}

		private void PlaceOverlay(int id, int uid)
		{
			Slot first = null;
			for (int i = 0; i < inventory.GetLength(0); i++)
			{
				Slot[] slots = Array.FindAll(inventory[i], target => target.Uid == uid);
				foreach (Slot slot in slots)
				{
					first = slot;
					break;
				}
				if (first != null) break;;
			}

			int length = 0;
			DefineItem item = itemStorage.GetItems()[id];
			string[] firstLine = item.slotSize[0].Split(' ');
			for (int i = 0; i < firstLine.Length; i++)
			{
				if (firstLine[i] == "1")
				{
					length = i;
					break;
				}
			}

			Image image = Instantiate(imagePrefab, overlayParent).GetComponent<Image>();
			image.sprite = item.sprite;
			RectTransform rectTransform = image.GetComponent<RectTransform>(); 
			rectTransform.sizeDelta = new Vector2(firstLine.Length * overlaySizeMultiply, item.slotSize.Count * overlaySizeMultiply);
			rectTransform.anchoredPosition = new Vector2(first.Position.y * overlaySizeMultiply, (first.Position.x + length) * overlaySizeMultiply * -1);
			
			overlayImages.Add(uid, image);
		}

		public int DeleteItem(int uid)
		{
			int id = 0;
			
			for (int i = 0; i < inventory.GetLength(0); i++)
			{
				Slot[] slots = Array.FindAll(inventory[i], target => target.Uid == uid);
				foreach (Slot slot in slots)
				{
					id = slot.ItemId;
					slot.SetItemId(0, -1);
				}
			}
			
			Destroy(overlayImages[uid].gameObject);
			overlayImages.Remove(uid);

			return id;
		}

		private IEnumerator PauseForSeconds()
		{
			protectModify = true;
			yield return new WaitForSeconds(0.25f);
			protectModify = false;
		}
	}
}
