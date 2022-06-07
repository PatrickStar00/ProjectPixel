using System;
using System.Collections;
using UnityEngine;

namespace flanne.Core
{
		public class InitState : GameState
	{
				public override void Enter()
		{
			PauseController.SharedInstance.Pause();
			base.StartCoroutine(this.WaitToShowStartBattle());
			AudioManager.Instance.SetLowPassFilter(true);
			AudioManager.Instance.PlayMusic(base.battleMusic);
			AudioManager.Instance.FadeInMusic(0.5f);
		}

				public override void Exit()
		{
			PauseController.SharedInstance.UnPause();
			AudioManager.Instance.SetLowPassFilter(false);
		}

				private IEnumerator WaitToShowStartBattle()
		{
			yield return new WaitForSecondsRealtime(0.5f);
			this.fogRevealTweenID = LeanTween.scale(base.playerFogRevealer, new Vector3(0.5f, 0.5f, 1f), 0.5f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true).id;
			while (LeanTween.isTweening(this.fogRevealTweenID))
			{
				yield return null;
			}
			this.fogRevealTweenID = LeanTween.scale(base.playerFogRevealer, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutCubic).setIgnoreTimeScale(true).id;
			while (LeanTween.isTweening(this.fogRevealTweenID))
			{
				yield return null;
			}
			base.hud.Show();
			this.owner.ChangeState<CombatState>();
			yield break;
		}

				private int fogRevealTweenID;
	}
}
