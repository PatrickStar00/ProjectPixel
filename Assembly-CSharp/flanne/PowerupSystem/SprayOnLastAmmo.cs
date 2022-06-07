using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystem
{
		public class SprayOnLastAmmo : MonoBehaviour
	{
				private void Start()
		{
			this.PF = ProjectileFactory.SharedInstance;
			this.player = base.GetComponentInParent<PlayerController>();
			this.myGun = this.player.gun;
			this.ammo = this.player.ammo;
			this.ammo.OnAmmoChanged.AddListener(new UnityAction<int>(this.OnAmmoChanged));
		}

				private void OnDestroy()
		{
			this.ammo.OnAmmoChanged.RemoveListener(new UnityAction<int>(this.OnAmmoChanged));
		}

				private void OnAmmoChanged(int ammoAmount)
		{
			if (ammoAmount == 0)
			{
				base.StartCoroutine(this.SprayBulletsCR());
			}
		}

				private IEnumerator SprayBulletsCR()
		{
			Vector2 startDirection = Vector2.zero;
			while (startDirection == Vector2.zero)
			{
				startDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			}
			yield return new WaitForSeconds(this.myGun.shotCooldown);
			int num;
			for (int i = 0; i < this.numOfBullets; i = num + 1)
			{
				float degrees = (float)i / (float)this.numOfBullets * 360f;
				Vector2 direction = startDirection.Rotate(degrees);
				this.PF.SpawnProjectile(this.myGun.GetProjectileRecipe(), direction, base.transform.position, this.damageMultiplier, false);
				SoundEffectSO gunshotSFX = this.myGun.gunData.gunshotSFX;
				if (gunshotSFX != null)
				{
					gunshotSFX.Play(null);
				}
				yield return new WaitForSeconds(this.delayBetweenShots);
				num = i;
			}
			yield break;
		}

				[SerializeField]
		private string bulletOPTag;

				[SerializeField]
		private int numOfBullets;

				[SerializeField]
		private float damageMultiplier;

				[SerializeField]
		private float delayBetweenShots;

				private ProjectileFactory PF;

				private PlayerController player;

				private Gun myGun;

				private Ammo ammo;
	}
}
