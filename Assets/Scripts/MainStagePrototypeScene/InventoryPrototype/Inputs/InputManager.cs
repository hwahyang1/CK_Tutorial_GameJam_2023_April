using System.Collections;
using System.Collections.Generic;

using UnityEngine;
	
using CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Slots;

namespace CK_Tutorial_GameJam_April.MainStagePrototypeScene.InventoryPrototype.Inputs
{
	/// <summary>
	/// 키 입력을 관리합니다.
	/// </summary>
	public class InputManager : MonoBehaviour
	{
		private SlotsManager slotsManager;
		private CharacterManager characterManager;

		private void Start()
		{
			slotsManager = GetComponent<SlotsManager>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				slotsManager.SetTabActive(!slotsManager.IsActive);
			}
			
		}
	}
}
