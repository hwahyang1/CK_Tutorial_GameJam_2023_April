using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 저장 데이터의 구조를 정의하는 클래스 입니다.
	/// </summary>
	[Serializable]
	public class DefineSaveData
	{
		public Tuple<float, float> playerPosition;
		public Time playtime;
		public List<bool> playerKeySlots;
		public Tuple<int[][], int[][]> playerInventory;
		public Dictionary<int, Tuple<DefineNpcFlow, int[][], int[][]>> npcInventory;

		public DefineSaveData(
			Tuple<float, float> playerPosition,
			Time playtime,
			List<bool> playerKeySlots,
			Tuple<int[][], int[][]> playerInventory,
			Dictionary<int, Tuple<DefineNpcFlow, int[][], int[][]>> npcInventory
		)
		{
			this.playerPosition = playerPosition;
			this.playtime = playtime;
			this.playerKeySlots = playerKeySlots;
			this.playerInventory = playerInventory;
			this.npcInventory = npcInventory;
		}
	}
}
