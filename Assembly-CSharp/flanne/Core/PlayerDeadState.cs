using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.Core
{
    public class PlayerDeadState : GameState
    {
        private void OnClick()
        {
            this.owner.ChangeState<TransitionToTitleState>();
        }

        public override void Enter()
        {
            GameTimer.SharedInstance.Stop();
            AIController.SharedInstance.playerRepel = true;
            base.playerCameraRig.enabled = false;
            base.StartCoroutine(this.LoseVisionCR());
            base.quitToTitleButton.onClick.AddListener(new UnityAction(this.OnClick));
            AudioManager.Instance.SetLowPassFilter(true);
        }

        public override void Exit()
        {
            base.quitToTitleButton.onClick.RemoveListener(new UnityAction(this.OnClick));
            base.powerupListUI.Hide();
            AudioManager.Instance.SetLowPassFilter(false);
        }

        private IEnumerator LoseVisionCR()
        {
            LeanTween.scale(base.playerFogRevealer, new Vector3(0.7f, 0.7f, 1f), 1f).setEase(LeanTweenType.easeOutBack);
            yield return new WaitForSeconds(1.5f);
            LeanTween.scale(base.playerFogRevealer, new Vector3(0f, 0f, 1f), 1f).setEase(LeanTweenType.easeInCubic);
            AudioManager.Instance.FadeOutMusic(0.5f);
            yield return new WaitForSeconds(0.5f);
            base.hud.Hide();
            base.youDiedSFX.Play(null);
            Score score = ScoreCalculator.SharedInstance.GetScore();
            base.endScreenUIC.SetScores(score);
            base.endScreenUIC.Show(false);
            base.powerupListUI.Show();
            PauseController.SharedInstance.Pause();
            yield break;
        }
    }
}
