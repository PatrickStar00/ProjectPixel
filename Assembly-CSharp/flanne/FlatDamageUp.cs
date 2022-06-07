using System;
using UnityEngine;

namespace flanne
{
		public class FlatDamageUp : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnImpact), Projectile.ImpactEvent);
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
					Health component = gameObject.gameObject.GetComponent<Health>();
					if (component == null)
					{
						return;
					}
					component.HPChange(-1 * this.damage);
				}
			}
		}

				public int damage;
	}
}
