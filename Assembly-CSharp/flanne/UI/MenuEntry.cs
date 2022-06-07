using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace flanne.UI
{
		public class MenuEntry : Button, ICancelHandler, IEventSystemHandler
	{
				public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			this.Select();
		}

				public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			if (this.onSelect != null)
			{
				this.onSelect.Invoke();
			}
		}

				public void OnCancel(BaseEventData eventData)
		{
			if (this.onCancel != null)
			{
				this.onCancel.Invoke();
			}
		}

				public UnityEvent onSelect;

				public UnityEvent onCancel;
	}
}
