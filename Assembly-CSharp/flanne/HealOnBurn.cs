using System;
using UnityEngine;

namespace flanne
{
		public class HealOnBurn : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.playerHealth = componentInParent.playerHealth;
			this.AddObserver(new Action<object, object>(this.OnInflictBurn), BurnSystem.InflictBurnEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnInflictBurn), BurnSystem.InflictBurnEvent);
		}

				private void OnInflictBurn(object sender, object args)
		{
			if (Random.Range(0f, 1f) < this.chanceToHeal)
			{
				this.playerHealth.HPChange(1);
			}
		}

				[Range(0f, 1f)]
		[SerializeField]
		private float chanceToHeal;

				private Health playerHealth;
	}
}
