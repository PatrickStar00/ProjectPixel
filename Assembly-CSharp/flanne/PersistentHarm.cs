using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class PersistentHarm : MonoBehaviour
	{
				private void Awake()
		{
			this._damageTargets = new List<Health>();
		}

				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				Health component = other.gameObject.GetComponent<Health>();
				if (component != null)
				{
					this._damageTargets.Add(component);
				}
			}
		}

				private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				Health component = other.gameObject.GetComponent<Health>();
				if (component != null)
				{
					this._damageTargets.Remove(component);
				}
			}
		}

				private void Update()
		{
			this._timer += Time.deltaTime;
			if (this._timer >= this.secondsPerTick)
			{
				this._timer -= this.secondsPerTick;
				for (int i = 0; i < this._damageTargets.Count; i++)
				{
					this._damageTargets[i].HPChange(-1 * this.damageAmount);
					if (this.triggerOnHit && i >= 0 && i < this._damageTargets.Count)
					{
						this.PostNotification(Projectile.ImpactEvent, this._damageTargets[i].gameObject);
					}
				}
			}
		}

				[SerializeField]
		private string hitTag;

				public int damageAmount;

				public float secondsPerTick;

				public bool triggerOnHit;

				private List<Health> _damageTargets;

				private float _timer;
	}
}
