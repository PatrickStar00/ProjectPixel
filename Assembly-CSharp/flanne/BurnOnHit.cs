using System;
using UnityEngine;

namespace flanne
{
		public class BurnOnHit : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
			this.BurnSys = BurnSystem.SharedInstance;
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
		}

				private void OnImpact(object sender, object args)
		{
			if (Random.Range(0f, 1f) < this.chanceToHit && (sender as MonoBehaviour).gameObject.tag == "Bullet")
			{
				GameObject gameObject = args as GameObject;
				if (gameObject.tag.Contains("Enemy"))
				{
					this.BurnSys.Burn(gameObject, this.burnDamge);
				}
			}
		}

				[Range(0f, 1f)]
		public float chanceToHit;

				public int burnDamge;

				private BurnSystem BurnSys;
	}
}
