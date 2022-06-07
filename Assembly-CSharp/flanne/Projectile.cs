using System;
using UnityEngine;

namespace flanne
{
		public class Projectile : MonoBehaviour
	{
						public virtual float damage
		{
			set
			{
				if (this.harm != null)
				{
					this.harm.damageAmount = Mathf.FloorToInt(value);
				}
			}
		}

						public float knockback
		{
			set
			{
				this.kb.knockbackForce = value;
			}
		}

						public float angle
		{
			set
			{
				this.rb.rotation = value;
			}
		}

						public float size
		{
			set
			{
				this.SetSize(value);
			}
		}

						public Vector2 vector
		{
			set
			{
				this.move.vector = value;
			}
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
		}

				protected virtual void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				Health component = other.gameObject.GetComponent<Health>();
				int? num = (component != null) ? new int?(component.HP) : null;
				Harmful harmful = this.harm;
				int? num2 = (harmful != null) ? new int?(harmful.damageAmount) : null;
				if (num.GetValueOrDefault() <= num2.GetValueOrDefault() & (num != null & num2 != null))
				{
					this.PostNotification(Projectile.KillEvent, other);
				}
				this.PostNotification(Projectile.ImpactEvent, other.gameObject);
				if (this.bounce == 0)
				{
					if (this.piercing == 0)
					{
						base.gameObject.SetActive(false);
						return;
					}
					this.piercing--;
					return;
				}
				else
				{
					this.bounce--;
					float magnitude = this.move.vector.magnitude;
					Vector2 v = Vector2.Reflect(base.transform.right, this.move.vector).normalized * magnitude;
					v = v.Rotate((float)Random.Range(-45, 45));
					this.move.vector = v.normalized * magnitude;
				}
			}
		}

				protected virtual void SetSize(float size)
		{
			base.transform.localScale = size * Vector3.one;
			if (this.trail != null)
			{
				this.trail.widthMultiplier = size;
			}
		}

				public static string ImpactEvent = "Projectile.ImpactEvent";

				public static string KillEvent = "Projectile.KillEvent";

				[SerializeField]
		public string hitTag;

				[SerializeField]
		private Rigidbody2D rb;

				[SerializeField]
		private Harmful harm;

				[SerializeField]
		private Knockback kb;

				[SerializeField]
		private MoveComponent2D move;

				[SerializeField]
		private TrailRenderer trail;

				public int bounce;

				public int piercing;

				public bool isSecondary;

				protected ObjectPooler OP;
	}
}
