using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class BuffDuringHolyShield : MonoBehaviour
	{
				private void OnDamagePrevented()
		{
			this.Deactivate();
		}

				private void OnCooldownDone()
		{
			this.Activate();
		}

				private void Start()
		{
			PlayerController componentInParent = base.transform.GetComponentInParent<PlayerController>();
			this.stats = componentInParent.stats;
			this.holyShield = base.transform.root.GetComponentInChildren<PreventDamage>();
			if (this.holyShield.isActive)
			{
				this.Activate();
			}
			this.holyShield.OnDamagePrevented.AddListener(new UnityAction(this.OnDamagePrevented));
			this.holyShield.OnCooldownDone.AddListener(new UnityAction(this.OnCooldownDone));
		}

				private void OnDestroy()
		{
			this.holyShield.OnDamagePrevented.RemoveListener(new UnityAction(this.OnDamagePrevented));
			this.holyShield.OnCooldownDone.RemoveListener(new UnityAction(this.OnCooldownDone));
		}

				private void Activate()
		{
			this.stats[StatType.ReloadRate].AddMultiplierBonus(this.reloadRateMulti);
			this.stats[StatType.MoveSpeed].AddMultiplierBonus(this.movespeedMulti);
		}

				private void Deactivate()
		{
			this.stats[StatType.ReloadRate].AddMultiplierBonus(-1f * this.reloadRateMulti);
			this.stats[StatType.MoveSpeed].AddMultiplierBonus(-1f * this.movespeedMulti);
		}

				[SerializeField]
		private float reloadRateMulti;

				[SerializeField]
		private float movespeedMulti;

				private StatsHolder stats;

				private PreventDamage holyShield;
	}
}
