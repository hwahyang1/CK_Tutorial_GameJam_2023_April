using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.StageScene.Inventory.Slots;

namespace CK_Tutorial_GameJam_April.StageScene.Character
{
	/// <summary>
	/// 플레이어의 레벨과 스테미너를 관리합니다.
	/// </summary>
	public class LevelManager : MonoBehaviour
	{
		[SerializeField]
		private SlotsManager slotsManager;

		private int level = 0;

		public int Level
		{
			get => level;
			set
			{
				if (value < 0) level = 0;
				else if (5 < value) level = 5;
				else level = value;
			}
		}

		[Header("Stamina")]
		[SerializeField]
		private float staminaRecover;

		[SerializeField]
		private List<float> maxStamina = new List<float>();

		public float MaxStamina => maxStamina[level];

		private float stamina;

		public float Stamina
		{
			get => stamina;
			set
			{
				if (value < 0) stamina = 0;
				else if (maxStamina[level] < value) stamina = maxStamina[level];
				else stamina = value;
			}
		}

		[Header("Exp")]
		[SerializeField]
		private List<int> maxExp = new List<int>();

		public int MaxExp => maxExp[level];

		private int exp;

		public int Exp
		{
			get => exp;
			set
			{
				if (value < 0) exp = 0;
				else if (maxExp[level] < value) exp = maxExp[level];
				else exp = value;
			}
		}

		[HideInInspector]
		public bool isPlayerStay;

		private readonly float[] deltaTime = new float[2]; // 초반 3초, 후반 1초마다 회복

		private void Awake()
		{
			Level = 0;
			Stamina = maxStamina[Level];
			Exp = 0;

			int[][] inventory = new int[][]
			                    {
				                    new int[] { -1, -1, -1, -1, -1 },
				                    new int[] { -1, 0, 0, 0, -1 },
				                    new int[] { -1, 0, 0, 0, -1 },
				                    new int[] { -1, 0, 0, 0, -1 },
				                    new int[] { -1, -1, -1, -1, -1 },
				                    new int[] { -1, -1, -1, -1, -1 },
			                    };
			int[][] uid = new int[][]
			              {
				              new int[] { 0, 0, 0, 0, 0 },
				              new int[] { 0, 0, 0, 0, 0 },
				              new int[] { 0, 0, 0, 0, 0 },
				              new int[] { 0, 0, 0, 0, 0 },
				              new int[] { 0, 0, 0, 0, 0 },
				              new int[] { 0, 0, 0, 0, 0 },
			              };
			slotsManager.InitFromArray(inventory, uid);
		}

		private void Start()
		{
			DefineSaveData data = GameSaveData.Instance.SaveData;
			if (data == null) return;

			Stamina = data.playerStamina;
			Level = data.playerLevel;
			Exp = data.playerExp;
		}

		private void Update()
		{
			// exp가 최대가 되면 플레이어 레벨업
			if (Exp >= maxExp[Level])
			{
				Exp = 0;
				Level++;
				Stamina = maxStamina[Level];
				ChangeInventory();
			}

			// 플레이어가 3초간 가만히 있으면 -> 1초마다 특정 스태미너 회복
			if (isPlayerStay)
			{
				if (deltaTime[0] < 3f)
				{
					deltaTime[0] += Time.deltaTime;
				}
				else
				{
					if (deltaTime[1] < 1f)
					{
						deltaTime[1] += Time.deltaTime;
					}
					else
					{
						Stamina += staminaRecover;
						deltaTime[1] = 0f;
					}
				}
			}
			else
			{
				deltaTime[0] = 0f;
				deltaTime[1] = 0f;
			}
		}

		/// <summary>
		/// 인벤토리를 현재 레벨에 맞게 변경합니다.
		/// </summary>
		/// <param name="preservePast">이전 인벤토리에 있던 아이템을 유지 할 지 결정합니다.</param>
		private void ChangeInventory(bool preservePast = true)
		{
			int[][] inventory;

			switch (Level)
			{
				case 0:
					inventory = new int[][]
					            {
						            new int[] { -1, -1, -1, -1, -1 },
						            new int[] { -1, 0, 0, 0, -1 },
						            new int[] { -1, 0, 0, 0, -1 },
						            new int[] { -1, 0, 0, 0, -1 },
						            new int[] { -1, -1, -1, -1, -1 },
						            new int[] { -1, -1, -1, -1, -1 },
					            };
					break;
				case 1:
					inventory = new int[][]
					            {
						            new int[] { -1, -1, -1, -1, -1 },
						            new int[] { -1, 0, 0, 0, -1 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, -1 },
						            new int[] { -1, -1, -1, -1, -1 }
					            };
					break;
				case 2:
					inventory = new int[][]
					            {
						            new int[] { -1, 0, 0, -1, -1 },
						            new int[] { -1, 0, 0, 0, -1 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, -1, -1, -1, -1 }
					            };
					break;
				case 3:
					inventory = new int[][]
					            {
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, -1, -1, -1, -1 }
					            };
					break;
				case 4:
					inventory = new int[][]
					            {
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { -1, 0, 0, 0, 0 },
						            new int[] { -1, -1, 0, 0, 0 }
					            };
					break;
				case 5:
					inventory = new int[][]
					            {
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 },
						            new int[] { 0, 0, 0, 0, 0 }
					            };
					break;
				default:
					inventory = new int[][]
					            {
						            new int[] { }
					            };
					break;
			}

			Tuple<int[][], int[][]> pastData = slotsManager.ExportAllTilesIdsUids();

			int[][] ids = new int[pastData.Item1.Length][];
			for (int i = 0; i < ids.Length; i++)
			{
				ids[i] = new int[pastData.Item1[0].Length];
				for (int j = 0; j < ids[i].Length; j++)
				{
					ids[i][j] = pastData.Item1[i][j] == -1 ? inventory[i][j] : pastData.Item1[i][j];
				}
			}

			slotsManager.InitFromArray(ids, pastData.Item2);
		}
	}
}
