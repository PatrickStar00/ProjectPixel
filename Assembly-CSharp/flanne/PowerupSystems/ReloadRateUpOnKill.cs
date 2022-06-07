using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystems
{
		public class ReloadRateUpOnKill : MonoBehaviour
	{
				private void OnDeath(object sender, object args)
		{
			if ((sender as Health).gameObject.tag == "Enemy")
			{
				this.stats[StatType.ReloadRate].AddMultiplierBonus(this.bonusPerStack);
				this._stacks++;
			}
		}

				private void OnReload()
		{
			this.stats[StatType.ReloadRate].AddMultiplierBonus((float)(-1 * this._stacks) * this.bonusPerStack);
			this._stacks = 0;
		}

				private void Start()
		{
			PlayerController componentInParent = base.transform.GetComponentInParent<PlayerController>();
			this.stats = componentInParent.stats;
			this.ammo = componentInParent.ammo;
			this.ammo.OnReload.AddListener(new UnityAction(this.OnReload));
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDestroy()
		{
			this.ammo.OnReload.RemoveListener(new UnityAction(this.OnReload));
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				[SerializeField]
		private float bonusPerStack;

				private StatsHolder stats;

				private Ammo ammo;

				private int _stacks;
	}
}
