using System.Collections;
using System.Collections.Generic;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;
using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// Description
	/// </summary>
	public class LevelManager : MonoBehaviour
	{
		private int level;

		[SerializeField]
		private CharacterManager characterManager;

		[SerializeField]
		private SlotsManager slotsManager;

		private void Update()
		{
			level = characterManager.level;

			switch (level)
			{
				case 0:
					slotsManager.InitFromArray(new int[6, 5]
					                           {
						                           { -1, -1, -1, -1, -1 },
						                           { -1, 0, 0, -1, -1 },
						                           { 0, 0, 0, 0, -1 },
						                           { -1, 0, 0, -1, -1 },
						                           { -1, -1, -1, -1, -1 },
						                           { -1, -1, -1, -1, -1 }
					                           }, new int[6, 5]);
					break;
				case 1:
					slotsManager.InitFromArray(new int[6, 5]
					                           {
						                           { -1, -1, -1, -1, -1 },				                         
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, -1 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, -1 },
						                           { -1, -1, -1, -1, -1 }
					                           }, new int[6, 5]);
					break;
				case 2:
					slotsManager.InitFromArray(new int[6, 5]
					                           {
						                           { -1, 0, 0, -1, -1 },
						                           { -1, 0, 0, 0, -1 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, -1, -1, -1, -1 }
					                           }, new int[6, 5]);
					break;
				case 3:
					slotsManager.InitFromArray(new int[6, 5]
					                           {
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, 0, 0, 0, 0 },
						                           { -1, -1, -1, -1, -1 }
					                           }, new int[6, 5]);
					break;
				case 4:
					slotsManager.InitFromArray(new int[6, 5]
					                           {
						                           { -1, 0, 0, -1, -1 },
						                           { 0, 0, 0, 0, -1 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 }
					                           }, new int[6, 5]);
					break;
				case 5:
					slotsManager.InitFromArray(new int[6, 5]
					                           {
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 },
						                           { 0, 0, 0, 0, 0 }
					                           }, new int[6, 5]);
					break;
			}
		}
	}
}
