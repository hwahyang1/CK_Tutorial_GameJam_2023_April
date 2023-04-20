using System;
using System.Collections;
using System.Collections.Generic;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 저장 데이터의 구조를 정의하는 클래스 입니다.
	/// </summary>
	[Serializable]
	public class DefineSaveData
	{
		public Tuple<float, float> playerPosition;
		public float playtime;
		public int playerKeys;
		public float playerStamina;
		public int playerLevel;
		public int playerExp;
		public Tuple<int[][], int[][]> playerInventory;
		public Dictionary<string, Tuple<DefineNpcFlow, int[][], int[][]>> npcInventory;
		
		public DefineSaveData(){}

		public DefineSaveData(
			Tuple<float, float> playerPosition,
			float playtime,
			int playerKeys,
			float playerStamina,
			int playerLevel,
			int playerExp,
			Tuple<int[][], int[][]> playerInventory,
			Dictionary<string, Tuple<DefineNpcFlow, int[][], int[][]>> npcInventory
		)
		{
			this.playerPosition = playerPosition;
			this.playtime = playtime;
			this.playerKeys = playerKeys;
			this.playerStamina = playerStamina;
			this.playerLevel = playerLevel;
			this.playerExp = playerExp;
			this.playerInventory = playerInventory;
			this.npcInventory = npcInventory;
		}
	}
}
