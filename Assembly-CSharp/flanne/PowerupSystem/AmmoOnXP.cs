using System;
using flanne.Pickups;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class AmmoOnXP : MonoBehaviour
	{
				private void Start()
		{
			this.ammo = base.transform.parent.GetComponentInChildren<Ammo>();
			base.transform.localPosition = Vector3.zero;
			this.AddObserver(new Action<object, object>(this.OnXPPickup), XPPickup.XPPickupEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnXPPickup), XPPickup.XPPickupEvent);
		}

				private void OnXPPickup(object sender, object args)
		{
			if (Random.Range(0f, 1f) < this.chanceToActivate)
			{
				if (this.ammo != null)
				{
					this.ammo.GainAmmo(this.ammoRefillAmount);
					this.sfx.Play(null);
					return;
				}
				Debug.LogWarning("No ammo component found");
			}
		}

				[SerializeField]
		private SoundEffectSO sfx;

				[Range(0f, 1f)]
		[SerializeField]
		private float chanceToActivate;

				[SerializeField]
		private int ammoRefillAmount;

				private Ammo ammo;
	}
}
