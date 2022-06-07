using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "ThunderDamageUp", menuName = "Powerups/ThunderDamageUp")]
	public class ThunderDamageUp : StatPowerup
	{
				protected override void Apply(GameObject target)
		{
			base.Apply(target);
			ThunderGenerator.SharedInstance.damageMod.AddMultiplierBonus(this.thunderDamageMulti);
			ThunderGenerator.SharedInstance.sizeMultiplier *= 1.75f;
		}

				[SerializeField]
		private float thunderDamageMulti;
	}
}
