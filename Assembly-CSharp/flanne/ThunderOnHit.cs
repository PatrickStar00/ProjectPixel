using System;
using UnityEngine;

namespace flanne
{
		public class ThunderOnHit : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
			this.TGen = ThunderGenerator.SharedInstance;
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
		}

				private void OnImpact(object sender, object args)
		{
			if (Random.Range(0f, 1f) < this.chanceToHit)
			{
				this.TGen.GenerateAt(args as GameObject, this.baseDamage);
			}
		}

				[Range(0f, 1f)]
		public float chanceToHit;

				public int baseDamage;

				private ThunderGenerator TGen;
	}
}
