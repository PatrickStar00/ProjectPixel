using System;
using UnityEngine;

namespace flanne
{
		public class MoveComponent2D : MonoBehaviour
	{
								public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this.vectorLastFrame = this._vector;
				this._vector = value;
			}
		}

				private void Start()
		{
			MoveSystem2D.Register(this);
		}

				private void OnDisable()
		{
			this.vector = Vector2.zero;
			this.Rb.velocity = Vector2.zero;
			this.Rb.angularVelocity = 0f;
		}

				private Vector2 _vector;

				public Vector2 vectorLastFrame;

				public float drag;

				public Rigidbody2D Rb;

				public bool knockbackImmune;

				public bool rotateTowardsMove;
	}
}
