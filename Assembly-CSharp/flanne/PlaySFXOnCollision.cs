using System;
using UnityEngine;

namespace flanne
{
		public class PlaySFXOnCollision : MonoBehaviour
	{
				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				this.soundFX.Play(null);
			}
		}

				[SerializeField]
		private string hitTag;

				[SerializeField]
		private SoundEffectSO soundFX;
	}
}
