using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace CK_Tutorial_GameJam_April.StageScene
{
	/// <summary>
	/// Description
	/// </summary>
	public class HideObjectManager : MonoBehaviour
	{
		private Tilemap tilemap;

		[SerializeField]
		private Color beforeColor;

		[SerializeField]
		private Color afterColor;

		[SerializeField]
		private bool useTrigger = true;

		private Color targetColor;

		public bool isTrigger = false;

		private void Start()
		{
			tilemap = GetComponent<Tilemap>();
			tilemap.color = beforeColor;
			targetColor = beforeColor;
		}

		private void FixedUpdate()
		{
			tilemap.color = Color.Lerp(tilemap.color, targetColor, 0.25f);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (useTrigger) return;
			if (other.gameObject.CompareTag("Player"))
			{
				//tilemap.color = afterColor;
				targetColor = afterColor;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (useTrigger) return;
			if (other.gameObject.CompareTag("Player"))
			{
				//tilemap.color = beforeColor;
				targetColor = beforeColor;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!useTrigger) return;
			if (other.gameObject.CompareTag("Player"))
			{
				//tilemap.color = afterColor;
				targetColor = afterColor;
				isTrigger = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!useTrigger) return;
			if (other.gameObject.CompareTag("Player"))
			{
				//tilemap.color = beforeColor;
				targetColor = beforeColor;
				isTrigger = false;
			}
		}
	}
}
