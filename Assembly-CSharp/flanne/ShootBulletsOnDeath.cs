using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class ShootBulletsOnDeath : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
			this.PF = ProjectileFactory.SharedInstance;
			this.myGun = base.GetComponentInParent<PlayerController>().gun;
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDeath(object sender, object args)
		{
			GameObject gameObject = (sender as Health).gameObject;
			base.StartCoroutine(this.ShootOnDeathCR(gameObject));
		}

				private IEnumerator ShootOnDeathCR(GameObject deathObj)
		{
			yield return null;
			if (deathObj.tag == "Enemy")
			{
				Vector2 zero = Vector2.zero;
				while (zero == Vector2.zero)
				{
					zero..ctor(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				}
				for (int i = 0; i < this.numOfBullets; i++)
				{
					float degrees = (float)i / (float)this.numOfBullets * 360f;
					Vector2 direction = zero.Rotate(degrees);
					ProjectileRecipe projectileRecipe = this.myGun.GetProjectileRecipe();
					projectileRecipe.size *= 0.5f;
					projectileRecipe.knockback *= 0.1f;
					this.PF.SpawnProjectile(projectileRecipe, direction, deathObj.transform.position, this.damageMultiplier, true);
				}
			}
			yield break;
		}

				[SerializeField]
		private string bulletOPTag;

				[SerializeField]
		private int numOfBullets;

				[SerializeField]
		private float damageMultiplier;

				private ProjectileFactory PF;

				private Gun myGun;
	}
}
