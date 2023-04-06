using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype
{
	/// <summary>
	/// 현재 마우스의 위치를 Canvas의 좌표로 변환합니다.
	/// (Render Mode가 Screen Space - Overlay로 설정 되어 있는 경우에 한함.)
	/// </summary>
	public class CanvasCursorPosition : MonoBehaviour
	{
		[SerializeField]
		private RectTransform targetCanvas;

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
			
			// 화면 크기는 달라지지만 Canvas 상의 좌표값은 바뀌지 않음 -> 보정
			canvasPosition.x = canvasPosition.x / Screen.width * 1920;
			canvasPosition.y = canvasPosition.y / Screen.height * 1080;
		}
	}
}
