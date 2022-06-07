using System;
using UnityEngine;

namespace flanne
{
		public class ShootingSummon : AttackingSummon
	{
				protected override void Init()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.projectilePrefab.name, this.projectilePrefab, 50, true);
			this.SC = ShootingCursor.Instance;
		}

				protected override bool Attack()
		{
			Vector2 vector = base.transform.position;
			Vector2 zero = Vector2.zero;
			if (this.targetMouse)
			{
				Vector2 direction = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition) - vector;
				this.Shoot(direction);
				return true;
			}
			if (EnemyFinder.GetRandomEnemy(vector, new Vector2(9f, 6f)) != null)
			{
				Vector2 direction2 = EnemyFinder.GetRandomEnemy(vector, new Vector2(9f, 6f)).transform.position - vector;
				this.Shoot(direction2);
				return true;
			}
			return false;
		}

				private void Shoot(Vector2 direction)
		{
			if (direction.x < 0f)
			{
				base.transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else if (direction.x > 0f)
			{
				base.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			this.shooter.Shoot(this.GetProjectileRecipe(), direction, this.numProjectiles, (float)((this.numProjectiles - 1) * 15), 0f);
		}

				private ProjectileRecipe GetProjectileRecipe()
		{
			ProjectileRecipe projectileRecipe = new ProjectileRecipe();
			projectileRecipe.objectPoolTag = this.projectilePrefab.name;
			if (this.inheritPlayerDamage)
			{
				projectileRecipe.damage = base.summonDamageMod.Modify(this.player.gun.damage);
			}
			else
			{
				projectileRecipe.damage = base.summonDamageMod.Modify((float)this.baseDamage);
			}
			projectileRecipe.projectileSpeed = this.projectileSpeed;
			projectileRecipe.size = 1f;
			projectileRecipe.knockback = this.knockback;
			projectileRecipe.bounce = this.bounce;
			projectileRecipe.piercing = this.pierce;
			return projectileRecipe;
		}

				[SerializeField]
		private GameObject projectilePrefab;

				[SerializeField]
		private Shooter shooter;

				public bool targetMouse;

				public bool inheritPlayerDamage;

				public int baseDamage;

				public int numProjectiles;

				public float projectileSpeed;

				public float knockback;

				public int bounce;

				public int pierce;

				private ObjectPooler OP;

				private ShootingCursor SC;
	}
}
