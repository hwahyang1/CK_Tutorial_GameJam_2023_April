using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

using CK_Tutorial_GameJam_April.StageScene.UI;
using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.StageScene.Inventory;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene
{
	/// <summary>
	/// 조력자를 관리합니다.
	/// </summary>
	public class Npc : MonoBehaviour
	{
		[SerializeField]
		private List<Sprite> sprites = new List<Sprite>();

		[SerializeField]
		private float spritesTime = 0.25f;

		private int currentSpriteIndex = 0;
		private float currentTime = 0f;
		
		[Header("Scripts")]
		[SerializeField]
		private SlotsManager slotsManager;

		[SerializeField]
		private MessageManager messageManager;

		[SerializeField]
		private PlayerAdditional playerAdditional;

		[SerializeField]
		private NpcAdditional npcAdditional;

		[SerializeField]
		private ItemEarn itemEarn;

		[Header("Npc Data")]
		[Tooltip("0과 -1만 사용합니다. 공백으로 구분합니다.")]
		public List<string> slotSize;

		[SerializeField]
		private string name;

		[SerializeField]
		[Tooltip("줄바꿈은 \\n로 입력합니다.")]
		private string description;

		[SerializeField]
		private Sprite npcSprite;

		[SerializeField]
		[Tooltip("줄바꿈은 \\n로 입력합니다.")]
		private List<string> greetingMessages;

		[SerializeField]
		[Tooltip("줄바꿈은 \\n로 입력합니다.")]
		private List<string> thanksMessages;

		private Tuple<int[][], int[][]> backup;
		public Tuple<int[][], int[][]> Backup => backup;

		private DefineNpcFlow currentFlow = DefineNpcFlow.Greeting;
		public DefineNpcFlow CurrentFlow => currentFlow;

		private SpriteRenderer spriteRenderer;

		private void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			currentSpriteIndex = Random.Range(0, sprites.Count);
			
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

		private void Start()
		{
			DefineSaveData data = GameSaveData.Instance.SaveData;
			if (data == null) return;
			if (!data.npcInventory.ContainsKey(gameObject.name)) return;
			Tuple<DefineNpcFlow, int[][], int[][]> previousInventory = data.npcInventory[gameObject.name];

			SetStatus(previousInventory.Item1 != DefineNpcFlow.Ended, previousInventory.Item1);
			backup = new Tuple<int[][], int[][]>(previousInventory.Item2, previousInventory.Item3);
		}

		private void Update()
		{
			if (currentTime >= spritesTime)
			{
				currentSpriteIndex++;
				if (currentSpriteIndex >= sprites.Count) currentSpriteIndex = 0;
				spriteRenderer.sprite = sprites[currentSpriteIndex];
				currentTime = 0f;
			}
			currentTime += Time.deltaTime;
			
			if (currentFlow != DefineNpcFlow.Inventory || slotsManager.current != gameObject.name || !slotsManager.IsActive) return;

			Tuple<int[][], int[][]> data = slotsManager.ExportAllTilesIdsUids();

			foreach (int[] currentH in data.Item1)
			{
				foreach (int currentV in currentH)
				{
					if (currentV == 0) return;
				}
			}
			
			// 여기까지 왔으면 다 채운거임
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
					messageManager.Show(name, greetingMessages, npcSprite, () =>
					                                            {
						                                            SetStatus(true, DefineNpcFlow.Inventory);
						                                            npcAdditional.Set(name, description, npcSprite);
						                                            OpenInventory();
					                                            });
					break;
				case DefineNpcFlow.Inventory:
					npcAdditional.Set(name, description, npcSprite);
					OpenInventory();
					break;
				case DefineNpcFlow.Thanks:
					messageManager.Show(name, thanksMessages, npcSprite, () =>
					                                          {
						                                          GameManager.Instance.status = GameStatus.ItemEarn;
						                                          playerAdditional.SetKeyCount(playerAdditional.CurrentKeysCount+1);
						                                          itemEarn.Show();
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

			slotsManager.current = gameObject.name;

			slotsManager.InitFromArray(backup.Item1, backup.Item2);
			slotsManager.SetTabActive(true);
		}

		public void CloseInventory()
		{
			if (!slotsManager.IsActive) return;

			slotsManager.current = "";
			
			backup = slotsManager.ExportAllTilesIdsUids();
			slotsManager.SetTabActive(false);
		}
	}
}
