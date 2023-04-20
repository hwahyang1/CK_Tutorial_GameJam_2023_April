using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.StageScene.UI
{
	/// <summary>
	/// Description
	/// </summary>
	public class ItemEarn : MonoBehaviour
	{
		[SerializeField]
		private GameObject alertParent;

		private void Awake()
		{
			alertParent.SetActive(false);
		}

		public void Update()
		{
			if (GameManager.Instance.status != GameStatus.ItemEarn) return;

			if (Input.anyKeyDown)
			{
				alertParent.SetActive(false);
				GameManager.Instance.status = GameStatus.Playing;
			}
		}

		public void Show()
		{
			GameManager.Instance.status = GameStatus.ItemEarn;
			alertParent.SetActive(true);
		}
	}
}
