using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.PreloadScene.Item;

namespace CK_Tutorial_GameJam_April.StageScene.Items
{
	/// <summary>
	/// 아이템의 스폰을 관리합니다.
	/// </summary>
	public class ItemSpawnManager : MonoBehaviour
	{
		[SerializeField]
		private string spawnPositionTag;

		[SerializeField]
		private Transform itemParent;

		[SerializeField]
		private GameObject itemPrefab;

		[Tooltip("아이템 등급 순서로 입력합니다.")]
		[SerializeField]
		private List<Color> effectColors = new List<Color>();

		[SerializeField]
		private List<Item> positions = new List<Item>();

		private void Awake()
		{
			RefreshPositions();
			RespawnItem();
		}

		private void Start()
		{
			DefineSaveData data = GameSaveData.Instance.SaveData;
			if (data == null) return;

			CustomSpawnItem(data.droppedItems);
		}

		/// <summary>
		/// 소환가능 위치를 다시 불러옵니다.
		/// </summary>
		public void RefreshPositions()
		{
			foreach (Item position in positions) Destroy(position.gameObject);
			positions.Clear();

			GameObject[] spawnPositions = GameObject.FindGameObjectsWithTag(spawnPositionTag);
			foreach (GameObject position in spawnPositions)
			{
				GameObject child = Instantiate(itemPrefab, position.transform.position, Quaternion.identity,
				                               itemParent);
				Item item = child.GetComponent<Item>();
				positions.Add(item);
				child.SetActive(false);
			}
		}

		public void CustomSpawnItem(List<Tuple<int, bool>> data)
		{
			List<DefineItem> items = ItemStorage.Instance.GetItems();
			for (int i = 0; i < positions.Count; i++)
			{
				Item position = positions[i];
				position.gameObject.SetActive(true);
				position.SetItem(data[i].Item1, effectColors[(int)items[data[i].Item1].rank]);
				position.ChangeProgressBar(false, 0f);
				position.gameObject.SetActive(data[i].Item2);
			}
		}

		public List<Tuple<int, bool>> ExportAllItems()
		{
			List<Tuple<int, bool>> data = new List<Tuple<int, bool>>();
			
			if (positions.Count == 0) return data;

			foreach (Item item in positions)
			{
				data.Add(new Tuple<int, bool>(item.ItemId, item.gameObject.activeInHierarchy));
			}
			
			return data;
		}

		/// <summary>
		/// 아이템을 전부 재생성 합니다.
		/// </summary>
		public void RespawnItem()
		{
			if (positions.Count == 0) return;

			foreach (Item position in positions) position.gameObject.SetActive(true);

			List<Item> availablePositions = new List<Item>(positions);

			/* 기획서 보니까 하드코딩이 빠르게 생김 */

			// 전설 2개 (고구마(9) 2개)
			for (int i = 0; i < 2; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(9, effectColors[2]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			// 레어 8개 (당근(6) 3개, 귤(7) 3개, 딸기(8) 2개)
			for (int i = 0; i < 3; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(6, effectColors[1]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			for (int i = 0; i < 3; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(7, effectColors[1]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			for (int i = 0; i < 2; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(8, effectColors[1]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			// 일반 8개 (아몬드(1) 4개, 해바라기씨(2) 4개, 캐슈넛(3) 4개, 호두(4) 4개, 샐러리(5) 4개)
			for (int i = 0; i < 4; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(1, effectColors[0]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			for (int i = 0; i < 4; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(2, effectColors[0]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			for (int i = 0; i < 4; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(3, effectColors[0]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			for (int i = 0; i < 4; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(4, effectColors[0]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}

			for (int i = 0; i < 4; i++)
			{
				Item current = availablePositions[Random.Range(0, availablePositions.Count)];
				current.SetItem(5, effectColors[0]);
				current.ChangeProgressBar(false, 0f);
				availablePositions.Remove(current);
			}
		}
	}
}
