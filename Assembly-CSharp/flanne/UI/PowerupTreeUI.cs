using System;
using UnityEngine;

namespace flanne.UI
{
		public class PowerupTreeUI : DataUIBinding<PowerupTreeUIData>
	{
				public override void Refresh()
		{
			this.startingPowerup.data = base.data.startingPowerup;
			this.leftPowerup.data = base.data.leftPowerup;
			this.rightPowerup.data = base.data.rightPowerup;
			this.finalPowerup.data = base.data.finalPowerup;
		}

				[SerializeField]
		private PowerupIcon startingPowerup;

				[SerializeField]
		private PowerupIcon leftPowerup;

				[SerializeField]
		private PowerupIcon rightPowerup;

				[SerializeField]
		private PowerupIcon finalPowerup;
	}
}
