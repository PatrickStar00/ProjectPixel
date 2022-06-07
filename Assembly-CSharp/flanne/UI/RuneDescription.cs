using System;
using flanne.RuneSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class RuneDescription : DataUIBinding<RuneData>
	{
				public override void Refresh()
		{
			this.iconImage.sprite = base.data.icon;
			this.costTMP.text = base.data.costPerLevel.ToString();
			this.nameTMP.text = base.data.nameString;
			this.descriptionTMP.text = base.data.description;
		}

				[SerializeField]
		private Image iconImage;

				[SerializeField]
		private TMP_Text costTMP;

				[SerializeField]
		private TMP_Text nameTMP;

				[SerializeField]
		private TMP_Text descriptionTMP;
	}
}
