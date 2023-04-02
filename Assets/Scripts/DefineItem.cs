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

		[Tooltip("공백으로 구분합니다.")]
		public List<string> slotSize;

		public Sprite sprite;

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
