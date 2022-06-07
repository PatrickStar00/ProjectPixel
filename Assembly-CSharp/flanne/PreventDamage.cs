using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class PreventDamage : MonoBehaviour
	{
								public bool isActive { get; private set; }

				private void OnPreventDamage()
		{
			if (this._waitCooldownCoroutine == null)
			{
				this._waitCooldownCoroutine = this.InvincibilityCooldownCR();
				base.StartCoroutine(this._waitCooldownCoroutine);
			}
		}

				private void Start()
		{
			this._waitCooldownCoroutine = null;
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.playerHealth = componentInParent.playerHealth;
			this.playerHealth.onDamagePrevented.AddListener(new UnityAction(this.OnPreventDamage));
			base.StartCoroutine(this.WaitToApplyInvincibilityCR());
		}

				private void OnDestroy()
		{
			this.playerHealth.onDamagePrevented.RemoveListener(new UnityAction(this.OnPreventDamage));
		}

				private IEnumerator InvincibilityCooldownCR()
		{
			this.OnDamagePrevented.Invoke();
			yield return new WaitForSeconds(0.5f);
			this.playerHealth.isInvincible = false;
			this.isActive = false;
			yield return new WaitForSeconds(this.cooldownTime);
			base.StartCoroutine(this.WaitToApplyInvincibilityCR());
			yield break;
		}

				private IEnumerator WaitToApplyInvincibilityCR()
		{
			while (this.playerHealth.isInvincible)
			{
				yield return null;
			}
			this.playerHealth.isInvincible = true;
			this._waitCooldownCoroutine = null;
			this.isActive = true;
			this.OnCooldownDone.Invoke();
			yield break;
		}

				public float cooldownTime;

				public UnityEvent OnDamagePrevented;

				public UnityEvent OnCooldownDone;

				private Health playerHealth;

				private IEnumerator _waitCooldownCoroutine;
	}
}
