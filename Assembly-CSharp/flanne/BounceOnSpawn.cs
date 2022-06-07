using System;
using UnityEngine;

namespace flanne
{
		public class BounceOnSpawn : MonoBehaviour
	{
				private void OnEnable()
		{
			this._startPos = base.transform.localPosition;
			Vector3 to = this._startPos + new Vector3(0f, this.bounceHeight, 0f);
			this._tweenID = LeanTween.moveLocal(base.gameObject, to, this.duration / 4f).setEase(LeanTweenType.easeOutSine).setOnComplete(new Action(this.Fall)).id;
		}

				private void OnDisable()
		{
			if (LeanTween.isTweening(this._tweenID))
			{
				LeanTween.cancel(this._tweenID);
			}
		}

				private void Fall()
		{
			this._tweenID = LeanTween.moveLocal(base.gameObject, this._startPos, this.duration * 0.75f).setEase(LeanTweenType.easeOutBounce).id;
		}

				[SerializeField]
		private float bounceHeight;

				[SerializeField]
		private float duration;

				private Vector3 _startPos;

				private int _tweenID;
	}
}
