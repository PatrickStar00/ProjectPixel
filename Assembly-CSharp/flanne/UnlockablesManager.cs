using System;
using UnityEngine;

namespace flanne
{
		public class UnlockablesManager : MonoBehaviour
	{
						public UnlockData unlockData
		{
			get
			{
				UnlockData unlockData = new UnlockData(this.unlockables.Length);
				for (int i = 0; i < this.unlockables.Length; i++)
				{
					unlockData.unlocks[i] = !this.unlockables[i].IsLocked;
				}
				return unlockData;
			}
		}

				public void LoadData(UnlockData data)
		{
			if (data == null)
			{
				data = new UnlockData(this.unlockables.Length);
			}
			if (data.unlocks == null)
			{
				data.unlocks = new bool[this.unlockables.Length];
			}
			if (data.unlocks.Length != this.unlockables.Length)
			{
				Array.Resize<bool>(ref data.unlocks, this.unlockables.Length);
			}
			for (int i = 0; i < this.unlockables.Length; i++)
			{
				if (data.unlocks[i])
				{
					this.unlockables[i].Unlock();
				}
				else
				{
					this.unlockables[i].Lock();
				}
			}
		}

				[SerializeField]
		private Unlockable[] unlockables;
	}
}
