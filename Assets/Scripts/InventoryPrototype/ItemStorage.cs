using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.InventoryPrototype
{
	/// <summary>
	/// 사용 가능한 아이템 목록을 정의합니다.
	/// </summary>
	public class ItemStorage : MonoBehaviour
	{
		[SerializeField]
		private List<DefineItem> items = new List<DefineItem>();

		public List<DefineItem> GetItems() => items;
		public int GetIndexOf(string targetName) => items.FindIndex(target => target.name == targetName);
	}
}
