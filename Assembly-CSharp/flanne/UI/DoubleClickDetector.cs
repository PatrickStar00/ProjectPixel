using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace flanne.UI
{
		public class DoubleClickDetector : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
				public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				UnityEvent unityEvent = this.onDoubleClick;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

				public UnityEvent onDoubleClick;
	}
}
