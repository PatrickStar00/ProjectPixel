using System;
using UnityEngine;

namespace flanne
{
		public class VerticleBounce : MonoBehaviour
	{
				private void OnEnable()
		{
			float to = base.transform.localPosition.y + this.bounceHeight;
			this._tweenID = LeanTween.moveLocalY(base.gameObject, to, 1f / (this.bouncePerSecond / 2f)).setLoopPingPong().id;
		}

				private void OnDisable()
		{
			if (LeanTween.isTweening(this._tweenID))
			{
				LeanTween.cancel(this._tweenID);
			}
		}

				[SerializeField]
		private float bounceHeight;

				[SerializeField]
		private float bouncePerSecond;

				private int _tweenID;
	}
}
