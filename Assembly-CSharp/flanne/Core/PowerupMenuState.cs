using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.Core
{
		public class PowerupMenuState : GameState
	{
				private void OnConfirm(object sender, Powerup e)
		{
			base.StartCoroutine(this.EndLevelUpAnimationCR());
			e.ApplyAndNotify(base.player);
			if (!e.isRepeatable)
			{
				base.powerupGenerator.RemoveFromPool(e);
			}
		}

				private void OnReroll()
		{
			this.GeneratePowerups();
			base.powerupRerollButton.gameObject.SetActive(false);
			base.powerupMenuPanel.SelectDefault();
		}

				public override void Enter()
		{
			base.pauseController.Pause();
			base.StartCoroutine(this.PlayLevelUpAnimationCR());
			AudioManager.Instance.SetLowPassFilter(true);
		}

				public override void Exit()
		{
			base.pauseController.UnPause();
			AudioManager.Instance.SetLowPassFilter(false);
		}

				private void GeneratePowerups()
		{
			this.powerupChoices = base.powerupGenerator.GetRandom(5);
			for (int i = 0; i < this.powerupChoices.Count; i++)
			{
				base.powerupMenu.SetData(i, this.powerupChoices[i]);
			}
		}

				private IEnumerator PlayLevelUpAnimationCR()
		{
			base.screenFlash.Flash(1);
			base.levelupAnimator.SetTrigger("Start");
			LeanTween.scale(base.playerFogRevealer, new Vector3(2f, 2f, 1f), 0.7f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);
			yield return new WaitForSecondsRealtime(0.6f);
			base.screenFlash.Flash(1);
			base.powerupMenuSFX.Play(null);
			yield return new WaitForSecondsRealtime(0.1f);
			this.GeneratePowerups();
			base.powerupMenuPanel.Show();
			base.powerupMenu.ConfirmEvent += this.OnConfirm;
			base.powerupRerollButton.onClick.AddListener(new UnityAction(this.OnReroll));
			if (PowerupGenerator.CanReroll)
			{
				base.powerupRerollButton.gameObject.SetActive(true);
			}
			yield break;
		}

				private IEnumerator EndLevelUpAnimationCR()
		{
			base.levelupAnimator.SetTrigger("End");
			LeanTween.scale(base.playerFogRevealer, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.easeOutCubic).setIgnoreTimeScale(true);
			base.powerupMenu.ConfirmEvent -= this.OnConfirm;
			base.powerupRerollButton.onClick.RemoveListener(new UnityAction(this.OnReroll));
			base.powerupRerollButton.gameObject.SetActive(false);
			base.powerupMenuPanel.Hide();
			yield return new WaitForSecondsRealtime(0.5f);
			if (!base.playerHealth.isDead)
			{
				this.owner.ChangeState<CombatState>();
			}
			else
			{
				this.owner.ChangeState<PlayerDeadState>();
			}
			yield break;
		}

				private List<Powerup> powerupChoices;
	}
}
