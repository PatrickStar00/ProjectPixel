using System;
using UnityEngine;

namespace flanne.Player
{
		public class PlayerXP : MonoBehaviour
	{
								public int level { get; private set; }

						private int xpToLevel
		{
			get
			{
				int num = this.level + 1;
				if (num < 20)
				{
					return num * 10 - 5;
				}
				if (num > 20 && num < 40)
				{
					return num * 13 - 6;
				}
				return num * 16 - 8;
			}
		}

				private void Awake()
		{
			this.xp = 0;
			this.level = 1;
			this.OnXPToLevelChanged.Invoke(this.xpToLevel);
		}

				public void GainXP(int amount)
		{
			this.xp += amount;
			if (this.xp > this.xpToLevel)
			{
				this.xp -= this.xpToLevel;
				int level = this.level;
				this.level = level + 1;
				this.OnLevelChanged.Invoke(this.level);
				this.OnXPToLevelChanged.Invoke(this.xpToLevel);
			}
			this.OnXPChanged.Invoke(this.xp);
		}

				public UnityIntEvent OnXPChanged;

				public UnityIntEvent OnXPToLevelChanged;

				public UnityIntEvent OnLevelChanged;

				private int xp;
	}
}
