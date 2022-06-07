using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace flanne.UI
{
		public class SelectorImage : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
	{
				public void OnSelect(BaseEventData eventData)
		{
			this.img.enabled = true;
		}

				public void OnDeselect(BaseEventData eventData)
		{
			this.img.enabled = false;
		}

				[SerializeField]
		private Image img;
	}
}
