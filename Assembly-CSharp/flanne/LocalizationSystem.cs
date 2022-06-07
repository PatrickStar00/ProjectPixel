using System;
using System.Collections.Generic;

namespace flanne
{
		public class LocalizationSystem
	{
				public static void Init()
		{
			LocalizationSystem.csvLoader = new CSVLoader();
			LocalizationSystem.csvLoader.LoadCSV();
			LocalizationSystem.UpdateDictionaries();
			LocalizationSystem.isInit = true;
		}

				public static void UpdateDictionaries()
		{
			LocalizationSystem.localizedEN = LocalizationSystem.csvLoader.GetDictionaryValues("en");
			LocalizationSystem.localizedJP = LocalizationSystem.csvLoader.GetDictionaryValues("jp");
			LocalizationSystem.localizedCH = LocalizationSystem.csvLoader.GetDictionaryValues("ch");
			LocalizationSystem.localizedBR = LocalizationSystem.csvLoader.GetDictionaryValues("br");
		}

				public static Dictionary<string, string> GetDictionaryForEditor()
		{
			if (!LocalizationSystem.isInit)
			{
				LocalizationSystem.Init();
			}
			return LocalizationSystem.localizedEN;
		}

				public static string GetLocalizedValue(string key)
		{
			if (!LocalizationSystem.isInit)
			{
				LocalizationSystem.Init();
			}
			string result = key;
			switch (LocalizationSystem.language)
			{
			case LocalizationSystem.Language.English:
				LocalizationSystem.localizedEN.TryGetValue(key, out result);
				break;
			case LocalizationSystem.Language.Japanese:
				LocalizationSystem.localizedJP.TryGetValue(key, out result);
				break;
			case LocalizationSystem.Language.Chinese:
				LocalizationSystem.localizedCH.TryGetValue(key, out result);
				break;
			case LocalizationSystem.Language.BrazilPortuguese:
				LocalizationSystem.localizedBR.TryGetValue(key, out result);
				break;
			}
			return result;
		}

				public static LocalizationSystem.Language language;

				private static Dictionary<string, string> localizedEN;

				private static Dictionary<string, string> localizedJP;

				private static Dictionary<string, string> localizedCH;

				private static Dictionary<string, string> localizedBR;

				public static bool isInit;

				public static CSVLoader csvLoader;

				public enum Language
		{
						English,
						Japanese,
						Chinese,
						BrazilPortuguese
		}
	}
}
