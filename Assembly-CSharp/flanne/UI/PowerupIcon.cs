using System;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class PowerupIcon : DataUIBinding<Powerup>
	{
				public override void Refresh()
		{
			this.iconImage.sprite = base.data.icon;
		}

				[SerializeField]
		private Image iconImage;
	}
}
