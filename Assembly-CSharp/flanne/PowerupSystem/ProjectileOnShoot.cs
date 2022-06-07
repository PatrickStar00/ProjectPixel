using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class ProjectileOnShoot : AttackOnShoot
	{
				protected override void Init()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.projectilePrefab.name, this.projectilePrefab, 50, true);
			this.SC = ShootingCursor.Instance;
		}

				public override void Attack()
		{
			GameObject pooledObject = this.OP.GetPooledObject(this.projectilePrefab.name);
			pooledObject.SetActive(true);
			pooledObject.transform.position = base.transform.position;
			Projectile component = pooledObject.GetComponent<Projectile>();
			Vector2 vector = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition);
			Vector2 vector2 = base.transform.position;
			Vector2 vector3 = vector - vector2;
			float num = -1f * this.inaccuracy / 2f;
			float num2 = -1f * num;
			Vector2 vector4 = new Vector2(vector3.x, vector3.y).Rotate(Random.Range(num2, num));
			component.vector = this.speed * vector4.normalized;
			component.angle = Mathf.Atan2(vector4.y, vector4.x) * 57.29578f;
		}

				[SerializeField]
		private GameObject projectilePrefab;

				[SerializeField]
		private float speed = 20f;

				[SerializeField]
		private float inaccuracy = 45f;

				private ObjectPooler OP;

				private ShootingCursor SC;
	}
}
