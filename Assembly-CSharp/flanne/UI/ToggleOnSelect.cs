using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace flanne.UI
{
		[RequireComponent(typeof(Toggle))]
	public class ToggleOnSelect : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
				private void Start()
		{
			this.toggle = base.GetComponent<Toggle>();
		}

				public void OnSelect(BaseEventData eventData)
		{
			this.toggle.isOn = true;
		}

				private Toggle toggle;
	}
}
