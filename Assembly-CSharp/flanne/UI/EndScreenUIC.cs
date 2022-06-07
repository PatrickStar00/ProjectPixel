﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class EndScreenUIC : MonoBehaviour
	{
				public void Show(bool survived)
		{
			if (survived)
			{
				this.youSurvivedPanel.Show();
			}
			else
			{
				this.youDiedPanel.Show();
			}
			base.StartCoroutine(this.ShowPanelsCR());
		}

				public void Hide()
		{
			this.youSurvivedPanel.Hide();
			this.youDiedPanel.Hide();
			Panel[] array = this.scorePanels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Hide();
			}
			this.quitButtonPanel.Hide();
		}

				public void SetScores(Score score)
		{
			this.timeSurvivedTMP.text = "Time Survived <color=#F5D6C1>(" + score.timeSurvivedString + ")</color>";
			this.timeSurvivedScoreTMP.text = score.timeSurvivedScore.ToString();
			this.enemiesKilledTMP.text = "Enemies Killed <color=#F5D6C1>(" + score.enemiesKilled + ")</color>";
			this.enemiesKilledScoreTMP.text = score.enemiesKilledScore.ToString();
			this.levelsEarnedTMP.text = "Levels Earned <color=#F5D6C1>(" + score.levelsEarned + ")</color>";
			this.levelsEarnedScoreTMP.text = score.levelsEarnedScore.ToString();
			this.totalScoreTMP.text = score.totalScore.ToString();
		}

				private IEnumerator ShowPanelsCR()
		{
			yield return new WaitForSecondsRealtime(1.5f);
			Panel[] array = this.scorePanels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Show();
				yield return new WaitForSecondsRealtime(0.1f);
			}
			array = null;
			yield return new WaitForSecondsRealtime(1f);
			this.screenCoverPanel.Show();
			this.quitButtonPanel.Show();
			yield break;
		}

				[SerializeField]
		private Panel youDiedPanel;

				[SerializeField]
		private Panel youSurvivedPanel;

				[SerializeField]
		private Panel screenCoverPanel;

				[SerializeField]
		private Panel[] scorePanels;

				[SerializeField]
		private Panel quitButtonPanel;

				[SerializeField]
		private TMP_Text timeSurvivedTMP;

				[SerializeField]
		private TMP_Text timeSurvivedScoreTMP;

				[SerializeField]
		private TMP_Text enemiesKilledTMP;

				[SerializeField]
		private TMP_Text enemiesKilledScoreTMP;

				[SerializeField]
		private TMP_Text levelsEarnedTMP;

				[SerializeField]
		private TMP_Text levelsEarnedScoreTMP;

				[SerializeField]
		private TMP_Text totalScoreTMP;
	}
}
