using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class OnDisableEvent : MonoBehaviour
	{
				private void OnDisable()
		{
			UnityEvent unityEvent = this.onDisableEvent;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

				[SerializeField]
		private UnityEvent onDisableEvent;
	}
}
