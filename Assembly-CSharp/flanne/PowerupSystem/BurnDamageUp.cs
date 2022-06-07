using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "BurnDamageUp", menuName = "Powerups/BurnDamageUp")]
	public class BurnDamageUp : Powerup
	{
				protected override void Apply(GameObject target)
		{
			BurnSystem.SharedInstance.burnDamageMultiplier.Increase(this.burnDamageMulti);
		}

				[SerializeField]
		private float burnDamageMulti;
	}
}
