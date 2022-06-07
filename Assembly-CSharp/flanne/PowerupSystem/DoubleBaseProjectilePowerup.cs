using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "DoubleBaseProjectilePowerup", menuName = "Powerups/DoubleBaseProjectilePowerup", order = 1)]
	public class DoubleBaseProjectilePowerup : StatPowerup
	{
				protected override void Apply(GameObject target)
		{
			base.Apply(target);
			PlayerController componentInChildren = target.GetComponentInChildren<PlayerController>();
			componentInChildren.stats[StatType.Projectiles].AddFlatBonus(componentInChildren.gun.gunData.numOfProjectiles);
		}
	}
}
