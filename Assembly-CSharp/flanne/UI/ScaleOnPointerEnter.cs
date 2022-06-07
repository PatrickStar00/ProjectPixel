using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace flanne.UI
{
		public class ScaleOnPointerEnter : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
				public void Awake()
		{
			this._originalScale = base.transform.localScale;
		}

				public void OnPointerEnter(PointerEventData pointerEventData)
		{
			base.transform.localScale = this.scaleTo;
		}

				public void OnPointerExit(PointerEventData pointerEventData)
		{
			base.transform.localScale = this._originalScale;
		}

				[SerializeField]
		private Vector3 scaleTo;

				private Vector3 _originalScale;
	}
}
