using System;
using UnityEngine;

namespace flanne
{
		public class TieredUnlockManager : MonoBehaviour
	{
						public TieredUnlockData unlockData
		{
			get
			{
				TieredUnlockData tieredUnlockData = new TieredUnlockData(this.unlockables.Length);
				for (int i = 0; i < this.unlockables.Length; i++)
				{
					tieredUnlockData.unlocks[i] = this.unlockables[i].level;
				}
				return tieredUnlockData;
			}
		}

				public void LoadData(TieredUnlockData data)
		{
			if (data == null)
			{
				data = new TieredUnlockData(this.unlockables.Length);
			}
			if (data.unlocks == null)
			{
				data.unlocks = new int[this.unlockables.Length];
			}
			if (data.unlocks.Length != this.unlockables.Length)
			{
				Array.Resize<int>(ref data.unlocks, this.unlockables.Length);
			}
			for (int i = 0; i < this.unlockables.Length; i++)
			{
				this.unlockables[i].level = data.unlocks[i];
			}
		}

				[SerializeField]
		[SerializeReference]
		private TieredUnlockable[] unlockables;
	}
}
