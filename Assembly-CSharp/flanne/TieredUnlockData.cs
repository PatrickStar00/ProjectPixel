using System;

namespace flanne
{
		[Serializable]
	public class TieredUnlockData
	{
				public TieredUnlockData(int size)
		{
			this.unlocks = new int[size];
		}

				public int[] unlocks;
	}
}
