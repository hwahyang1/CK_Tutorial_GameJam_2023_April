using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.StageScene.UI;
using CK_Tutorial_GameJam_April.StageScene.Inventory;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene
{
	/// <summary>
	/// 조력자를 관리합니다.
	/// </summary>
	public class Npc : MonoBehaviour
	{
		[Header("Scripts")]
		[SerializeField]
		private SlotsManager slotsManager;

		[SerializeField]
		private MessageManager messageManager;

		[SerializeField]
		private NpcAdditional npcAdditional;
		
		[Header("Npc Data")]
		[Tooltip("0과 -1만 사용합니다. 공백으로 구분합니다.")]
		public List<string> slotSize;

		[SerializeField]
		private string name;
		[SerializeField]
		[Tooltip("줄바꿈은 \\n로 입력합니다.")]
		private string description;
		[SerializeField]
		[Tooltip("줄바꿈은 \\n로 입력합니다.")]
		private List<string> greetingMessages;
		[SerializeField]
		[Tooltip("줄바꿈은 \\n로 입력합니다.")]
		private List<string> thanksMessages;

		private Tuple<int[][], int[][]> backup;

		private DefineNpcFlow currentFlow = DefineNpcFlow.Greeting;

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
					uidData[i][j] = currentData[j] == "0" ? 0 : -1;
					slotData[i][j] = int.Parse(currentData[j]);
				}
			}

			backup = new Tuple<int[][], int[][]>(slotData, uidData);
		}

		private void Update()
		{
			if (currentFlow != DefineNpcFlow.Inventory) return;
			
			Tuple<int[][], int[][]> data = slotsManager.ExportAllTilesIdsUids();

			foreach (int[] currentH in data.Item2)
			{
				foreach (int currentV in currentH)
				{
					if (currentV == 0) return;
				}
			}

			backup = slotsManager.ExportAllTilesIdsUids();
			slotsManager.SetTabActive(false);
			currentFlow = DefineNpcFlow.Thanks;
			Interaction();
		}

		public void Interaction()
		{
			switch (currentFlow)
			{
				case DefineNpcFlow.Greeting:
					messageManager.Show(name, greetingMessages, () =>
					                                            {
						                                            SetStatus(true, DefineNpcFlow.Inventory);
						                                            npcAdditional.Set(name, description);
						                                            OpenInventory();
					                                            });
					break;
				case DefineNpcFlow.Inventory:
					npcAdditional.Set(name, description);
					OpenInventory();
					break;
				case DefineNpcFlow.Thanks:
					messageManager.Show(name, thanksMessages, () =>
					                                          {
						                                          // TODO: 열쇠 지급 필요
						                                          SetStatus(false, DefineNpcFlow.Ended);
					                                          });
					break;
				case DefineNpcFlow.Ended:
					break;
			}
		}

		public void SetStatus(bool visible, DefineNpcFlow flow)
		{
			currentFlow = flow;
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, visible ? 1f : 0f);
		}

		private void OpenInventory()
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
