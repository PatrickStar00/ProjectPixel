using System;
using UnityEngine;

namespace flanne
{
		public class FreezeOnCollision : MonoBehaviour
	{
				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				FreezeSystem.SharedInstance.Freeze(other.gameObject, this.duration);
			}
		}

				[SerializeField]
		private string hitTag;

				[SerializeField]
		private float duration;
	}
}
