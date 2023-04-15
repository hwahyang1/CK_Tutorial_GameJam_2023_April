using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// Description
	/// </summary>
	public class HideObjectManager : MonoBehaviour
	{
		private Tilemap tilemap;

		private void Start()
		{
			tilemap = GetComponent<Tilemap>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				tilemap.color = new Color(1f, 1f, 1f, 0.3f);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				tilemap.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}
}
