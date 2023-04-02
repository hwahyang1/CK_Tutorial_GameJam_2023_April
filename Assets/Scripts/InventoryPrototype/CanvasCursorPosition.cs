using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.InventoryPrototype
{
	/// <summary>
	/// 현재 마우스의 위치를 Canvas의 좌표로 변환합니다.
	/// (Screen Space - Overlay로 설정 된 경우에 한함.)
	/// </summary>
	public class CanvasCursorPosition : MonoBehaviour
	{
		[SerializeField]
		private RectTransform targetCanvas;

		[SerializeField]
		private Text mousePositionText;

		[Header("Status")]
		[SerializeField, ReadOnly]
		private Vector2 canvasPosition;
		public Vector2 CanvasPosition => canvasPosition;

		private void Update()
		{
			canvasPosition = Input.mousePosition;

			if (canvasPosition.x < 0) canvasPosition.x = 0;
			if (canvasPosition.y < 0) canvasPosition.y = 0;
			if (canvasPosition.x > Screen.width) canvasPosition.x = Screen.width;
			if (canvasPosition.y > Screen.height) canvasPosition.y = Screen.height;

			mousePositionText.text = "Mouse Position: " + canvasPosition.ToString("F1");
		}
	}
}
