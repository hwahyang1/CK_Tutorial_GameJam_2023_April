using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.StageScene.Inventory.Slots
{
	/// <summary>
	/// 개별 슬롯을 관리합니다.
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
		
		private System.Action<Vector2Int, bool> overCallback = null;
		private System.Action clickCallback = null;

		private void Awake()
		{
			image = GetComponent<Image>();
		}

		/// <summary>
		/// 슬롯을 초기화 합니다.
		/// </summary>
		/// <param name="id">해당 슬롯에 위치하는 아이템의 ID를 지정합니다.</param>
		/// <param name="uid">ID에 대응하는 UID를 지정합니다.</param>
		/// <param name="position">현재 슬롯의 위치를 지정합니다.</param>
		/// <param name="overCallback">현재 슬롯에 마우스가 들어오거나 나갔을 때의 Callback을 지정합니다. 없는 경우, null로 지정합니다.</param>
		/// <param name="clickCallback">현재 슬롯에 클릭 이벤트가 발생했을 때의 Callback을 지정합니다. 없는 경우, null로 지정합니다.</param>
		/// <param name="activeSprite">현재 슬롯이 활성화 된 상태일 때의 Sprite를 지정합니다.</param>
		/// <param name="inActiveSprite">현재 슬롯이 비활성화 된 상태일 때의 Sprite를 지정합니다.</param>
		/// <remarks>
		/// 아래의 ID는 사전 지정된 ID 입니다.
		/// ItemStorage에 사전 지정된 ID가 존재해도 무시 될 수 있습니다.
		/// -1 -> 사용 안함
		/// 0 -> 빈칸
		/// </remarks>
		public void Init(int id, int uid, Vector2Int position, System.Action<Vector2Int, bool> overCallback, System.Action clickCallback, Sprite activeSprite, Sprite inActiveSprite)
		{
			itemId = id;
			this.uid = uid;
			this.position = position;
			isSlotActive = id != -1;
			this.overCallback = overCallback;
			this.clickCallback = clickCallback;
			image.sprite = isSlotActive ? activeSprite : inActiveSprite;
		}

		/// <summary>
		/// 현재 칸에 지정된 ID를 변경합니다.
		/// </summary>
		/// <param name="id">변경할 ID를 지정합니다.</param>
		/// <param name="uid">ID에 대응하는 UID를 지정합니다.</param>
		/// <remarks>
		/// 아래의 ID는 사전 지정된 ID 입니다.
		/// ItemStorage에 사전 지정된 ID가 존재해도 무시 될 수 있습니다.
		/// -1 -> 사용 안함
		/// 0 -> 빈칸
		/// </remarks>
		public void SetItemId(int id, int uid)
		{
			if (id < 0) return;
			itemId = id;
			this.uid = uid;
		}

		/// <summary>
		/// 현재 칸에 지정된 배경 Sprite를 변경합니다.
		/// </summary>
		/// <param name="sprite">변경할 Sprite를 지정합니다.</param>
		public void SetSprite(Sprite sprite)
		{
			image.sprite = sprite;
		}

		/* ==================== Events ==================== */
		
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
			clickCallback?.Invoke();
		}
	}
}
