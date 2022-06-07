using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "TripleBaseAmmoPowerup", menuName = "Powerups/TripleBaseAmmoPowerup")]
	public class TripleBaseAmmoPowerup : StatPowerup
	{
				protected override void Apply(GameObject target)
		{
			base.Apply(target);
			PlayerController componentInChildren = target.GetComponentInChildren<PlayerController>();
			componentInChildren.stats[StatType.MaxAmmo].AddFlatBonus(2 * componentInChildren.gun.gunData.maxAmmo);
		}
	}
}
