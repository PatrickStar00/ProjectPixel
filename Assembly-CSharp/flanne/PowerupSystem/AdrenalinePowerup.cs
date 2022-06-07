using System;
using System.Collections;
using UnityEngine;

namespace flanne.PowerupSystem
{
		[CreateAssetMenu(fileName = "AdrenalinePowerup", menuName = "Powerups/AdrenalinePowerup")]
	public class AdrenalinePowerup : Powerup
	{
				protected override void Apply(GameObject target)
		{
			PlayerController component = target.GetComponent<PlayerController>();
			StatsHolder stats = component.stats;
			component.StartCoroutine(this.StartAdrenaline(stats));
		}

				private IEnumerator StartAdrenaline(StatsHolder stats)
		{
			foreach (AdrenalinePowerup.StatChange statChange in this.statChanges)
			{
				stats[statChange.type].AddMultiplierBonus(statChange.value);
			}
			yield return new WaitForSeconds(this.duration);
			foreach (AdrenalinePowerup.StatChange statChange2 in this.statChanges)
			{
				stats[statChange2.type].AddMultiplierBonus(-1f * statChange2.value);
			}
			yield break;
		}

				[SerializeField]
		private float duration;

				[SerializeField]
		private AdrenalinePowerup.StatChange[] statChanges = new AdrenalinePowerup.StatChange[0];

				[Serializable]
		private struct StatChange
		{
						public StatType type;

						public float value;
		}
	}
}
