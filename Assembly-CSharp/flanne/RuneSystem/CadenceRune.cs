using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.RuneSystem
{
		public class CadenceRune : Rune
	{
				protected override void Init()
		{
			this.player.gun.OnShoot.AddListener(new UnityAction(this.IncrementCounter));
		}

				private void OnDestroy()
		{
			this.player.gun.OnShoot.RemoveListener(new UnityAction(this.IncrementCounter));
		}

				public void IncrementCounter()
		{
			this._counter++;
			if (this._counter == this.shotsPerBuff - 1)
			{
				this.Activate();
				return;
			}
			if (this._counter >= this.shotsPerBuff)
			{
				this._counter = 0;
				this.Deactivate();
			}
		}

				private void Activate()
		{
			this.player.stats[StatType.Piercing].AddFlatBonus(99);
			float value = this.bonusStatsPerLevel * (float)this.level;
			this.player.stats[StatType.ProjectileSize].AddMultiplierBonus(value);
			this.player.stats[StatType.BulletDamage].AddMultiplierBonus(value);
		}

				private void Deactivate()
		{
			this.player.stats[StatType.Piercing].AddFlatBonus(-99);
			float num = this.bonusStatsPerLevel * (float)this.level;
			this.player.stats[StatType.ProjectileSize].AddMultiplierBonus(-1f * num);
			this.player.stats[StatType.BulletDamage].AddMultiplierBonus(-1f * num);
		}

				[SerializeField]
		private float bonusStatsPerLevel;

				[SerializeField]
		private int shotsPerBuff;

				private int _counter;
	}
}
