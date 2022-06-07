using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "VisionPowerup", menuName = "Powerups/VisionPowerup")]
	public class VisionPowerup : StatPowerup
	{
				protected override void Apply(GameObject target)
		{
			base.Apply(target);
			AttachVisionDamage componentInChildren = target.GetComponentInChildren<AttachVisionDamage>();
			if (this.setOnHitTrigger)
			{
				componentInChildren.visionDamage.triggerOnHit = true;
			}
			componentInChildren.visionDamage.damageAmount = Mathf.FloorToInt((float)componentInChildren.visionDamage.damageAmount * (1f + this.visionDamageMultiplierBonus));
			componentInChildren.visionDamage.secondsPerTick /= 1f + this.visionTickSpeedMultiplierBonus;
		}

				public float visionDamageMultiplierBonus;

				public float visionTickSpeedMultiplierBonus;

				public bool setOnHitTrigger;
	}
}
