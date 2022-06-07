using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class CharacterDescription : DataUIBinding<CharacterData>
	{
				public override void Refresh()
		{
			this.portrait.sprite = base.data.portrait;
			this.nameTMP.text = base.data.nameString;
			this.healthTMP.text = base.data.startHP.ToString();
			this.descriptionTMP.text = base.data.description;
		}

				[SerializeField]
		private Image portrait;

				[SerializeField]
		private TMP_Text nameTMP;

				[SerializeField]
		private TMP_Text healthTMP;

				[SerializeField]
		private TMP_Text descriptionTMP;
	}
}
