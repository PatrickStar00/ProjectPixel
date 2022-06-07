using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystems
{
		public class DamageUpOnReload : MonoBehaviour
	{
				private void OnReload()
		{
			if (this._timer <= 0f)
			{
				this.stats[StatType.BulletDamage].AddMultiplierBonus(this.damageBonus);
			}
			this._timer = this.duration;
		}

				private void Start()
		{
			PlayerController componentInParent = base.transform.GetComponentInParent<PlayerController>();
			this.stats = componentInParent.stats;
			this.ammo = componentInParent.ammo;
			this.ammo.OnReload.AddListener(new UnityAction(this.OnReload));
		}

				private void OnDestroy()
		{
			this.ammo.OnReload.RemoveListener(new UnityAction(this.OnReload));
		}

				private void Update()
		{
			if (this._timer > 0f)
			{
				this._timer -= Time.deltaTime;
				if (this._timer <= 0f)
				{
					this.stats[StatType.BulletDamage].AddMultiplierBonus(-1f * this.damageBonus);
				}
			}
		}

				[SerializeField]
		private float damageBonus;

				[SerializeField]
		private float duration;

				private StatsHolder stats;

				private Ammo ammo;

				private float _timer;
	}
}
