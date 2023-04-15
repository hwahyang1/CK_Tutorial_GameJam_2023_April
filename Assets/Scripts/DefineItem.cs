using System.Collections;
using System.Collections.Generic;
using Serializable = System.SerializableAttribute;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 아이템을 정의합니다.
	/// </summary>
	[Serializable]
	public class DefineItem
	{
		public string name;

		[Tooltip("0과 1만 사용합니다. 공백으로 구분합니다.")]
		public List<string> slotSize;

		public bool dropable; // 버릴 수 있는지

		public int rank; // 아이템 등급

		public float stamina; // 스태미나 회복치
		public int exp; // 경험치

		public Sprite sprite;

		/// <summary>
		/// List<string>인 DefineItem.slotSize를 2차원 bool 배열로 변환합니다.
		/// </summary>
		/// <param name="original">변환할 slotSize 변수를 지정합니다.</param>
		/// <returns>변환된 2차원 bool 배열이 반환됩니다.</returns>
		public static bool[,] ConvertStringListToBoolArray(List<string> original)
		{
			int horizontalCount = original[0].Split(' ').Length;
			bool[,] data = new bool[original.Count, horizontalCount];

			for (int i = 0; i < original.Count; i++)
			{
				string[] currentData = original[i].Split(' ');
				for (int j = 0; j < currentData.Length; j++)
				{
					data[i, j] = currentData[j] == "1";
				}
			}

			return data;
		}
	}
}
