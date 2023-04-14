using System;
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
		private bool isTrigger;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Player")
			{
				Color color = GetComponent<Tilemap>().color;
				color.a = 30;
			}
		}
	}
}
