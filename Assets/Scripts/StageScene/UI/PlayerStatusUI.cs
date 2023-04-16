using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CK_Tutorial_GameJam_April.StageScene.Character;

namespace CK_Tutorial_GameJam_April.StageScene.UI
{
	/// <summary>
	/// 플레이어의 현재 상태를 보여줍니다.
	/// </summary>
	public class PlayerStatusUI : MonoBehaviour
	{
		[SerializeField]
		private LevelManager levelManager;

		[SerializeField]
		private float max;

		[Header("Stamina")]
		[SerializeField]
		private RectTransform staminaFront;
		[SerializeField]
		private Text staminaText;

		[Header("Exp")]
		[SerializeField]
		private RectTransform expFront;
		[SerializeField]
		private Text expText;

		private void Update()
		{
			// 스태미나
			Vector2 staminaSizeDelta = staminaFront.sizeDelta;
			staminaFront.sizeDelta = Vector2.Lerp(staminaSizeDelta,
			                                      new Vector2(1f * levelManager.Stamina / levelManager.MaxStamina * max,
			                                                  staminaSizeDelta.y), 0.5f);
			staminaText.text = $"Stat: {levelManager.Stamina:0.#}/{levelManager.MaxStamina:0.#}";

			// 경험치
			Vector2 expSizeDelta = expFront.sizeDelta;
			if (levelManager.Level == 5)
			{
				expFront.sizeDelta = new Vector2(max, expSizeDelta.y);
				expText.text = "MAX";
			}
			else
			{
				expFront.sizeDelta = Vector2.Lerp(expSizeDelta,
				                                  new Vector2(1f * levelManager.Exp / levelManager.MaxExp * max,
				                                              expSizeDelta.y), 0.5f);
				expText.text = $"Exp: {levelManager.Exp}/{levelManager.MaxExp}";
			}
		}
	}
}
