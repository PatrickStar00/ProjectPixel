using System;

namespace flanne
{
		[Serializable]
	public class UnlockData
	{
				public UnlockData(int size)
		{
			this.unlocks = new bool[size];
		}

				public bool[] unlocks;
	}
}
