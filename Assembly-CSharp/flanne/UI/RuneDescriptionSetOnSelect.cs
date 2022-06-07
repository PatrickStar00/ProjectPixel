using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace flanne.UI
{
		public class RuneDescriptionSetOnSelect : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
				public void OnSelect(BaseEventData eventData)
		{
			this.runeDescription.data = this.runeIcon.data;
		}

				[SerializeField]
		private RuneIcon runeIcon;

				[SerializeField]
		private RuneDescription runeDescription;
	}
}
