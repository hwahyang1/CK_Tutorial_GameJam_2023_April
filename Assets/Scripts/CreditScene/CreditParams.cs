using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.CreditScene
{
	/// <summary>
	/// Description
	/// </summary>
	public class CreditParams : SingleTon<CreditParams>
	{
		public bool isControllable = true;

		public void Exit()
		{
			Destroy(gameObject);
		}
	}
}
