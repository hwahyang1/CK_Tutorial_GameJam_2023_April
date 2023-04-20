using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CK_Tutorial_GameJam_April.StageScene.Inventory
{
	/// <summary>
	/// NPC 인벤토리 한정으로 추가되는 항목을 처리합니다.
	/// </summary>
	public class NpcAdditional : MonoBehaviour
	{
		[SerializeField]
		private Text nameText;

		[SerializeField]
		private Text descriptionText;

		[SerializeField]
		private Image npcImage;

		public void Set(string name, string description, Sprite npcSprite)
		{
			nameText.text = name;
			descriptionText.text = description.Replace("\\n", "\n");
			npcImage.sprite = npcSprite;
		}
	}
}
