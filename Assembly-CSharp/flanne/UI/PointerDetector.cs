using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace flanne.UI
{
		public class PointerDetector : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
				public void OnPointerEnter(PointerEventData pointerEventData)
		{
			this.onEnter.Invoke();
		}

				public void OnPointerExit(PointerEventData pointerEventData)
		{
			this.onExit.Invoke();
		}

				public UnityEvent onEnter;

				public UnityEvent onExit;
	}
}
