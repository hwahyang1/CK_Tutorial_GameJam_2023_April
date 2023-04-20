using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Cysharp.Threading.Tasks;

using CK_Tutorial_GameJam_April.StageScene.Boss;
using CK_Tutorial_GameJam_April.StageScene.Items;
using CK_Tutorial_GameJam_April.PreloadScene.Save;
using CK_Tutorial_GameJam_April.StageScene.Inventory;
using CK_Tutorial_GameJam_April.StageScene.Character;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene.Save
{
	/// <summary>
	/// Description
	/// </summary>
	public class GameSaveData : SingleTon<GameSaveData>
	{
		private DefineSaveData saveData;
		public DefineSaveData SaveData => saveData;

		private Image savingSpinner;

		[SerializeField]
		private float animationTime = 0.5f;
		[SerializeField]
		private float animationDelay = 0.05f;

		private bool saving = false;
		
		protected override void Awake()
		{
			base.Awake();
			destroyCondition = () => SceneManager.GetActiveScene().name != "StageScene" &&
			                         SceneManager.GetActiveScene().name != "MenuScene";
			saveData = SaveManager.Instance.GetSaveData();
			savingSpinner = GameObject.FindGameObjectWithTag("SavingSpinner").GetComponent<Image>();
		}

		public void ExportData(Action callback = null)
		{
			if (saving) return;
			ExportDataTask(callback).Forget();
		}

		private async UniTaskVoid ExportDataTask(Action callback)
		{
			saving = true;
			
			//savingSpinner.gameObject.SetActive(true);
			for (float i = 0f; i <= animationTime; )
			{
				savingSpinner.color = new Color(1f, 1f, 1f, i / animationTime);
				await UniTask.Delay(TimeSpan.FromSeconds(animationDelay));
				i += animationDelay;
			}
			savingSpinner.color = new Color(1f, 1f, 1f, 1f);
			
			await UniTask.Delay(TimeSpan.FromSeconds(0.25f));

			// 없을 경우 생성
			saveData ??= new DefineSaveData();

			Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
			saveData.playerPosition = new Tuple<float, float>(playerPosition.x, playerPosition.y);

			int playerKeys = GameObject.FindAnyObjectByType<PlayerAdditional>().CurrentKeysCount;
			saveData.playerKeys = playerKeys;

			LevelManager levelManager = GameObject.FindAnyObjectByType<LevelManager>();
			saveData.playerStamina = levelManager.Stamina;
			saveData.playerLevel = levelManager.Level;
			saveData.playerExp = levelManager.Exp;

			BossManager bossManager = GameObject.FindAnyObjectByType<BossManager>();
			saveData.bossTime = bossManager.ElapsedTime;

			ItemSpawnManager itemSpawnManager = GameObject.FindAnyObjectByType<ItemSpawnManager>();
			saveData.droppedItems = itemSpawnManager.ExportAllItems();
			
			Tuple<int[][], int[][]> playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory")
			                                                    .GetComponent<SlotsManager>().ExportAllTilesIdsUids();
			saveData.playerInventory = playerInventory;

			GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
			if (saveData.npcInventory == null)
				saveData.npcInventory = new Dictionary<string, Tuple<DefineNpcFlow, int[][], int[][]>>();
			else
				saveData.npcInventory.Clear();
			foreach (GameObject npc in npcs)
			{
				Npc current = npc.GetComponent<Npc>();
				string name = npc.gameObject.name;
				DefineNpcFlow flow = current.CurrentFlow;
				Tuple<int[][], int[][]> inventory = current.Backup;
				
				saveData.npcInventory.Add(name, new Tuple<DefineNpcFlow, int[][], int[][]>(flow, inventory.Item1, inventory.Item2));
			}
			
			SaveManager.Instance.Save(saveData);
			
			await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
			
			for (float i = animationTime; i >= 0f; )
			{
				savingSpinner.color = new Color(1f, 1f, 1f, i / animationTime);
				await UniTask.Delay(TimeSpan.FromSeconds(animationDelay));
				i -= animationDelay;
			}
			savingSpinner.color = new Color(1f, 1f, 1f, 0f);
			//savingSpinner.gameObject.SetActive(false);

			saving = false;
			callback?.Invoke();
		}
		
		public void ResetData()
		{
			saveData = null;
		}

		public void Exit()
		{
			Destroy(gameObject);
		}
	}
}
