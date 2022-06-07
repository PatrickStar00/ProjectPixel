using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystem
{
		public class RefundAmmoWhenStanding : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.myGun = componentInParent.gun;
			this.myGun.OnShoot.AddListener(new UnityAction(this.ChangeToRefundAmmo));
			this.ammo = base.transform.parent.GetComponentInChildren<Ammo>();
			this._lastFramePos = base.transform.position;
			this._thisFramePos = base.transform.position;
		}

				private void OnDestroy()
		{
			this.myGun.OnShoot.RemoveListener(new UnityAction(this.ChangeToRefundAmmo));
		}

				private void Update()
		{
			this._lastFramePos = this._thisFramePos;
			this._thisFramePos = base.transform.position;
		}

				private void ChangeToRefundAmmo()
		{
			if (this._lastFramePos == this._thisFramePos && Random.Range(0f, 1f) < this.chanceToRefund)
			{
				this.ammo.GainAmmo(1);
			}
		}

				[Range(0f, 1f)]
		[SerializeField]
		private float chanceToRefund;

				private Vector3 _lastFramePos;

				private Vector3 _thisFramePos;

				private Gun myGun;

				private Ammo ammo;
	}
}
