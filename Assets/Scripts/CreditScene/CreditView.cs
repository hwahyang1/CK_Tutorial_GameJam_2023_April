using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.CreditScene
{
	/// <summary>
	/// 크레딧의 노출을 관리합니다.
	/// </summary>
	public class CreditView : MonoBehaviour
	{
		[SerializeField]
		private Transform parent;

		[SerializeField]
		private Vector2 startAt;

		[SerializeField]
		private Vector2 endAt;

		[SerializeField]
		private float flowSpeed;

		[SerializeField]
		private float flowSpeedMultiply; // 마우스 눌렀을 때 속도 가중치

		private RectTransform rectTransform;

		private void Start()
		{
			rectTransform = parent.GetComponent<RectTransform>();
			rectTransform.anchoredPosition = startAt;
		}

		private void Update()
		{
			EventSystem.current.SetSelectedGameObject(null);

			float multiply = 1f;
			if (Input.GetMouseButton(0)) multiply = flowSpeedMultiply;

			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + (flowSpeed * multiply * Time.deltaTime));

			if (rectTransform.anchoredPosition.x >= endAt.x && rectTransform.anchoredPosition.y >= endAt.y)
			{
				Exit();
			}
		}

		public void Exit()
		{
			SceneChange.Instance.Unload("CreditScene");
		}
	}
}
