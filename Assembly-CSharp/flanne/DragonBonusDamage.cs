using System;
using UnityEngine;

namespace flanne
{
		public class DragonBonusDamage : MonoBehaviour
	{
						private int damage
		{
			get
			{
				return Mathf.FloorToInt((float)this._dragon.baseDamage * this.percentOfDragonsDamage);
			}
		}

				private void Start()
		{
			foreach (ShootingSummon shootingSummon in base.GetComponentInParent<PlayerController>().GetComponentsInChildren<ShootingSummon>(true))
			{
				if (shootingSummon.SummonTypeID == "Dragon")
				{
					this._dragon = shootingSummon;
				}
			}
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
					Health component = gameObject.GetComponent<Health>();
					if (component == null)
					{
						return;
					}
					component.HPChange(-1 * this.damage);
				}
			}
		}

				[Range(0f, 1f)]
		[SerializeField]
		private float percentOfDragonsDamage;

				private ShootingSummon _dragon;
	}
}
