using System;
using UnityEngine;

namespace flanne
{
		public class ScaleBounce : MonoBehaviour
	{
				private void OnEnable()
		{
			this._tweenID = LeanTween.scale(base.gameObject, this.scaleTo, 1f / (this.bouncePerSecond / 2f)).setLoopPingPong().setIgnoreTimeScale(this.ignoreTimeScale).id;
		}

				private void OnDisable()
		{
			if (LeanTween.isTweening(this._tweenID))
			{
				LeanTween.cancel(this._tweenID);
			}
		}

				[SerializeField]
		private Vector3 scaleTo;

				[SerializeField]
		private float bouncePerSecond;

				[SerializeField]
		private bool ignoreTimeScale;

				private int _tweenID;
	}
}
