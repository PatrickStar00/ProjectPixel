using System;
using UnityEngine;

namespace flanne
{
		public class ProjectileFactory : MonoBehaviour
	{
				private void Awake()
		{
			ProjectileFactory.SharedInstance = this;
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
		}

				public Projectile SpawnProjectile(ProjectileRecipe recipe, Vector2 direction, Vector3 position, float damageMultiplier = 1f, bool isSecondary = false)
		{
			GameObject pooledObject = this.OP.GetPooledObject(recipe.objectPoolTag);
			pooledObject.SetActive(true);
			pooledObject.transform.position = position;
			Projectile component = pooledObject.GetComponent<Projectile>();
			component.isSecondary = isSecondary;
			component.vector = recipe.projectileSpeed * direction.normalized;
			component.angle = Mathf.Atan2(direction.y, direction.x) * 57.29578f;
			component.size = recipe.size;
			component.damage = Mathf.Max(1f, recipe.damage * damageMultiplier);
			component.knockback = recipe.knockback;
			component.bounce = recipe.bounce;
			component.piercing = recipe.piercing;
			return component;
		}

				public static ProjectileFactory SharedInstance;

				private ObjectPooler OP;
	}
}
