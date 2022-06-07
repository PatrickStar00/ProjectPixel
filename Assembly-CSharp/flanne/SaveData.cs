using System;

namespace flanne
{
		[Serializable]
	public class SaveData
	{
				public int points;

				public UnlockData characterUnlocks;

				public UnlockData gunUnlocks;

				public TieredUnlockData runeUnlocks;

				public int[] swordRuneSelections;

				public int[] shieldRuneSelections;
	}
}
