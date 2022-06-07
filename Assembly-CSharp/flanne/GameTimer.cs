using System;
using UnityEngine;

namespace flanne
{
		public class GameTimer : MonoBehaviour
	{
								public float timer { get; private set; }

				private void Awake()
		{
			GameTimer.SharedInstance = this;
		}

				private void Update()
		{
			if (this._isPlaying)
			{
				this.timer += Time.deltaTime;
				if (this.timer >= this.timeLimit)
				{
					this.PostNotification(GameTimer.TimeLimitNotification, null);
					this.timer = this.timeLimit;
				}
			}
		}

				public void Start()
		{
			this._isPlaying = true;
		}

				public void Stop()
		{
			this._isPlaying = false;
		}

				public string TimeToString()
		{
			int num = Mathf.FloorToInt(this.timer / 60f);
			int num2 = Mathf.FloorToInt(this.timer % 60f);
			return num.ToString("00") + ":" + num2.ToString("00");
		}

				public string TimeRemainingToString()
		{
			float num = this.timeLimit - this.timer;
			int num2 = Mathf.FloorToInt(num / 60f);
			int num3 = Mathf.FloorToInt(num % 60f);
			return num2.ToString("00") + ":" + num3.ToString("00");
		}

				public static GameTimer SharedInstance;

				public static string TimeLimitNotification = "GameTimer.TimeLimitNotification";

				[NonSerialized]
		public float timeLimit = -1f;

				private bool _isPlaying = true;
	}
}
