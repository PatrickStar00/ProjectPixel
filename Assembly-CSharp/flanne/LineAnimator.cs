using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class LineAnimator : MonoBehaviour
	{
				private void Start()
		{
			base.StartCoroutine(this.PlayCR());
		}

				private IEnumerator PlayCR()
		{
			int num;
			for (int i = 0; i < this.textures.Length; i = num + 1)
			{
				this.lineRenderer.material.SetTexture("_MainTex", this.textures[i]);
				yield return new WaitForSeconds(this.secPerFrame);
				num = i;
			}
			while (this.loop)
			{
				for (int i = 0; i < this.textures.Length; i = num + 1)
				{
					this.lineRenderer.material.SetTexture("_MainTex", this.textures[i]);
					yield return new WaitForSeconds(this.secPerFrame);
					num = i;
				}
			}
			yield break;
		}

				[SerializeField]
		private LineRenderer lineRenderer;

				[SerializeField]
		private Texture[] textures;

				[SerializeField]
		private float secPerFrame;

				[SerializeField]
		private bool loop;
	}
}
