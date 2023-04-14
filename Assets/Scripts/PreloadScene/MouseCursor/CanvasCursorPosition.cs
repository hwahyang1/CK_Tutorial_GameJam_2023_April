using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CK_Tutorial_GameJam_April.PreloadScene.MouseCursor
{
	/// <summary>
	/// 현재 마우스의 위치를 Canvas의 좌표로 변환합니다.
	/// (Tag - MainCanvas로 등록된 Canvas의 Render Mode가 Screen Space - Overlay로 설정 되어 있는 경우에 한함.)
	/// </summary>
	public class CanvasCursorPosition : SingleTon<CanvasCursorPosition>
	{
		private CanvasScaler targetCanvas;
		private bool runCalculate = false;
		private string previousSceneName = "";
		
		private static Vector2 canvasPosition;
		public static Vector2 CanvasPosition => canvasPosition;

		protected override void Update()
		{
			// Scene이 변경 된 경우 -> targetCanvas 교체
			string currentSceneName = SceneManager.GetActiveScene().name;
			if (currentSceneName != previousSceneName)
			{
				RefreshTargetCanvas();
				previousSceneName = currentSceneName;
			}

			if (!runCalculate) return;
			
			// 위치 가져오고 값이 스크린 크기를 못넘어가게 조정
			canvasPosition = Input.mousePosition;

			if (canvasPosition.x < 0) canvasPosition.x = 0;
			if (canvasPosition.y < 0) canvasPosition.y = 0;
			if (canvasPosition.x > Screen.width) canvasPosition.x = Screen.width;
			if (canvasPosition.y > Screen.height) canvasPosition.y = Screen.height;
			
			// 화면 크기는 달라지지만 Canvas 상의 좌표값은 바뀌지 않음 -> 보정
			canvasPosition.x = canvasPosition.x / Screen.width * targetCanvas.referenceResolution.x;
			canvasPosition.y = canvasPosition.y / Screen.height * targetCanvas.referenceResolution.y;
		}

		/// <summary>
		/// 위치 계산의 기반이 되는 Target Canvas를 다시 찾습니다.
		/// </summary>
		public void RefreshTargetCanvas()
		{
			GameObject target = GameObject.FindGameObjectWithTag("CursorPositionTargetCanvas");
			if (target == null)
			{
				runCalculate = false;
				return;
			}
			runCalculate = true;
			targetCanvas = target.GetComponent<CanvasScaler>();
		}
	}
}
