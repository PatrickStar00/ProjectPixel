using System;
using UnityEngine;

namespace flanne
{
		[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
	public class SoundEffectSO : ScriptableObject
	{
				public void SyncPitchAndSemitones()
		{
			if (this.useSemitones)
			{
				this.pitch.x = Mathf.Pow(SoundEffectSO.SEMITONES_TO_PITCH_CONVERSION_UNIT, (float)this.semitones.x);
				this.pitch.y = Mathf.Pow(SoundEffectSO.SEMITONES_TO_PITCH_CONVERSION_UNIT, (float)this.semitones.y);
				return;
			}
			this.semitones.x = Mathf.RoundToInt(Mathf.Log10(this.pitch.x) / Mathf.Log10(SoundEffectSO.SEMITONES_TO_PITCH_CONVERSION_UNIT));
			this.semitones.y = Mathf.RoundToInt(Mathf.Log10(this.pitch.y) / Mathf.Log10(SoundEffectSO.SEMITONES_TO_PITCH_CONVERSION_UNIT));
		}

				private AudioClip GetAudioClip()
		{
			AudioClip result = this.clips[(this.playIndex >= this.clips.Length) ? 0 : this.playIndex];
			switch (this.playOrder)
			{
			case SoundEffectSO.SoundClipPlayOrder.random:
				this.playIndex = Random.Range(0, this.clips.Length);
				break;
			case SoundEffectSO.SoundClipPlayOrder.in_order:
				this.playIndex = (this.playIndex + 1) % this.clips.Length;
				break;
			case SoundEffectSO.SoundClipPlayOrder.reverse:
				this.playIndex = (this.playIndex + this.clips.Length - 1) % this.clips.Length;
				break;
			}
			return result;
		}

				public AudioSource Play(AudioSource audioSourceParam = null)
		{
			if (this.clips.Length == 0)
			{
				Debug.LogError("Missing sound clips for " + base.name);
				return null;
			}
			AudioSource audioSource = audioSourceParam;
			if (audioSourceParam == null)
			{
				audioSource = new GameObject("Sound", new Type[]
				{
					typeof(AudioSource)
				}).GetComponent<AudioSource>();
			}
			audioSource.clip = this.GetAudioClip();
			AudioManager instance = AudioManager.Instance;
			if (instance != null)
			{
				audioSource.volume = instance.SFXVolume * Random.Range(this.volume.x, this.volume.y);
			}
			else
			{
				audioSource.volume = Random.Range(this.volume.x, this.volume.y);
			}
			audioSource.pitch = (this.useSemitones ? Mathf.Pow(SoundEffectSO.SEMITONES_TO_PITCH_CONVERSION_UNIT, (float)Random.Range(this.semitones.x, this.semitones.y)) : Random.Range(this.pitch.x, this.pitch.y));
			audioSource.Play();
			Object.Destroy(audioSource.gameObject, audioSource.clip.length / audioSource.pitch);
			return audioSource;
		}

				private static readonly float SEMITONES_TO_PITCH_CONVERSION_UNIT = 1.05946f;

				public AudioClip[] clips;

				public Vector2 volume = new Vector2(0.5f, 0.5f);

				public bool useSemitones;

				public Vector2Int semitones = new Vector2Int(0, 0);

				public Vector2 pitch = new Vector2(1f, 1f);

				[SerializeField]
		private SoundEffectSO.SoundClipPlayOrder playOrder;

				[SerializeField]
		private int playIndex;

				private enum SoundClipPlayOrder
		{
						random,
						in_order,
						reverse
		}
	}
}
