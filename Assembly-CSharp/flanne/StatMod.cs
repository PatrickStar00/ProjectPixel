using System;

namespace flanne
{
		public class StatMod
	{
				// (add) Token: 0x0600062F RID: 1583 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		// (remove) Token: 0x06000630 RID: 1584 RVA: 0x0001C1F0 File Offset: 0x0001A3F0
		public event EventHandler ChangedEvent;

				public float Modify(float baseValue)
		{
			return baseValue * (1f + this._multiplierBonus) * this._multiplierReduction + (float)this._flatBonus;
		}

				public float ModifyInverse(float baseValue)
		{
			return baseValue / ((1f + this._multiplierBonus) * this._multiplierReduction) + (float)this._flatBonus;
		}

				public void AddFlatBonus(int value)
		{
			this._flatBonus += value;
			EventHandler changedEvent = this.ChangedEvent;
			if (changedEvent == null)
			{
				return;
			}
			changedEvent(this, null);
		}

				public void AddMultiplierBonus(float value)
		{
			this._multiplierBonus += value;
			EventHandler changedEvent = this.ChangedEvent;
			if (changedEvent == null)
			{
				return;
			}
			changedEvent(this, null);
		}

				public void AddMultiplierReduction(float value)
		{
			this._multiplierReduction *= value;
			EventHandler changedEvent = this.ChangedEvent;
			if (changedEvent == null)
			{
				return;
			}
			changedEvent(this, null);
		}

				private int _flatBonus;

				private float _multiplierBonus;

				private float _multiplierReduction = 1f;
	}
}
