using System;
using System.Collections;
using UnityEngine;

namespace flanne.CharacterPassives
{
		public class DumpAmmoPassive : SkillPassive
	{
				protected override void Init()
		{
			this.player = base.transform.root.GetComponent<PlayerController>();
			this.ammo = this.player.ammo;
			this.myGun = this.player.gun;
			this._isSpraying = false;
		}

				protected override void PerformSkill()
		{
			if (!this.ammo.outOfAmmo && !this._isSpraying && !this.player.playerHealth.isDead)
			{
				base.StartCoroutine(this.SprayCR(this.ammo.amount));
			}
		}

				private IEnumerator SprayCR(int amountShots)
		{
			this._isSpraying = true;
			this.player.disableAction.Flip();
			this.myGun.gunObj.SetActive(false);
			this.player.disableAnimation.Flip();
			this.player.playerAnimator.ResetTrigger("Idle");
			this.player.playerAnimator.ResetTrigger("Run");
			this.player.playerAnimator.ResetTrigger("Walk");
			this.player.playerAnimator.SetTrigger("Special");
			while (this.ammo.amount > 1)
			{
				this.ShootRandom();
				yield return new WaitForSeconds(this.myGun.shotCooldown * this.shotCDMultiplier);
			}
			this.player.disableAnimation.UnFlip();
			this.player.playerAnimator.ResetTrigger("Special");
			this.player.playerAnimator.SetTrigger("Idle");
			this.myGun.gunObj.SetActive(true);
			this._isSpraying = false;
			this.player.disableAction.UnFlip();
			this.ShootRandom();
			yield break;
		}

				private void ShootRandom()
		{
			Vector2 zero = Vector2.zero;
			while (zero == Vector2.zero)
			{
				zero..ctor(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			}
			this.shooter.Shoot(this.myGun.GetProjectileRecipe(), zero, this.myGun.numOfProjectiles, this.myGun.spread, 0f);
			SoundEffectSO gunshotSFX = this.myGun.gunData.gunshotSFX;
			if (gunshotSFX != null)
			{
				gunshotSFX.Play(null);
			}
			this.myGun.OnShoot.Invoke();
		}

				[SerializeField]
		private float shotCDMultiplier;

				[SerializeField]
		private Shooter shooter;

				private PlayerController player;

				private Ammo ammo;

				private Gun myGun;

				private bool _isSpraying;
	}
}
