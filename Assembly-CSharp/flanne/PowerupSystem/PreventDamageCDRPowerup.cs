using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "PreventDamageCDRPowerup", menuName = "Powerups/PreventDamageCDRPowerup", order = 1)]
	public class PreventDamageCDRPowerup : Powerup
	{
				protected override void Apply(GameObject target)
		{
			target.GetComponentInChildren<PreventDamage>().cooldownTime -= this.cdr;
		}

				[SerializeField]
		private float cdr;
	}
}
