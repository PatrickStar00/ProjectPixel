using System;

namespace flanne.UI
{
		public class PowerupProperties : IUIProperties
	{
				public PowerupProperties(Powerup p)
		{
			this.powerup = p;
		}

				public Powerup powerup;
	}
}
