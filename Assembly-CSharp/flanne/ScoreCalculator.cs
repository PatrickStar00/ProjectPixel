using System;
using flanne.Player;
using UnityEngine;

namespace flanne
{
		public class ScoreCalculator : MonoBehaviour
	{
				private void OnDeath(object sender, object args)
		{
			if ((sender as Health).gameObject.tag == "Enemy")
			{
				this._enemiesKilled++;
			}
		}

				private void Awake()
		{
			ScoreCalculator.SharedInstance = this;
		}

				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				public Score GetScore()
		{
			return new Score
			{
				timeSurvivedString = this.gameTimer.TimeToString(),
				timeSurvivedScore = Mathf.CeilToInt(this.gameTimer.timer),
				enemiesKilled = this._enemiesKilled,
				enemiesKilledScore = this._enemiesKilled,
				levelsEarned = this.playerXP.level - 1,
				levelsEarnedScore = (this.playerXP.level - 1) * 100
			};
		}

				public static ScoreCalculator SharedInstance;

				[SerializeField]
		private GameTimer gameTimer;

				[SerializeField]
		private PlayerXP playerXP;

				private int _enemiesKilled;
	}
}
