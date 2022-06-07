using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class TimerUI : MonoBehaviour
	{
				private void Start()
		{
			this.gameTimer = GameTimer.SharedInstance;
		}

				private void Update()
		{
			this.TimerTMP.text = this.gameTimer.TimeRemainingToString();
		}

				public TMP_Text TimerTMP;

				private GameTimer gameTimer;
	}
}
