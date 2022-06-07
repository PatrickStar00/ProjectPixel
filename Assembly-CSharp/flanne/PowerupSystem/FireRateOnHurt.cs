using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystem
{
		public class FireRateOnHurt : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.stats = componentInParent.stats;
			this.health = componentInParent.playerHealth;
			this.health.onHurt.AddListener(new UnityAction<int>(this.OnHurt));
		}

				private void OnDestroy()
		{
			this.health.onHurt.RemoveListener(new UnityAction<int>(this.OnHurt));
		}

				private void OnHurt(int i)
		{
			if (this.statBoostCoroutine != null)
			{
				base.StopCoroutine(this.statBoostCoroutine);
				this.statBoostCoroutine = null;
				this.RemoveBoost();
			}
			this.statBoostCoroutine = this.StatBoostCR();
			base.StartCoroutine(this.statBoostCoroutine);
		}

				private IEnumerator StatBoostCR()
		{
			this.AddBoost();
			yield return new WaitForSeconds(this.duration);
			this.RemoveBoost();
			this.statBoostCoroutine = null;
			yield break;
		}

				private void AddBoost()
		{
			this.stats[StatType.FireRate].AddMultiplierBonus(this.fireRateBoost);
			this.stats[StatType.ReloadRate].AddMultiplierBonus(this.fireRateBoost);
		}

				private void RemoveBoost()
		{
			this.stats[StatType.FireRate].AddMultiplierBonus(-1f * this.fireRateBoost);
			this.stats[StatType.ReloadRate].AddMultiplierBonus(-1f * this.fireRateBoost);
		}

				[SerializeField]
		private float duration;

				[SerializeField]
		private float fireRateBoost;

				[SerializeField]
		private float reloadRateBoost;

				private StatsHolder stats;

				private Health health;

				private IEnumerator statBoostCoroutine;
	}
}
