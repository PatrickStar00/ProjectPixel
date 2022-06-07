using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		[RequireComponent(typeof(TMP_Text))]
	public class TextLocalizerUI : MonoBehaviour
	{
				private void OnLanguageChanged(object sender, object args)
		{
			this.tmp.text = LocalizationSystem.GetLocalizedValue(this.localizedString.key);
		}

				private void Start()
		{
			this.tmp = base.GetComponent<TMP_Text>();
			this.tmp.text = LocalizationSystem.GetLocalizedValue(this.localizedString.key);
			this.AddObserver(new Action<object, object>(this.OnLanguageChanged), LanguageSetter.ChangedEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnLanguageChanged), LanguageSetter.ChangedEvent);
		}

				private TMP_Text tmp;

				[SerializeField]
		private LocalizedString localizedString;
	}
}
