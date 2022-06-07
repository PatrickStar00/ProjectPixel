using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class FlashSprite : MonoBehaviour
	{
				private void Start()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.originalMaterial = this.spriteRenderer.material;
		}

				private void OnDisable()
		{
			if (this.flashRoutine != null)
			{
				base.StopCoroutine(this.flashRoutine);
				this.spriteRenderer.material = this.originalMaterial;
				this.flashRoutine = null;
			}
		}

				public void Flash()
		{
			if (this.flashRoutine != null)
			{
				base.StopCoroutine(this.flashRoutine);
			}
			if (base.gameObject.activeSelf)
			{
				this.flashRoutine = base.StartCoroutine(this.FlashRoutine());
			}
		}

				private IEnumerator FlashRoutine()
		{
			if (this.spriteRenderer != null)
			{
				this.spriteRenderer.material = this.flashMaterial;
				yield return new WaitForSeconds(this.duration);
				this.spriteRenderer.material = this.originalMaterial;
				this.flashRoutine = null;
			}
			yield break;
		}

				[Tooltip("Material to switch to during the flash.")]
		[SerializeField]
		private Material flashMaterial;

				[Tooltip("Duration of the flash.")]
		[SerializeField]
		private float duration;

				private SpriteRenderer spriteRenderer;

				private Material originalMaterial;

				private Coroutine flashRoutine;
	}
}
