using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.MenuScene
{
	/// <summary>
	/// 배경 이미지의 노출을 관리합니다.
	/// </summary>
	public class MenuBackground : MonoBehaviour
	{
		[SerializeField]
		private RectTransform parent;

		[SerializeField]
		private Vector2 startAt;

		[SerializeField]
		private Vector2 endAt;

		[SerializeField]
		private float flowSpeed;

		private void Start()
		{
			parent.anchoredPosition = startAt;
		}

		private void Update()
		{
			EventSystem.current.SetSelectedGameObject(null);

			parent.anchoredPosition = new Vector2(parent.anchoredPosition.x, parent.anchoredPosition.y + (-flowSpeed * Time.deltaTime));

			if (parent.anchoredPosition.x <= endAt.x && parent.anchoredPosition.y <= endAt.y)
			{
				parent.anchoredPosition = endAt;
				gameObject.SetActive(false);
			}
		}
	}
}
