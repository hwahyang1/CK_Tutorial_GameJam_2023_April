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
		private RectTransform parent;

		[SerializeField]
		private Vector2 startAt;

		[SerializeField]
		private Vector2 endAt;

		[SerializeField]
		private float flowSpeed;

		[SerializeField]
		private float flowSpeedMultiply; // 마우스 눌렀을 때 속도 가중치

		[SerializeField]
		private GameObject backButton;

		[SerializeField]
		private List<GameObject> targetObjects = new List<GameObject>();
		
		private void Start()
		{
			parent.anchoredPosition = startAt;
			
			backButton.SetActive(CreditParams.Instance.isControllable);

			foreach (GameObject currentObject in targetObjects)
			{
				if (CreditParams.Instance.isControllable)
				{
					Destroy(currentObject);
				}
			}
		}

		private void Update()
		{
			EventSystem.current.SetSelectedGameObject(null);

			float multiply = 1f;
			if (Input.GetMouseButton(0) && CreditParams.Instance.isControllable) multiply = flowSpeedMultiply;

			parent.anchoredPosition = new Vector2(parent.anchoredPosition.x, parent.anchoredPosition.y + (flowSpeed * multiply * Time.deltaTime));

			if (parent.anchoredPosition.x >= endAt.x && parent.anchoredPosition.y >= endAt.y)
			{
				Exit();
			}
		}

		public void Exit()
		{
			if (CreditParams.Instance.isControllable)
			{
				CreditParams.Instance.Exit();
				SceneChange.Instance.Unload("CreditScene");
			}
			else
			{
				CreditParams.Instance.Exit();
				SceneChange.Instance.ChangeScene("MenuScene");
			}
		}
	}
}
