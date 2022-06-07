using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystem
{
		public class ShootBulletsBehind : MonoBehaviour
	{
				private void Start()
		{
			this.PF = ProjectileFactory.SharedInstance;
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.myGun = componentInParent.gun;
			this.myGun.OnShoot.AddListener(new UnityAction(this.OnShoot));
			this.SC = ShootingCursor.Instance;
		}

				private void OnDestroy()
		{
			this.myGun.OnShoot.RemoveListener(new UnityAction(this.OnShoot));
		}

				private void OnShoot()
		{
			Vector2 vector = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition);
			Vector2 v = base.transform.position - vector;
			float num = -1f * this.spread / 2f;
			for (int i = 0; i < this.numOfBullets; i++)
			{
				float degrees = num + (float)i / (float)this.numOfBullets * this.spread;
				Vector2 direction = v.Rotate(degrees);
				this.PF.SpawnProjectile(this.myGun.GetProjectileRecipe(), direction, base.transform.position, this.damageMultiplier, false);
			}
		}

				[SerializeField]
		private string bulletOPTag;

				[SerializeField]
		private int numOfBullets;

				[SerializeField]
		private float spread;

				[SerializeField]
		private float damageMultiplier;

				private ProjectileFactory PF;

				private Gun myGun;

				private ShootingCursor SC;
	}
}
