using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class OnCollisionEvent : MonoBehaviour
	{
				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.targetTag))
			{
				UnityEvent unityEvent = this.onCollision;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

				[SerializeField]
		private string targetTag;

				[SerializeField]
		private UnityEvent onCollision;
	}
}
