using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CK_Tutorial_GameJam_April.StageScene.Save;

namespace CK_Tutorial_GameJam_April.StageScene.Inventory
{
	/// <summary>
	/// 플레이어 인벤토리 한정으로 추가되는 항목을 처리합니다.
	/// </summary>
	public class PlayerAdditional : MonoBehaviour
	{
		[SerializeField]
		private Transform keysParent;

		[SerializeField]
		private Text keysAdditionalText;

		[SerializeField]
		private Exit exit;

		private Color disabledColor = new Color(1f, 1f, 1f, 0f);

		private Color enabledColor = new Color(1f, 1f, 1f, 1f);
		
		private int currentKeysCount = 0;
		public int CurrentKeysCount => currentKeysCount;

		private void Awake()
		{
			UpdateUI();
		}

		private void Start()
		{
			DefineSaveData data = GameSaveData.Instance.SaveData;
			if (data == null) return;

			SetKeyCount(data.playerKeys);
		}

		public void SetKeyCount(int count)
		{
			currentKeysCount = count;
			
			UpdateUI();
		}

		public void UpdateUI()
		{
			for (int i = 0; i < keysParent.childCount; i++)
			{
				Image current = keysParent.GetChild(i).GetChild(0).GetComponent<Image>();
				current.color = i < currentKeysCount ? enabledColor : disabledColor;
			}

			keysAdditionalText.gameObject.SetActive(keysParent.childCount <= currentKeysCount);
			if (keysParent.childCount <= currentKeysCount)
			{
				keysAdditionalText.text = $"+{currentKeysCount - keysParent.childCount}";
			}
			
			exit.SetDoorActive(currentKeysCount >= 3);
		}
	}
}
