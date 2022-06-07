using System;
using CameraShake;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class OptionsSetter : MonoBehaviour
	{
				private void Start()
		{
			this.AM = AudioManager.Instance;
			this.Refresh();
		}

				public void Refresh()
		{
			this.SetBGM(this.AM.MusicVolume);
			this.SetSFX(this.AM.SFXVolume);
			this._resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
			bool fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
			this.SetFullscreen(fullscreen);
			this.SetCameraShake(PlayerPrefs.GetInt("CameraShake", 1) == 1);
		}

				public void OnClickBGMVolume()
		{
			float num = this.AM.MusicVolume;
			num += 0.25f;
			if (num > 1f)
			{
				num = 0f;
			}
			this.SetBGM(num);
		}

				public void OnClickSFXVolume()
		{
			float num = this.AM.SFXVolume;
			num += 0.25f;
			if (num > 1f)
			{
				num = 0f;
			}
			this.SetSFX(num);
		}

				public void OnClickFullscreen()
		{
			this.SetFullscreen(!Screen.fullScreen);
		}

				public void OnClickResolution()
		{
			this._resolutionIndex++;
			if (this._resolutionIndex >= this.SupportedResolutions.Length)
			{
				this._resolutionIndex = 0;
			}
			PlayerPrefs.SetInt("ResolutionIndex", this._resolutionIndex);
			this.SetResolution(this._resolutionIndex);
		}

				public void OnClickCameraShake()
		{
			this.SetCameraShake(!CameraShaker.ShakeOn);
		}

				private void SetBGM(float volume)
		{
			this.bgmTMP.text = string.Format(LocalizationSystem.GetLocalizedValue(this.bgmLabel.key) + " {0:P0}.", volume);
			this.AM.MusicVolume = volume;
		}

				private void SetSFX(float volume)
		{
			this.sfxTMP.text = string.Format(LocalizationSystem.GetLocalizedValue(this.sfxLabel.key) + " {0:P0}.", volume);
			this.AM.SFXVolume = volume;
		}

				private void SetFullscreen(bool isFS)
		{
			string text = LocalizationSystem.GetLocalizedValue(this.fullscreenLabel.key) + " ";
			if (isFS)
			{
				text += LocalizationSystem.GetLocalizedValue(this.onString.key);
			}
			else
			{
				text += LocalizationSystem.GetLocalizedValue(this.offString.key);
			}
			this.fullscreenTMP.text = text;
			if (isFS)
			{
				this.DisableResolution();
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
			}
			else
			{
				this.resolutionButton.interactable = true;
				this.SetResolution(this._resolutionIndex);
			}
			PlayerPrefs.SetInt("Fullscreen", isFS ? 1 : 0);
		}

				private void SetResolution(int resolutionIndex)
		{
			Vector2Int vector2Int = this.SupportedResolutions[resolutionIndex];
			Screen.SetResolution(vector2Int.x, vector2Int.y, false);
			string text = LocalizationSystem.GetLocalizedValue(this.resolutionLabel.key) + " ";
			text = string.Concat(new object[]
			{
				text,
				vector2Int.x,
				"x",
				vector2Int.y
			});
			this.resolutionTMP.text = text;
		}

				private void DisableResolution()
		{
			this.resolutionButton.interactable = false;
			string text = LocalizationSystem.GetLocalizedValue(this.resolutionLabel.key) + " ";
			text += "-";
			this.resolutionTMP.text = text;
		}

				private void SetCameraShake(bool isOn)
		{
			string text = LocalizationSystem.GetLocalizedValue(this.cameraShakeLabel.key) + " ";
			if (isOn)
			{
				text += LocalizationSystem.GetLocalizedValue(this.onString.key);
			}
			else
			{
				text += LocalizationSystem.GetLocalizedValue(this.offString.key);
			}
			this.cameraShakeTMP.text = text;
			CameraShaker.ShakeOn = isOn;
			PlayerPrefs.SetInt("CameraShake", isOn ? 1 : 0);
		}

				[SerializeField]
		private LocalizedString bgmLabel;

				[SerializeField]
		private LocalizedString sfxLabel;

				[SerializeField]
		private LocalizedString fullscreenLabel;

				[SerializeField]
		private LocalizedString resolutionLabel;

				[SerializeField]
		private LocalizedString cameraShakeLabel;

				[SerializeField]
		private LocalizedString onString;

				[SerializeField]
		private LocalizedString offString;

				[SerializeField]
		private TMP_Text bgmTMP;

				[SerializeField]
		private TMP_Text sfxTMP;

				[SerializeField]
		private TMP_Text fullscreenTMP;

				[SerializeField]
		private TMP_Text resolutionTMP;

				[SerializeField]
		private TMP_Text cameraShakeTMP;

				[SerializeField]
		private Button resolutionButton;

				private Vector2Int[] SupportedResolutions = new Vector2Int[]
		{
			new Vector2Int(800, 450),
			new Vector2Int(1200, 675),
			new Vector2Int(1600, 900),
			new Vector2Int(1920, 1080)
		};

				private int _resolutionIndex;

				private AudioManager AM;
	}
}
