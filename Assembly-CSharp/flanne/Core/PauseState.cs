using System;
using UnityEngine.Events;

namespace flanne.Core
{
		public class PauseState : GameState
	{
				private void OnResume()
		{
			this.owner.ChangeState<CombatState>();
			base.pauseController.UnPause();
			base.powerupListUI.Hide();
			AudioManager.Instance.SetLowPassFilter(false);
		}

				private void OnOptions()
		{
			this.owner.ChangeState<OptionsState>();
		}

				private void OnGiveUp()
		{
			this.owner.ChangeState<CombatState>();
			base.playerHealth.AutoKill();
			base.pauseController.UnPause();
			base.powerupListUI.Hide();
			AudioManager.Instance.SetLowPassFilter(false);
		}

				public override void Enter()
		{
			base.pauseController.Pause();
			AudioManager.Instance.SetLowPassFilter(true);
			base.pauseMenu.Show();
			base.powerupListUI.Show();
			base.pauseResumeButton.onClick.AddListener(new UnityAction(this.OnResume));
			base.optionsButton.onClick.AddListener(new UnityAction(this.OnOptions));
			base.giveupButton.onClick.AddListener(new UnityAction(this.OnGiveUp));
		}

				public override void Exit()
		{
			base.pauseMenu.Hide();
			base.pauseResumeButton.onClick.RemoveListener(new UnityAction(this.OnResume));
			base.optionsButton.onClick.RemoveListener(new UnityAction(this.OnOptions));
			base.giveupButton.onClick.RemoveListener(new UnityAction(this.OnGiveUp));
		}
	}
}
