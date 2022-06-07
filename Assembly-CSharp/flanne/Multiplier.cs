using System;
using UnityEngine;

namespace flanne
{
		public class Multiplier
	{
				// (add) Token: 0x060004AC RID: 1196 RVA: 0x00017A4C File Offset: 0x00015C4C
		// (remove) Token: 0x060004AD RID: 1197 RVA: 0x00017A84 File Offset: 0x00015C84
		public event EventHandler<float> ChangedEvent;

						public float value
		{
			get
			{
				return (1f + this.bonus) * this.reduction;
			}
		}

				public void Increase(float amount)
		{
			if (amount < 0f)
			{
				this.Decrease(Mathf.Abs(amount));
				return;
			}
			this.bonus += amount;
			EventHandler<float> changedEvent = this.ChangedEvent;
			if (changedEvent == null)
			{
				return;
			}
			changedEvent(this, this.value);
		}

				public void Decrease(float amount)
		{
			if (amount < 0f)
			{
				this.Decrease(Mathf.Abs(amount));
				return;
			}
			this.reduction *= 1f - amount;
			EventHandler<float> changedEvent = this.ChangedEvent;
			if (changedEvent == null)
			{
				return;
			}
			changedEvent(this, this.value);
		}

				public void ChangeBonus(float amount)
		{
			this.bonus += amount;
			EventHandler<float> changedEvent = this.ChangedEvent;
			if (changedEvent == null)
			{
				return;
			}
			changedEvent(this, this.value);
		}

				private float bonus;

				private float reduction = 1f;
	}
}
