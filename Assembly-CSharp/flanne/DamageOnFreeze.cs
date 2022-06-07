using System;
using UnityEngine;

namespace flanne
{
		public class DamageOnFreeze : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnFreeze), FreezeSystem.InflictFreezeEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnFreeze), FreezeSystem.InflictFreezeEvent);
		}

				private void OnFreeze(object sender, object args)
		{
			GameObject gameObject = args as GameObject;
			Health component = gameObject.GetComponent<Health>();
			int change;
			if (gameObject.tag.Contains("Champion"))
			{
				change = -1 * Mathf.FloorToInt((float)component.maxHP * this.championPercentDamage);
			}
			else
			{
				change = -1 * Mathf.FloorToInt((float)component.maxHP * this.percentDamage);
			}
			component.HPChange(change);
		}

				[Range(0f, 1f)]
		[SerializeField]
		private float percentDamage;

				[Range(0f, 1f)]
		[SerializeField]
		private float championPercentDamage;
	}
}
