using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class SpriteFX : MonoBehaviour
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
				this.spriteRenderer.sprite = this.sprites[i];
				yield return new WaitForSeconds(this.secPerFrame);
				num = i;
			}
			while (this.loop)
			{
				for (int i = 0; i < this.sprites.Length; i = num + 1)
				{
					this.spriteRenderer.sprite = this.sprites[i];
					yield return new WaitForSeconds(this.secPerFrame);
					num = i;
				}
			}
			yield break;
		}

				[SerializeField]
		private SpriteRenderer spriteRenderer;

				[SerializeField]
		private float secPerFrame;

				[SerializeField]
		private bool loop;

				[SerializeField]
		private Sprite[] sprites;

				private IEnumerator _coroutine;
	}
}
