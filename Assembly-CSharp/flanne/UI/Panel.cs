using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.UI
{
		[RequireComponent(typeof(CanvasGroup))]
	public class Panel : MonoBehaviour
	{
								public bool interactable
		{
			get
			{
				return this.canvasGroup.interactable;
			}
			set
			{
				this.canvasGroup.interactable = value;
				this.canvasGroup.blocksRaycasts = value;
			}
		}

				protected virtual void Start()
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			this.canvasGroup.interactable = false;
			this.canvasGroup.blocksRaycasts = false;
			base.StartCoroutine(this.DelayStartCR());
		}

				public virtual void Show()
		{
			this.canvasGroup.interactable = true;
			this.canvasGroup.blocksRaycasts = true;
			UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Show();
			}
			if (Gamepad.current != null)
			{
				Selectable selectable = this.gamepadDefaultSelectable;
				if (selectable == null)
				{
					return;
				}
				selectable.Select();
			}
		}

				public virtual void Hide()
		{
			this.canvasGroup.interactable = false;
			this.canvasGroup.blocksRaycasts = false;
			UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Hide();
			}
		}

				public void SelectDefault()
		{
			Selectable selectable = this.gamepadDefaultSelectable;
			if (selectable == null)
			{
				return;
			}
			selectable.Select();
		}

				private IEnumerator DelayStartCR()
		{
			yield return null;
			UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetOff();
			}
			yield break;
		}

				[SerializeField]
		private Selectable gamepadDefaultSelectable;

				private CanvasGroup canvasGroup;
	}
}
