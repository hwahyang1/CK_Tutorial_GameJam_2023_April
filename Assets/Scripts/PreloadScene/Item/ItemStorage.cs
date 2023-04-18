using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.PreloadScene.Item
{
	/// <summary>
	/// 사용 가능한 아이템 목록을 정의합니다.
	/// </summary>
	public class ItemStorage : SingleTon<ItemStorage>
	{
		[SerializeField]
		[Tooltip("0번을 제외하고 입력합니다. (0번은 선택해제로 사용됩니다.)")]
		private List<DefineItem> items = new List<DefineItem>();

		/// <summary>
		/// 전체 아이템 목록을 반환합니다.
		/// </summary>
		public List<DefineItem> GetItems() => items;

		/// <summary>
		/// 특정 아이템의 ID를 반환합니다.
		/// </summary>
		/// <param name="targetName">찾을 아이템의 이름을 지정합니다.</param>
		/// <returns>찾은 아이템의 ID를 반환합니다.</returns>
		public int GetIndexOf(string targetName) => items.FindIndex(target => target.name == targetName);
	}
}
