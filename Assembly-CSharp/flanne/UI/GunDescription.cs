using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class GunDescription : DataUIBinding<GunData>
	{
				public override void Refresh()
		{
			this.nameTMP.text = base.data.nameString;
			this.descriptionTMP.text = base.data.description;
			this.damageTMP.text = base.data.damage.ToString("00");
			this.fireRateTMP.text = (1f / base.data.shotCooldown).ToString("0.0");
			this.projectilesTMP.text = base.data.numOfProjectiles.ToString("00");
			this.ammoTMP.text = base.data.maxAmmo.ToString("00");
			this.reloadTimeTMP.text = base.data.reloadDuration.ToString("0.0");
		}

				[SerializeField]
		private TMP_Text nameTMP;

				[SerializeField]
		private TMP_Text descriptionTMP;

				[SerializeField]
		private TMP_Text damageTMP;

				[SerializeField]
		private TMP_Text fireRateTMP;

				[SerializeField]
		private TMP_Text projectilesTMP;

				[SerializeField]
		private TMP_Text ammoTMP;

				[SerializeField]
		private TMP_Text reloadTimeTMP;
	}
}
