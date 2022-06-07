using System;
using UnityEngine;

namespace flanne
{
		public class HomingProjectile : MonoBehaviour
	{
				private void OnEnable()
		{
			this._target = EnemyFinder.GetRandomEnemy(Vector3.zero, Vector3.positiveInfinity);
		}

				private void FixedUpdate()
		{
			if (this._target != null && this._target.activeSelf)
			{
				Vector2 vector = base.transform.position;
				Vector2 vector2 = this._target.transform.position - vector;
				this.moveComponent.vector += vector2.normalized * this.acceleration * Time.fixedDeltaTime;
				return;
			}
			this._target = EnemyFinder.GetRandomEnemy(Vector3.zero, Vector3.positiveInfinity);
		}

				[SerializeField]
		private MoveComponent2D moveComponent;

				[SerializeField]
		private float acceleration;

				private GameObject _target;
	}
}
