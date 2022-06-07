using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace flanne.UI
{
		public class ChestUIController : MonoBehaviour
	{
				private void OnTakeClick()
		{
			EventHandler takeClickEvent = this.TakeClickEvent;
			if (takeClickEvent == null)
			{
				return;
			}
			takeClickEvent(this, null);
		}

				private void OnLeaveClick()
		{
			EventHandler leaveClickEvent = this.LeaveClickEvent;
			if (leaveClickEvent == null)
			{
				return;
			}
			leaveClickEvent(this, null);
		}

				public void SetToPowerup(Powerup powerup)
		{
			this.powerupWidget.SetProperties(new PowerupProperties(powerup));
		}

				public void Show()
		{
			this.chestRenderer.enabled = true;
			this.chestAnimator.Play("A_ChestAnimation", -1, 0f);
			base.StartCoroutine(this.WaitToChestOpen());
			this.takeButton.onClick.AddListener(new UnityAction(this.OnTakeClick));
			this.leaveButton.onClick.AddListener(new UnityAction(this.OnLeaveClick));
		}

				public void Hide()
		{
			this.chestRenderer.enabled = false;
			this.powerupIconPanel.Hide();
			this.powerupDescriptionPanel.Hide();
			this.takeButtonPanel.Hide();
			this.leaveButtonPanel.Hide();
			this.takeButton.onClick.RemoveListener(new UnityAction(this.OnTakeClick));
			this.leaveButton.onClick.RemoveListener(new UnityAction(this.OnLeaveClick));
		}

				private IEnumerator WaitToChestOpen()
		{
			SoundEffectSO soundEffectSO = this.chestLeadupSFX;
			if (soundEffectSO != null)
			{
				soundEffectSO.Play(null);
			}
			yield return new WaitForSecondsRealtime(this.chestOpenTiming);
			SoundEffectSO soundEffectSO2 = this.chestOpenSFX;
			if (soundEffectSO2 != null)
			{
				soundEffectSO2.Play(null);
			}
			this.coinParticles.Play();
			this.powerupIconPanel.Show();
			this.powerupIconPanel.transform.localPosition = Vector3.zero;
			LeanTween.moveLocalY(this.powerupIconPanel.gameObject, 50f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBack);
			this.screenFlash.Flash(3);
			yield return new WaitForSecondsRealtime(0.1f);
			this.powerupDescriptionPanel.Show();
			yield return new WaitForSecondsRealtime(1f);
			this.takeButtonPanel.Show();
			this.leaveButtonPanel.Show();
			yield break;
		}

				public EventHandler TakeClickEvent;

				public EventHandler LeaveClickEvent;

				[SerializeField]
		private ParticleSystem coinParticles;

				[SerializeField]
		private SpriteRenderer chestRenderer;

				[SerializeField]
		private Animator chestAnimator;

				[SerializeField]
		private PowerupWidget powerupWidget;

				[SerializeField]
		private Panel powerupIconPanel;

				[SerializeField]
		private Panel powerupDescriptionPanel;

				[SerializeField]
		private Panel takeButtonPanel;

				[SerializeField]
		private Button takeButton;

				[SerializeField]
		private Panel leaveButtonPanel;

				[SerializeField]
		private Button leaveButton;

				[SerializeField]
		private ScreenFlash screenFlash;

				[SerializeField]
		private SoundEffectSO chestLeadupSFX;

				[SerializeField]
		private SoundEffectSO chestOpenSFX;

				[SerializeField]
		private float chestOpenTiming;
	}
}
