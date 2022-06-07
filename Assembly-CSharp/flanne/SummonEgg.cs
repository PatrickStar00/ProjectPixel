using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class SummonEgg : MonoBehaviour
	{
				private void Start()
		{
			this.summon.gameObject.SetActive(false);
			base.StartCoroutine(this.WaitToHatchCR());
		}

				private IEnumerator WaitToHatchCR()
		{
			yield return new WaitForSeconds(this.secondsToHatch);
			int num;
			for (int i = 0; i < 3; i = num + 1)
			{
				this.hatchFlasher.Flash();
				yield return new WaitForSeconds(0.2f);
				num = i;
			}
			this.hatchParticles.Play();
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO != null)
			{
				soundEffectSO.Play(null);
			}
			this.summon.gameObject.SetActive(true);
			base.gameObject.SetActive(false);
			yield break;
		}

				[SerializeField]
		private Summon summon;

				[SerializeField]
		private ParticleSystem hatchParticles;

				[SerializeField]
		private FlashSprite hatchFlasher;

				[SerializeField]
		private SoundEffectSO soundFX;

				[SerializeField]
		private float secondsToHatch;
	}
}
