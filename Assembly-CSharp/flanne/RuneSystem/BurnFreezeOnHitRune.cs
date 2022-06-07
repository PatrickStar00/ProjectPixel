using System;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class BurnFreezeOnHitRune : Rune
	{
				protected override void Init()
		{
			this.AddObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
			this.BurnSys = BurnSystem.SharedInstance;
			this.FreezeSys = FreezeSystem.SharedInstance;
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
		}

				private void OnImpact(object sender, object args)
		{
			if ((sender as MonoBehaviour).gameObject.tag == "Bullet")
			{
				GameObject gameObject = args as GameObject;
				if (gameObject.tag.Contains("Enemy"))
				{
					if (Random.Range(0f, 1f) < this.chancePerLevel * (float)this.level)
					{
						this.BurnSys.Burn(gameObject, this.burnDamge);
					}
					if (Random.Range(0f, 1f) < this.chancePerLevel * (float)this.level)
					{
						this.FreezeSys.Freeze(gameObject, this.freezeDuration);
					}
				}
			}
		}

				[Range(0f, 1f)]
		public float chancePerLevel;

				public int burnDamge;

				public float freezeDuration;

				private BurnSystem BurnSys;

				private FreezeSystem FreezeSys;
	}
}
