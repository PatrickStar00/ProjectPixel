using System;

namespace flanne
{
		public class BoolToggle
	{
				// (add) Token: 0x06000481 RID: 1153 RVA: 0x0001706C File Offset: 0x0001526C
		// (remove) Token: 0x06000482 RID: 1154 RVA: 0x000170A4 File Offset: 0x000152A4
		public event EventHandler<bool> ToggleEvent;

						public bool value
		{
			get
			{
				if (this._flip > 0)
				{
					return !this.defaultValue;
				}
				return this.defaultValue;
			}
		}

								public bool defaultValue { get; private set; }

				public BoolToggle(bool b)
		{
			this.defaultValue = b;
		}

				public void Flip()
		{
			this._flip++;
			if (this.ToggleEvent != null)
			{
				this.ToggleEvent(this, this.value);
			}
		}

				public void UnFlip()
		{
			this._flip--;
			if (this.ToggleEvent != null)
			{
				this.ToggleEvent(this, this.value);
			}
		}

				private int _flip;
	}
}
