using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace flanne.UI
{
		[RequireComponent(typeof(Selectable))]
	public class SelectOnHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
	{
				public void OnPointerEnter(PointerEventData eventData)
		{
			this.selectable.Select();
		}

				private void Start()
		{
			this.selectable = base.GetComponent<Selectable>();
		}

				private Selectable selectable;
	}
}
