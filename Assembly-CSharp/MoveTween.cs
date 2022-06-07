using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MoveTween : UITweener
{
		private void Start()
	{
		switch (this.entranceFrom)
		{
		case MoveTween.Direction.Up:
			this.moveVector = new Vector3(0f, this.moveAmount, 0f);
			break;
		case MoveTween.Direction.Down:
			this.moveVector = new Vector3(0f, -1f * this.moveAmount, 0f);
			break;
		case MoveTween.Direction.Left:
			this.moveVector = new Vector3(-1f * this.moveAmount, 0f, 0f);
			break;
		case MoveTween.Direction.Right:
			this.moveVector = new Vector3(this.moveAmount, 0f, 0f);
			break;
		}
		this.rectTransform = base.GetComponent<RectTransform>();
		this.startPosition = this.rectTransform.anchoredPosition;
	}

		public override void Show()
	{
		LeanTween.move(this.rectTransform, this.startPosition, this.duration).setEase(this.easeType).setIgnoreTimeScale(true);
	}

		public override void Hide()
	{
		LeanTween.move(this.rectTransform, this.startPosition + this.moveVector, this.duration).setIgnoreTimeScale(true);
	}

		public override void SetOff()
	{
		LeanTween.move(this.rectTransform, this.startPosition + this.moveVector, 0f).setIgnoreTimeScale(true);
	}

		[SerializeField]
	private LeanTweenType easeType = LeanTweenType.linear;

		public MoveTween.Direction entranceFrom;

		public float moveAmount;

		private Vector3 moveVector;

		private Vector3 startPosition;

		private RectTransform rectTransform;

		public enum Direction
	{
				Up,
				Down,
				Left,
				Right
	}
}
