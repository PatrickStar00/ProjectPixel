using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class Shooter : MonoBehaviour
	{
				private void Start()
		{
			this.PF = ProjectileFactory.SharedInstance;
			this.OP = ObjectPooler.SharedInstance;
			if (this.muzzleFlashPrefab != null)
			{
				this.OP.AddObject(this.muzzleFlashPrefab.name, this.muzzleFlashPrefab, 100, true);
			}
		}

				public void Shoot(ProjectileRecipe recipe, Vector2 pointDirection, int numProjectiles, float spread, float inaccuracy)
		{
			pointDirection = this.RandomizeDirection(pointDirection, inaccuracy);
			if (numProjectiles > 1)
			{
				float num = -1f * spread / 2f;
				for (int i = 0; i < numProjectiles; i++)
				{
					float degrees = num + (float)i / (float)numProjectiles * spread;
					Vector2 direction = pointDirection.Rotate(degrees);
					Projectile e = this.PF.SpawnProjectile(recipe, direction, base.transform.position, 1f, false);
					this.PostNotification(Shooter.BulletShotEvent, e);
				}
			}
			else
			{
				Projectile e2 = this.PF.SpawnProjectile(recipe, pointDirection, base.transform.position, 1f, false);
				this.PostNotification(Shooter.BulletShotEvent, e2);
			}
			if (this.muzzleFlashPrefab != null)
			{
				GameObject pooledObject = this.OP.GetPooledObject(this.muzzleFlashPrefab.name);
				pooledObject.SetActive(true);
				pooledObject.transform.SetParent(base.transform);
				pooledObject.transform.localPosition = Vector3.zero;
				pooledObject.transform.localRotation = Quaternion.identity;
			}
			UnityEvent unityEvent = this.onShoot;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

				private Vector3 RandomizeDirection(Vector2 direction, float degrees)
		{
			float num = -1f * degrees / 2f;
			float num2 = -1f * num;
			Vector2 vector;
			vector..ctor(direction.x, direction.y);
			vector = vector.Rotate(Random.Range(num2, num));
			return new Vector3(vector.x, vector.y, 0f);
		}

				public static string BulletShotEvent = "Shooter.BulletShotEvent";

				public UnityEvent onShoot;

				[SerializeField]
		private GameObject muzzleFlashPrefab;

				private ProjectileFactory PF;

				private ObjectPooler OP;
	}
}
