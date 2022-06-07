using System;
using UnityEngine;

namespace flanne
{
		public class PierceOnKill : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnKill), Projectile.KillEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnKill), Projectile.KillEvent);
		}

				private void OnKill(object sender, object args)
		{
			Projectile projectile = sender as Projectile;
			if ((args as Collision2D).gameObject.tag == "Enemy")
			{
				projectile.piercing++;
			}
		}
	}
}
