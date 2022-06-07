using System;
using UnityEngine;

namespace flanne
{
		public class SeekerWeapon : WeaponSummon
	{
				protected override void Init()
		{
			base.summonAtkSpdMod.ChangedEvent += this.OnSummonAtkSpdChange;
			this.seeker.acceleration = base.summonAtkSpdMod.Modify(this.baseAcceleration);
		}

				private void OnDestroy()
		{
			base.summonAtkSpdMod.ChangedEvent -= this.OnSummonAtkSpdChange;
		}

				private void OnSummonAtkSpdChange(object sender, EventArgs e)
		{
			if (this.seeker != null)
			{
				this.seeker.acceleration = base.summonAtkSpdMod.Modify(this.baseAcceleration);
			}
		}

				[SerializeField]
		private SeekEnemy seeker;

				[SerializeField]
		private float baseAcceleration;
	}
}
