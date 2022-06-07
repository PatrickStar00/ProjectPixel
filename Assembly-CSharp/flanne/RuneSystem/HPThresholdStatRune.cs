using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.RuneSystem
{
		public class HPThresholdStatRune : Rune
	{
				private void OnHpChange(int hp)
		{
			if (this.statsActive == this.playerHealth.HP <= this.playerHealth.maxHP / 2)
			{
				return;
			}
			if (this.playerHealth.HP <= this.playerHealth.maxHP / 2)
			{
				this.Activate();
				return;
			}
			this.Deactivate();
		}

				protected override void Init()
		{
			this.statsActive = false;
			this.playerHealth = this.player.playerHealth;
			this.playerHealth.onHealthChange.AddListener(new UnityAction<int>(this.OnHpChange));
		}

				private void OnDestroy()
		{
			this.playerHealth.onHealthChange.RemoveListener(new UnityAction<int>(this.OnHpChange));
		}

				private void Activate()
		{
			foreach (StatChange s in this.statChanges)
			{
				for (int j = 0; j < this.level; j++)
				{
					this.ApplyStat(s);
				}
			}
			this.statsActive = true;
		}

				private void Deactivate()
		{
			foreach (StatChange s in this.statChanges)
			{
				for (int j = 0; j < this.level; j++)
				{
					this.RemoveStat(s);
				}
			}
			this.statsActive = false;
		}

				private void ApplyStat(StatChange s)
		{
			StatsHolder stats = this.player.stats;
			if (s.isFlatMod)
			{
				stats[s.type].AddFlatBonus(s.flatValue);
			}
			else if (s.value > 0f)
			{
				stats[s.type].AddMultiplierBonus(s.value);
			}
			else if (s.value < 0f)
			{
				stats[s.type].AddMultiplierReduction(1f + s.value);
			}
			if (s.type == StatType.MaxHP)
			{
				this.player.playerHealth.maxHP = Mathf.FloorToInt(stats[s.type].Modify((float)this.player.playerHealth.maxHP));
			}
			if (s.type == StatType.CharacterSize)
			{
				this.player.playerSprite.transform.localScale = Vector3.one * stats[s.type].Modify(1f);
			}
			if (s.type == StatType.PickupRange)
			{
				GameObject.FindGameObjectWithTag("Pickupper").transform.localScale = Vector3.one * stats[s.type].Modify(1f);
			}
			if (s.type == StatType.VisionRange)
			{
				GameObject.FindGameObjectWithTag("PlayerVision").transform.localScale = Vector3.one * stats[s.type].Modify(1f);
			}
		}

				private void RemoveStat(StatChange s)
		{
			StatsHolder stats = this.player.stats;
			if (s.isFlatMod)
			{
				stats[s.type].AddFlatBonus(-1 * s.flatValue);
			}
			else if (s.value > 0f)
			{
				stats[s.type].AddMultiplierBonus(-1f * s.value);
			}
			else if (s.value < 0f)
			{
				stats[s.type].AddMultiplierReduction(1f + -1f * s.value);
			}
			if (s.type == StatType.MaxHP)
			{
				this.player.playerHealth.maxHP = Mathf.FloorToInt(stats[s.type].Modify((float)this.player.playerHealth.maxHP));
			}
			if (s.type == StatType.CharacterSize)
			{
				this.player.playerSprite.transform.localScale = Vector3.one * stats[s.type].Modify(1f);
			}
			if (s.type == StatType.PickupRange)
			{
				GameObject.FindGameObjectWithTag("Pickupper").transform.localScale = Vector3.one * stats[s.type].Modify(1f);
			}
			if (s.type == StatType.VisionRange)
			{
				GameObject.FindGameObjectWithTag("PlayerVision").transform.localScale = Vector3.one * stats[s.type].Modify(1f);
			}
		}

				[SerializeField]
		private StatChange[] statChanges = new StatChange[0];

				private bool statsActive;

				private Health playerHealth;
	}
}
