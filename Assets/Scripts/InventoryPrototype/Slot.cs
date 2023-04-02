using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.InventoryPrototype
{
	/// <summary>
	/// 인벤토리의 각 슬롯을 관리합니다.
	/// </summary>
	public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
	{
		[Header("Status")]
		[SerializeField, ReadOnly]
		private Vector2Int position;
		public Vector2Int Position => position;
		
		[SerializeField, ReadOnly]
		private bool isSlotActive = false;
		public bool IsSlotActive => isSlotActive;

		[SerializeField, ReadOnly]
		private int itemId = 0;
		public int ItemId => itemId;

		[SerializeField, ReadOnly]
		private int uid = 0;
		public int Uid => uid;

		private Image image;
		private bool protectModify = false;
		
		private System.Action<Vector2Int, bool> overCallback = null;
		private System.Action clickCallback = null;

		private void Awake()
		{
			image = GetComponent<Image>();
		}

		// -1 -> 사용안함
		// 0 -> 빈 슬롯
		// 1~ -> 아이템
		public void Init(int id, int uid, Vector2Int position, System.Action<Vector2Int, bool> overCallback, System.Action clickCallback, Sprite activeSprite, Sprite inActiveSprite)
		{
			itemId = id;
			this.uid = uid;
			this.position = position;
			isSlotActive = id != -1;
			this.overCallback = overCallback;
			this.clickCallback = clickCallback;
			image.sprite = isSlotActive ? activeSprite : inActiveSprite;

			StartCoroutine(PauseForSeconds());
		}

		public void SetItemId(int id, int uid)
		{
			if (id < 0) return;
			itemId = id;
			this.uid = uid;

			StartCoroutine(PauseForSeconds());
		}

		public void SetSprite(Sprite sprite)
		{
			image.sprite = sprite;
		}
		
		public void OnPointerEnter(PointerEventData eventData)
		{
			overCallback?.Invoke(position, true);
		}
		
		public void OnPointerExit(PointerEventData eventData)
		{
			overCallback?.Invoke(position, false);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (protectModify) return;
			clickCallback?.Invoke();
		}

		private IEnumerator PauseForSeconds()
		{
			protectModify = true;
			yield return new WaitForSeconds(0.25f);
			protectModify = false;
		}
	}
}
