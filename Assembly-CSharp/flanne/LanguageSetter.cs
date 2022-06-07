using System;
using UnityEngine;

namespace flanne
{
		public class LanguageSetter : MonoBehaviour
	{
				private void Awake()
		{
			LocalizationSystem.language = (LocalizationSystem.Language)PlayerPrefs.GetInt("Language", 0);
		}

				public void SetEN()
		{
			LocalizationSystem.language = LocalizationSystem.Language.English;
			PlayerPrefs.SetInt("Language", (int)LocalizationSystem.language);
			this.PostNotification(LanguageSetter.ChangedEvent);
		}

				public void SetJP()
		{
			LocalizationSystem.language = LocalizationSystem.Language.Japanese;
			PlayerPrefs.SetInt("Language", (int)LocalizationSystem.language);
			this.PostNotification(LanguageSetter.ChangedEvent);
		}

				public void SetCH()
		{
			LocalizationSystem.language = LocalizationSystem.Language.Chinese;
			PlayerPrefs.SetInt("Language", (int)LocalizationSystem.language);
			this.PostNotification(LanguageSetter.ChangedEvent);
		}

				public void SetBR()
		{
			LocalizationSystem.language = LocalizationSystem.Language.BrazilPortuguese;
			PlayerPrefs.SetInt("Language", (int)LocalizationSystem.language);
			this.PostNotification(LanguageSetter.ChangedEvent);
		}

				public static string ChangedEvent = "LanguageSetter.ChangedEvent";
	}
}
