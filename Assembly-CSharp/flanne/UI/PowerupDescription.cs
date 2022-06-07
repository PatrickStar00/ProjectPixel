using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class PowerupDescription : DataUIBinding<Powerup>
	{
				public override void Refresh()
		{
			this.nameTMP.text = base.data.nameString;
			this.descriptionTMP.text = base.data.description;
			if (this.powerupTreeUI != null)
			{
				if (base.data.powerupTreeUIData != null)
				{
					this.powerupTreeUI.gameObject.SetActive(true);
					this.powerupTreeUI.data = base.data.powerupTreeUIData;
					return;
				}
				this.powerupTreeUI.gameObject.SetActive(false);
			}
		}

				[SerializeField]
		private TMP_Text nameTMP;

				[SerializeField]
		private TMP_Text descriptionTMP;

				[SerializeField]
		private PowerupTreeUI powerupTreeUI;
	}
}
