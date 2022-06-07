using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class DamageBoostInRange : MonoBehaviour
	{
				private void OnTweakDamge(object sender, object args)
		{
			(args as List<ValueModifier>).Add(new MultValueModifier(0, 1f + this.damageBoost));
		}

				private void Start()
		{
			this._targetsInRange = new List<Health>();
		}

				private void OnDestroy()
		{
			foreach (Health sender in this._targetsInRange)
			{
				this.RemoveObserver(new Action<object, object>(this.OnTweakDamge), Health.TweakDamageEvent, sender);
			}
		}

				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				Health component = other.gameObject.GetComponent<Health>();
				if (component != null)
				{
					this.AddObserver(new Action<object, object>(this.OnTweakDamge), Health.TweakDamageEvent, component);
					this._targetsInRange.Add(component);
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
					this.RemoveObserver(new Action<object, object>(this.OnTweakDamge), Health.TweakDamageEvent, component);
					this._targetsInRange.Remove(component);
				}
			}
		}

				[NonSerialized]
		public float damageBoost;

				[SerializeField]
		private string hitTag;

				private List<Health> _targetsInRange;
	}
}
