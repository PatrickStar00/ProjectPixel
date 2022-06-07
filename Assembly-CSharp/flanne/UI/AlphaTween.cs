using System;
using UnityEngine;

namespace flanne.UI
{
		[RequireComponent(typeof(CanvasGroup))]
	public class AlphaTween : UITweener
	{
				private void Awake()
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
		}

				public override void Show()
		{
			LeanTween.alphaCanvas(this.canvasGroup, 1f, this.duration).setIgnoreTimeScale(true);
		}

				public override void Hide()
		{
			LeanTween.alphaCanvas(this.canvasGroup, 0f, this.duration).setIgnoreTimeScale(true);
		}

				public override void SetOff()
		{
			this.canvasGroup.alpha = 0f;
		}

				private CanvasGroup canvasGroup;
	}
}
