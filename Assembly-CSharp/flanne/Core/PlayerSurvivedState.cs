using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.Core
{
		public class PlayerSurvivedState : GameState
	{
				private void OnClick()
		{
			this.owner.ChangeState<TransitionToTitleState>();
		}

				public override void Enter()
		{
			GameTimer.SharedInstance.Stop();
			base.StartCoroutine(this.KillEnemiesCR());
			base.quitToTitleButton.onClick.AddListener(new UnityAction(this.OnClick));
			AudioManager.Instance.SetLowPassFilter(true);
		}

				public override void Exit()
		{
			base.quitToTitleButton.onClick.RemoveListener(new UnityAction(this.OnClick));
			base.powerupListUI.Hide();
			AudioManager.Instance.SetLowPassFilter(false);
		}

				private IEnumerator KillEnemiesCR()
		{
			base.screenFlash.Flash(1);
			LeanTween.scale(base.playerFogRevealer, new Vector3(5f, 5f, 1f), 0.4f).setEase(LeanTweenType.easeInCubic);
			yield return new WaitForSeconds(0.3f);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < array.Length; i++)
			{
				Health component = array[i].GetComponent<Health>();
				if (component != null)
				{
					component.AutoKill();
				}
			}
			base.screenFlash.Flash(4);
			yield return new WaitForSeconds(1.5f);
			PauseController.SharedInstance.Pause();
			AudioManager.Instance.FadeOutMusic(0.5f);
			yield return new WaitForSecondsRealtime(0.5f);
			base.youSurvivedSFX.Play(null);
			base.hud.Hide();
			Score score = ScoreCalculator.SharedInstance.GetScore();
			base.endScreenUIC.SetScores(score);
			base.endScreenUIC.Show(true);
			base.powerupListUI.Show();
			yield break;
		}
	}
}
