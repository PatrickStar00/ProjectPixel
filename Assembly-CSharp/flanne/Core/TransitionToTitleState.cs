using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace flanne.Core
{
		public class TransitionToTitleState : GameState
	{
				public override void Enter()
		{
			base.StartCoroutine(this.WaitToExitToTitleScreen());
			this.Save();
		}

				private void Save()
		{
			PointsTracker.pts += ScoreCalculator.SharedInstance.GetScore().totalScore;
			if (SaveSystem.data != null)
			{
				SaveSystem.data.points = PointsTracker.pts;
				SaveSystem.Save();
			}
		}

				private IEnumerator WaitToExitToTitleScreen()
		{
			base.endScreenUIC.Hide();
			yield return new WaitForSecondsRealtime(1.5f);
			PauseController.SharedInstance.UnPause();
			SceneManager.LoadScene("TitleScreen", 0);
			yield break;
		}
	}
}
