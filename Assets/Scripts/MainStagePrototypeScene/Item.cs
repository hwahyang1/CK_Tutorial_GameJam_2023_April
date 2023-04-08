using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene
{
	/// <summary>
	/// 개별 아이템을 정의합니다.
	/// </summary>
	public class Item : MonoBehaviour
	{
		[SerializeField]
		private int itemId;
		public int ItemId => itemId;

		[SerializeField]
		private Transform sliderBackground;
		[SerializeField]
		private Transform sliderForeground;

		private Animator animator;

		private void Start()
		{
			animator = GetComponent<Animator>();
			SetSliderActive(false);
		}
		
		public void SetSliderActive(bool active)
		{
			sliderBackground.gameObject.SetActive(active);
			sliderForeground.gameObject.SetActive(active);
			animator.SetBool("Active", active);
		}

		public void Delete()
		{
			Destroy(gameObject);
		}
	}
}
