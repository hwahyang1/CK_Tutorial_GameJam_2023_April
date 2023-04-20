using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CK_Tutorial_GameJam_April.StageScene.Items
{
	/// <summary>
	/// 아이템 코드를 관리하는 스크립트
	/// </summary>
	public class Item : MonoBehaviour
	{
		[SerializeField]
		private Canvas uiCanvas;

		[SerializeField]
		private float maxSize;

		[SerializeField]
		private RectTransform progressBackground;

		[SerializeField]
		private RectTransform progressForeground;

		[SerializeField]
		private ParticleSystem[] targetEffects;

		private int itemId;
		public int ItemId => itemId;

		private void Start()
		{
			uiCanvas.worldCamera = Camera.main;
		}

		// value -> 0f~1f
		public void ChangeProgressBar(bool active, float value)
		{
			progressForeground.sizeDelta = Vector2.Lerp(progressForeground.sizeDelta,
			                                            new Vector2(maxSize * value, progressForeground.sizeDelta.y),
			                                            0.5f);

			progressBackground.gameObject.SetActive(active);
			progressForeground.gameObject.SetActive(active);
		}

		public void SetItem(int id, Color effectColor)
		{
			itemId = id;
			foreach (ParticleSystem particle in targetEffects)
			{
				ParticleSystem.MainModule settings = particle.main;
				settings.startColor = new ParticleSystem.MinMaxGradient(effectColor);
				progressForeground.GetComponent<Image>().color = effectColor;
			}
		}
	}
}
