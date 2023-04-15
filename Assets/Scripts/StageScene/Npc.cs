using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene
{
	/// <summary>
	/// 조력자를 관리합니다.
	/// </summary>
	public class Npc : MonoBehaviour
	{
		[SerializeField]
		private SlotsManager slotsManager;
		
		[Tooltip("0과 -1만 사용합니다. 공백으로 구분합니다.")]
		public List<string> slotSize;

		private Tuple<int[][], int[][]> backup;

		private void Awake()
		{
			int horizontalCount = slotSize[0].Split(' ').Length;
			int[][] uidData = new int[slotSize.Count][];
			int[][] slotData = new int[slotSize.Count][];

			for (int i = 0; i < slotSize.Count; i++)
			{
				uidData[i] = new int[horizontalCount];
				slotData[i] = new int[horizontalCount];
				
				string[] currentData = slotSize[i].Split(' ');
				for (int j = 0; j < currentData.Length; j++)
				{
					uidData[i][j] = 0;
					slotData[i][j] = int.Parse(currentData[j]);
				}
			}

			backup = new Tuple<int[][], int[][]>(slotData, uidData);
		}

		public void OpenInventory()
		{
			if (slotsManager.IsActive) return;
			
			slotsManager.InitFromArray(backup.Item1, backup.Item2);
			slotsManager.SetTabActive(true);
		}

		public void CloseInventory()
		{
			if (!slotsManager.IsActive) return;
			
			backup = slotsManager.ExportAllTilesIdsUids();
			slotsManager.SetTabActive(false);
		}
	}
}
