using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.PreloadScene.Save
{
	/// <summary>
	/// 저장 파일을 관리합니다.
	/// </summary>
	public class SaveManager : SingleTon<SaveManager>
	{
		private DefineSaveData currentSaveData = null;
		private string filePath;

		private BinaryFormatter formatter = new BinaryFormatter();

		protected override void Awake()
		{
			base.Awake();

			filePath = Application.persistentDataPath + @"\DrawerHamster.dat";

			LoadFromFile();
		}
		
		public void Save(DefineSaveData data)
		{
			currentSaveData = data;

			SaveToFile();
		}
		
		private void SaveToFile()
		{
			FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			formatter.Serialize(stream, currentSaveData);
			stream.Close();

			LoadFromFile();
		}
		
		private void LoadFromFile()
		{
			if (!File.Exists(filePath)) return;

			FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
			currentSaveData = formatter.Deserialize(stream) as DefineSaveData;
			stream.Close();
		}

		public DefineSaveData GetSaveData() => currentSaveData;
	}
}
