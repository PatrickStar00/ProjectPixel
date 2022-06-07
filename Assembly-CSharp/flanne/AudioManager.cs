using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class AudioManager : MonoBehaviour
	{
								public float MusicVolume
		{
			get
			{
				return this._musicVolume;
			}
			set
			{
				this._musicVolume = value;
				this.musicSource.volume = this._musicVolume;
				PlayerPrefs.SetFloat("MusicVolume", this._musicVolume);
			}
		}

								public float SFXVolume
		{
			get
			{
				return this._sfxVolume;
			}
			set
			{
				this._sfxVolume = value;
				PlayerPrefs.SetFloat("SFXVolume", this._sfxVolume);
			}
		}

				private void Awake()
		{
			if (AudioManager.Instance == null)
			{
				AudioManager.Instance = this;
			}
			else if (AudioManager.Instance != this)
			{
				Object.Destroy(base.gameObject);
			}
			Object.DontDestroyOnLoad(base.gameObject);
			this.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
			this.SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
		}

				public void FadeInMusic(float fadeDuration)
		{
			this.StopMusicFade();
			this._musicFadeCR = this.FadeInMusicCR(fadeDuration);
			base.StartCoroutine(this._musicFadeCR);
		}

				public void FadeOutMusic(float fadeDuration)
		{
			this.StopMusicFade();
			this._musicFadeCR = this.FadeOutMusicCR(fadeDuration);
			base.StartCoroutine(this._musicFadeCR);
		}

				public void PlayMusic(AudioClip clip)
		{
			this.musicSource.clip = clip;
			this.musicSource.Play();
		}

				public void SetLowPassFilter(bool isOn)
		{
			this.musicLowPassFilter.enabled = isOn;
		}

				private void StopMusicFade()
		{
			if (this._musicFadeCR != null)
			{
				base.StopCoroutine(this._musicFadeCR);
				this._musicFadeCR = null;
				this.musicSource.volume = this._musicVolume;
			}
		}

				private IEnumerator FadeInMusicCR(float fadeDuration)
		{
			this.musicSource.volume = 0f;
			while (this.musicSource.volume < this._musicVolume)
			{
				this.musicSource.volume += this._musicVolume * Time.unscaledDeltaTime / fadeDuration;
				yield return null;
			}
			this.musicSource.volume = this._musicVolume;
			this._musicFadeCR = null;
			yield break;
		}

				private IEnumerator FadeOutMusicCR(float fadeDuration)
		{
			while (this.musicSource.volume > 0f)
			{
				this.musicSource.volume -= this._musicVolume * Time.unscaledDeltaTime / fadeDuration;
				yield return null;
			}
			this.musicSource.Stop();
			this.musicSource.volume = this._musicVolume;
			this._musicFadeCR = null;
			yield break;
		}

				public static AudioManager Instance;

				[SerializeField]
		private AudioSource musicSource;

				[SerializeField]
		private AudioLowPassFilter musicLowPassFilter;

				private float _musicVolume;

				private float _sfxVolume;

				private IEnumerator _musicFadeCR;
	}
}
