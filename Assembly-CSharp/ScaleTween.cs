using System;
using UnityEngine;

public class ScaleTween : UITweener
{
		private void Awake()
	{
		base.transform.localScale = new Vector3(this.startScaleX, this.startScaleY, 1f);
	}

		public override void Show()
	{
		LeanTween.scaleX(base.gameObject, 1f, this.duration).setEase(this.easeType).setIgnoreTimeScale(true);
		LeanTween.scaleY(base.gameObject, 1f, this.duration).setEase(this.easeType).setIgnoreTimeScale(true);
	}

		public override void Hide()
	{
		LeanTween.scaleX(base.gameObject, this.startScaleX, this.duration).setIgnoreTimeScale(true);
		LeanTween.scaleY(base.gameObject, this.startScaleY, this.duration).setIgnoreTimeScale(true);
	}

		public override void SetOff()
	{
		this.Hide();
	}

		[SerializeField]
	private LeanTweenType easeType = LeanTweenType.linear;

		[SerializeField]
	private float startScaleX = 1f;

		[SerializeField]
	private float startScaleY = 1f;
}
