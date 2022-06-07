using System;
using UnityEngine;

namespace flanne
{
		public class AutoKillBelowHP : MonoBehaviour
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
					Health component = gameObject.GetComponent<Health>();
					if ((float)((component != null) ? new int?(component.HP) : null).Value / (float)((component != null) ? new int?(component.maxHP) : null).Value <= this.autoKillPercent)
					{
						component.AutoKill();
					}
				}
			}
		}

				[Range(0f, 1f)]
		public float autoKillPercent;
	}
}
