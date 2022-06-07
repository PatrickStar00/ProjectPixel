using System;
using UnityEngine;

namespace flanne
{
		public class OrbitalWeapon : WeaponSummon
	{
				protected override void Init()
		{
			base.summonAtkSpdMod.ChangedEvent += this.OnSummonAtkSpdChange;
			this.orbital.rotationSpeed = base.summonAtkSpdMod.Modify(this.baseRotationSpeed);
		}

				private void OnDestroy()
		{
			base.summonAtkSpdMod.ChangedEvent -= this.OnSummonAtkSpdChange;
		}

				private void OnSummonAtkSpdChange(object sender, EventArgs e)
		{
			if (this.orbital != null)
			{
				this.orbital.rotationSpeed = base.summonAtkSpdMod.Modify(this.baseRotationSpeed);
			}
		}

				[SerializeField]
		private Orbital orbital;

				[SerializeField]
		private float baseRotationSpeed;
	}
}
