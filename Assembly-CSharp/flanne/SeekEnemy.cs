using System;
using UnityEngine;

namespace flanne
{
		public class SeekEnemy : MonoBehaviour
	{
				private void Start()
		{
			if (base.GetComponentInParent<PlayerController>() != null)
			{
				this.player = base.GetComponentInParent<PlayerController>().transform;
			}
			this.GetNewTarget();
		}

				private void FixedUpdate()
		{
			if (this._target != null)
			{
				Vector2 vector = base.transform.position;
				Vector2 vector2 = this._target.position;
				if (Vector2.Distance(vector2, vector) < 1f)
				{
					this.GetNewTarget();
				}
				Vector2 vector3 = vector2 - vector;
				this.moveComponent.vector += vector3.normalized * this.acceleration * Time.fixedDeltaTime;
				return;
			}
			this.GetNewTarget();
		}

				private void GetNewTarget()
		{
			Vector2 center = this.player.transform.position;
			Vector2 range;
			range..ctor(this.seekDistanceX, this.seekDistanceY);
			GameObject randomEnemy = EnemyFinder.GetRandomEnemy(center, range);
			if (randomEnemy == null)
			{
				this._target = this.player;
				return;
			}
			this._target = randomEnemy.transform;
		}

				[SerializeField]
		private MoveComponent2D moveComponent;

				public float acceleration;

				[SerializeField]
		private float seekDistanceX;

				[SerializeField]
		private float seekDistanceY;

				public Transform player;

				private Transform _target;
	}
}
