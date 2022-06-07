using System;
using UnityEngine;

namespace flanne
{
		public class Knockback : MonoBehaviour
	{
				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				MoveComponent2D component = other.gameObject.GetComponent<MoveComponent2D>();
				if (component.knockbackImmune)
				{
					return;
				}
				MoveComponent2D component2 = base.GetComponent<MoveComponent2D>();
				if (component2 != null)
				{
					component.vector = this.knockbackForce * component2.vectorLastFrame.normalized;
					return;
				}
				Vector2 vector = other.transform.position - base.transform.position;
				component.vector = this.knockbackForce * vector.normalized;
			}
		}

				[SerializeField]
		private string hitTag;

				public float knockbackForce;
	}
}
