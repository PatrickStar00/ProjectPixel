using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "StatPowerup", menuName = "Powerups/StatPowerup", order = 1)]
	public class StatPowerup : Powerup
	{
						public override string description
		{
			get
			{
				string text = string.Empty;
				foreach (StatChange statChange in this.statChanges)
				{
					text = text + LocalizationSystem.GetLocalizedValue(StatLabels.Labels[statChange.type]) + " ";
					if (statChange.isFlatMod)
					{
						if (statChange.flatValue > 0)
						{
							text = string.Concat(new object[]
							{
								text,
								"<color=#f5d6c1>+",
								statChange.flatValue,
								"</color><br>"
							});
						}
						else if (statChange.flatValue < 0)
						{
							text = string.Concat(new object[]
							{
								text,
								"<color=#fd5161>",
								statChange.flatValue,
								"</color><br>"
							});
						}
					}
					else if (statChange.value > 0f)
					{
						text = string.Concat(new object[]
						{
							text,
							"<color=#f5d6c1>+",
							Mathf.FloorToInt(statChange.value * 100f),
							"%</color><br>"
						});
					}
					else if (statChange.value < 0f)
					{
						text = string.Concat(new object[]
						{
							text,
							"<color=#fd5161>",
							Mathf.FloorToInt(statChange.value * 100f),
							"%</color><br>"
						});
					}
				}
				return text + LocalizationSystem.GetLocalizedValue(this.descriptionStringID.key);
			}
		}

				protected override void Apply(GameObject target)
		{
			PlayerController component = target.GetComponent<PlayerController>();
			StatsHolder stats = component.stats;
			foreach (StatChange statChange in this.statChanges)
			{
				if (statChange.isFlatMod)
				{
					stats[statChange.type].AddFlatBonus(statChange.flatValue);
				}
				else if (statChange.value > 0f)
				{
					stats[statChange.type].AddMultiplierBonus(statChange.value);
				}
				else if (statChange.value < 0f)
				{
					stats[statChange.type].AddMultiplierReduction(1f + statChange.value);
				}
				if (statChange.type == StatType.MaxHP)
				{
					component.playerHealth.maxHP = Mathf.FloorToInt(stats[statChange.type].Modify((float)component.loadedCharacter.startHP));
				}
				if (statChange.type == StatType.CharacterSize)
				{
					component.playerSprite.transform.localScale = Vector3.one * stats[statChange.type].Modify(1f);
				}
				if (statChange.type == StatType.PickupRange)
				{
					GameObject.FindGameObjectWithTag("Pickupper").transform.localScale = Vector3.one * stats[statChange.type].Modify(1f);
				}
				if (statChange.type == StatType.VisionRange)
				{
					GameObject.FindGameObjectWithTag("PlayerVision").transform.localScale = Vector3.one * stats[statChange.type].Modify(1f);
				}
			}
		}

				[SerializeField]
		private StatChange[] statChanges = new StatChange[0];
	}
}
