using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class PowerupWidget : Widget<PowerupProperties>
	{
				public override void SetProperties(PowerupProperties properties)
		{
			Powerup powerup = properties.powerup;
			if (this.icon != null)
			{
				this.icon.sprite = powerup.icon;
			}
			if (this.nameTMP != null)
			{
				this.nameTMP.text = powerup.nameString;
			}
			if (this.descriptionTMP != null)
			{
				this.descriptionTMP.text = powerup.description;
			}
		}

				[SerializeField]
		private Image icon;

				[SerializeField]
		private TMP_Text nameTMP;

				[SerializeField]
		private TMP_Text descriptionTMP;
	}
}
