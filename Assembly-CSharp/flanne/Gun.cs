using System;
using System.Collections;
using flanne.Core;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class Gun : MonoBehaviour
	{
								public StatsHolder stats { get; private set; }

								public GunData gunData { get; private set; }

								public Animator[] gunAnimators { get; private set; }

								public Shooter[] shooters { get; private set; }

								public GameObject gunObj { get; private set; }

						public float shotCooldown
		{
			get
			{
				return this.stats[StatType.FireRate].ModifyInverse(this.gunData.shotCooldown);
			}
		}

						public float reloadDuration
		{
			get
			{
				return this.stats[StatType.ReloadRate].ModifyInverse(this.gunData.reloadDuration);
			}
		}

						public float damage
		{
			get
			{
				return this.stats[StatType.BulletDamage].Modify(this.gunData.damage);
			}
		}

						public int numOfProjectiles
		{
			get
			{
				return (int)this.stats[StatType.Projectiles].Modify((float)this.gunData.numOfProjectiles);
			}
		}

						public float spread
		{
			get
			{
				return this.stats[StatType.Spread].Modify(this.gunData.spread);
			}
		}

						public int maxAmmo
		{
			get
			{
				return Mathf.Max(1, (int)this.stats[StatType.MaxAmmo].Modify((float)this.gunData.maxAmmo));
			}
		}

						public bool shotReady
		{
			get
			{
				return this._shotTimer <= 0f;
			}
		}

				private void Start()
		{
			this._isShooting = false;
			this.stats = this.player.stats;
			this.SC = ShootingCursor.Instance;
		}

				private void Update()
		{
			if (!PauseController.isPaused)
			{
				if (this._shotTimer > 0f)
				{
					this._shotTimer -= Time.deltaTime;
					return;
				}
				if (this._isShooting)
				{
					if (this.gunData.isSummonGun)
					{
						this._shotTimer += this.stats[StatType.SummonAttackSpeed].ModifyInverse(this.shotCooldown);
					}
					else
					{
						this._shotTimer += this.shotCooldown;
					}
					Vector2 vector = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition);
					Vector2 vector2 = base.transform.position;
					Vector2 pointDirection = vector - vector2;
					Shooter[] shooters = this.shooters;
					for (int i = 0; i < shooters.Length; i++)
					{
						shooters[i].Shoot(this.GetProjectileRecipe(), pointDirection, this.numOfProjectiles, this.spread, 10f);
						this.OnShoot.Invoke();
					}
					SoundEffectSO gunshotSFX = this.gunData.gunshotSFX;
					if (gunshotSFX == null)
					{
						return;
					}
					gunshotSFX.Play(null);
				}
			}
		}

				public void LoadGun(GunData gunToLoad)
		{
			if (gunToLoad == null)
			{
				this.gunData = this.defaultGun;
			}
			else
			{
				this.gunData = gunToLoad;
			}
			if (this.shooters != null)
			{
				Shooter[] shooters = this.shooters;
				for (int i = 0; i < shooters.Length; i++)
				{
					Object.Destroy(shooters[i].gameObject);
				}
			}
			this.gunObj = Object.Instantiate<GameObject>(this.gunData.model);
			this.gunObj.transform.SetParent(base.transform);
			this.gunObj.transform.localPosition = Vector3.zero;
			this.gunAnimators = this.gunObj.GetComponentsInChildren<Animator>();
			if (this.gunAnimators == null)
			{
				Debug.LogError(this.gunObj + "is missing an animator.");
			}
			this.shooters = this.gunObj.GetComponentsInChildren<Shooter>();
			if (this.shooters == null)
			{
				Debug.LogError(this.gunObj + "is missing an shooter.");
			}
			ObjectPooler.SharedInstance.AddObject(this.gunData.bulletOPTag, this.gunData.bullet, 200, true);
		}

				public void SetAnimationTrigger(string trigger)
		{
			Animator[] gunAnimators = this.gunAnimators;
			for (int i = 0; i < gunAnimators.Length; i++)
			{
				gunAnimators[i].SetTrigger(trigger);
			}
		}

				public void StartShooting()
		{
			this._isShooting = true;
		}

				public void StopShooting()
		{
			this._isShooting = false;
		}

				public ProjectileRecipe GetProjectileRecipe()
		{
			ProjectileRecipe projectileRecipe = new ProjectileRecipe();
			projectileRecipe.objectPoolTag = this.gunData.bulletOPTag;
			if (this.gunData.isSummonGun)
			{
				projectileRecipe.damage = this.stats[StatType.SummonDamage].Modify(this.stats[StatType.BulletDamage].Modify(this.gunData.damage));
			}
			else
			{
				projectileRecipe.damage = this.stats[StatType.BulletDamage].Modify(this.gunData.damage);
			}
			projectileRecipe.projectileSpeed = this.stats[StatType.ProjectileSpeed].Modify(this.gunData.projectileSpeed);
			projectileRecipe.size = this.stats[StatType.ProjectileSize].Modify(1f);
			projectileRecipe.knockback = this.stats[StatType.Knockback].Modify(this.gunData.knockback);
			projectileRecipe.bounce = Mathf.Max(0, (int)this.stats[StatType.Bounce].Modify((float)this.gunData.bounce));
			projectileRecipe.piercing = Mathf.Max(0, (int)this.stats[StatType.Piercing].Modify((float)this.gunData.piercing));
			return projectileRecipe;
		}

				[SerializeField]
		private PlayerController player;

				[SerializeField]
		private GunData defaultGun;

				public UnityEvent OnShoot;

				private ShootingCursor SC;

				private IEnumerator _reloadCoroutine;

				private bool _isShooting;

				private float _shotTimer;
	}
}
