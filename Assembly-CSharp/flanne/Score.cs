using System;

namespace flanne
{
		public class Score
	{
						public int totalScore
		{
			get
			{
				return this.timeSurvivedScore + this.enemiesKilledScore + this.levelsEarnedScore;
			}
		}

				public string timeSurvivedString;

				public int timeSurvivedScore;

				public int enemiesKilled;

				public int enemiesKilledScore;

				public int levelsEarned;

				public int levelsEarnedScore;
	}
}
