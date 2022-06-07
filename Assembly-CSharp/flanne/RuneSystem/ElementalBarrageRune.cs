using System;
using System.Collections;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class ElementalBarrageRune : Rune
	{
				protected override void Init()
		{
			this.AddObserver(new Action<object, object>(this.OnInflict), BurnSystem.InflictBurnEvent);
			this.AddObserver(new Action<object, object>(this.OnInflict), FreezeSystem.InflictFreezeEvent);
			this.inflictCounter = 0;
			this.disableInflictGain = false;
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnInflict), BurnSystem.InflictBurnEvent);
			this.RemoveObserver(new Action<object, object>(this.OnInflict), FreezeSystem.InflictFreezeEvent);
		}

				private void OnInflict(object sender, object args)
		{
			if (this.disableInflictGain)
			{
				return;
			}
			this.inflictCounter++;
			if (this.inflictCounter >= this.inflictsToActivate)
			{
				this.inflictCounter = 0;
				base.StartCoroutine(this.StartBonusCR());
			}
		}

				private void ActivateBonus()
		{
			this.player.stats[StatType.FireRate].AddMultiplierBonus(this.bonusStatMulti);
			this.player.stats[StatType.FireRate].AddMultiplierBonus(this.bonusStatMulti);
		}

				private void DeactivateBonus()
		{
			this.player.stats[StatType.FireRate].AddMultiplierBonus(-1f * this.bonusStatMulti);
			this.player.stats[StatType.FireRate].AddMultiplierBonus(-1f * this.bonusStatMulti);
		}

				private IEnumerator StartBonusCR()
		{
			this.disableInflictGain = true;
			this.ActivateBonus();
			yield return new WaitForSeconds(this.secondsPerLevel * (float)this.level);
			this.DeactivateBonus();
			base.StartCoroutine(this.WaitForCooldownCR());
			yield break;
		}

				private IEnumerator WaitForCooldownCR()
		{
			yield return new WaitForSeconds(this.cooldown);
			this.disableInflictGain = false;
			yield break;
		}

				[SerializeField]
		private float bonusStatMulti;

				[SerializeField]
		private float secondsPerLevel;

				[SerializeField]
		private float cooldown;

				[SerializeField]
		private int inflictsToActivate;

				private int inflictCounter;

				private bool disableInflictGain;
	}
}
