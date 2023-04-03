using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.InventoryPrototype
{
	/// <summary>
	/// 각 버튼의 상호작용을 관리합니다.
	/// </summary>
	public class ButtonInteractions : MonoBehaviour
	{
		private int[,] blankUid = new int[5, 5];

		private SlotsManager slotsManager;

		private void Awake()
		{
			slotsManager = GetComponent<SlotsManager>();
		}

		public void OnClickInventoryType0()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 }
			                           }, blankUid);
		}

		public void OnClickInventoryType1()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { -1, -1, 0, -1, -1 },
				                           { -1, 0, 0, 0, -1 },
				                           { 0, 0, 0, 0, 0 },
				                           { -1, 0, 0, 0, -1 },
				                           { -1, -1, 0, -1, -1 }
			                           }, blankUid);
		}

		public void OnClickInventoryType2()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, 0, 0, 0 },
				                           { -1, 0, 0, 0, -1 },
				                           { -1, -1, 0, -1, -1 },
				                           { -1, 0, 0, 0, -1 },
				                           { 0, 0, 0, 0, 0 }
			                           }, blankUid);
		}

		public void OnClickInventoryType3()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, -1, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, 0, 0, 0 }
			                           }, blankUid);
		}

		public void OnClickInventoryType4()
		{
			slotsManager.InitFromArray(new int[5, 5]
			                           {
				                           { 0, 0, -1, 0, 0 },
				                           { 0, 0, 0, 0, 0 },
				                           { -1, 0, 0, 0, -1 },
				                           { 0, 0, 0, 0, 0 },
				                           { 0, 0, -1, 0, 0 }
			                           }, blankUid);
		}
	}
}
