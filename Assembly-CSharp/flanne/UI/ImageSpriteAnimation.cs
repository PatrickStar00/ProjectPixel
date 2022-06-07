using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class ImageSpriteAnimation : MonoBehaviour
	{
				private void OnEnable()
		{
			this._coroutine = this.Play();
			base.StartCoroutine(this._coroutine);
		}

				private void OnDisable()
		{
			base.StopCoroutine(this._coroutine);
		}

				private IEnumerator Play()
		{
			int num;
			for (int i = 0; i < this.sprites.Length; i = num + 1)
			{
				this.image.sprite = this.sprites[i];
				yield return new WaitForSecondsRealtime(this.secPerFrame);
				num = i;
			}
			while (this.isLooping)
			{
				yield return new WaitForSecondsRealtime(this.delayBetweenLoops);
				for (int i = 0; i < this.sprites.Length; i = num + 1)
				{
					this.image.sprite = this.sprites[i];
					yield return new WaitForSecondsRealtime(this.secPerFrame);
					num = i;
				}
			}
			yield break;
		}

				[SerializeField]
		private Image image;

				[SerializeField]
		private float secPerFrame;

				[SerializeField]
		private Sprite[] sprites;

				[SerializeField]
		private bool isLooping;

				[SerializeField]
		private float delayBetweenLoops;

				private IEnumerator _coroutine;
	}
}
