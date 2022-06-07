using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "ElementStatPowerup", menuName = "Powerups/ElementStatPowerup")]
	public class ElementStatPowerup : StatPowerup
	{
				protected override void Apply(GameObject target)
		{
			BurnSystem.SharedInstance.burnDamageMultiplier.Increase(this.burnDamageMulti);
			ThunderGenerator.SharedInstance.damageMod.AddMultiplierBonus(this.burnDamageMulti);
			FreezeSystem.SharedInstance.durationMod.AddMultiplierBonus(this.burnDamageMulti);
		}

				[SerializeField]
		private float burnDamageMulti;

				[SerializeField]
		private float thunderDamageMulti;

				[SerializeField]
		private float freezeDurationMutli;
	}
}
