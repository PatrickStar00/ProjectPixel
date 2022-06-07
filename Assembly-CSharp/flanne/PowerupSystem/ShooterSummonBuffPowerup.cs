using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "ShooterSummonBuffPowerup", menuName = "Powerups/ShooterSummonBuffPowerup")]
	public class ShooterSummonBuffPowerup : StatPowerup
	{
				protected override void Apply(GameObject target)
		{
			base.Apply(target);
			foreach (ShootingSummon shootingSummon in target.GetComponentsInChildren<ShootingSummon>())
			{
				if (shootingSummon.SummonTypeID == this.SummonTypeID)
				{
					if (this.targetMouse)
					{
						shootingSummon.targetMouse = true;
					}
					shootingSummon.numProjectiles += this.projectileMod;
				}
			}
		}

				[SerializeField]
		private string SummonTypeID;

				[SerializeField]
		private bool targetMouse;

				[SerializeField]
		private int projectileMod;
	}
}
