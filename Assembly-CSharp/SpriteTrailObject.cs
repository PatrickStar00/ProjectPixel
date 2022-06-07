using System;
using UnityEngine;

public class SpriteTrailObject : MonoBehaviour
{
		private void Start()
	{
		this.mRenderer.enabled = false;
	}

		private void Update()
	{
		if (this.mbInUse)
		{
			base.transform.position = this.mPosition;
			this.mTimeDisplayed += Time.deltaTime;
			this.mRenderer.color = Color.Lerp(this.mStartColor, this.mEndColor, this.mTimeDisplayed / this.mDisplayTime);
			if (this.mTimeDisplayed >= this.mDisplayTime)
			{
				this.mSpawner.RemoveTrailObject(base.gameObject);
				this.mbInUse = false;
				this.mRenderer.enabled = false;
			}
		}
	}

		public void Initiate(float displayTime, Sprite sprite, Vector2 position, SpriteTrail trail)
	{
		this.mDisplayTime = displayTime;
		this.mRenderer.sprite = sprite;
		this.mRenderer.enabled = true;
		this.mPosition = position;
		this.mTimeDisplayed = 0f;
		this.mSpawner = trail;
		this.mbInUse = true;
	}

		public SpriteRenderer mRenderer;

		public Color mStartColor;

		public Color mEndColor;

		private bool mbInUse;

		private Vector2 mPosition;

		private float mDisplayTime;

		private float mTimeDisplayed;

		private SpriteTrail mSpawner;
}
