using System;
using UnityEngine;

namespace flanne
{
		public class ElectroProjectile : Projectile
	{
						public override float damage
		{
			set
			{
				this._damage = Mathf.FloorToInt(value);
			}
		}

				private void Start()
		{
			this.TGen = ThunderGenerator.SharedInstance;
		}

				protected override void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				this.TGen.GenerateAt(other.gameObject, this._damage);
			}
			base.OnCollisionEnter2D(other);
		}

				private int _damage;

				private ThunderGenerator TGen;
	}
}
