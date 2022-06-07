using System;
using System.IO;
using UnityEngine;

namespace flanne
{
		public static class SaveSystem
	{
				public static void Load()
		{
			string path = Application.persistentDataPath + "/gamedata.json";
			if (File.Exists(path))
			{
				SaveSystem.data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
				return;
			}
			SaveSystem.data = new SaveData();
		}

				public static void Save()
		{
			string contents = JsonUtility.ToJson(SaveSystem.data);
			File.WriteAllText(Application.persistentDataPath + "/gamedata.json", contents);
		}

				public static SaveData data;
	}
}
