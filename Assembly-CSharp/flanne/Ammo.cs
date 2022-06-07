using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class Ammo : MonoBehaviour
	{
						public bool outOfAmmo
		{
			get
			{
				return this.amount == 0;
			}
		}

						public bool fullOnAmmo
		{
			get
			{
				return this.amount == this.gun.maxAmmo;
			}
		}

								public int amount { get; private set; }

				private void Start()
		{
			this.infiniteAmmo = new BoolToggle(false);
			this.Reload();
			this.OnAmmoChanged.Invoke(this.amount);
			this.OnMaxAmmoChanged.Invoke(this.gun.maxAmmo);
			this.gun.stats[StatType.MaxAmmo].ChangedEvent += this.AmmoModChanged;
		}

				private void OnDestory()
		{
			this.gun.stats[StatType.MaxAmmo].ChangedEvent -= this.AmmoModChanged;
		}

				public void Reload()
		{
			this.amount = this.gun.maxAmmo;
			this.OnAmmoChanged.Invoke(this.amount);
			this.OnReload.Invoke();
		}

				public void UseAmmo(int a = 1)
		{
			if (this.infiniteAmmo.value)
			{
				return;
			}
			this.amount -= a;
			this.amount = Mathf.Clamp(this.amount, 0, this.gun.maxAmmo);
			this.OnAmmoChanged.Invoke(this.amount);
		}

				public void GainAmmo(int value = 1)
		{
			this.amount += value;
			this.amount = Mathf.Clamp(this.amount, 0, this.gun.maxAmmo);
			this.OnAmmoChanged.Invoke(this.amount);
		}

				public void AmmoModChanged(object sender, EventArgs e)
		{
			this.OnMaxAmmoChanged.Invoke(this.gun.maxAmmo);
		}

				[SerializeField]
		private Gun gun;

				public UnityEvent OnReload;

				public UnityIntEvent OnAmmoChanged;

				public UnityIntEvent OnMaxAmmoChanged;

				public BoolToggle infiniteAmmo;
	}
}
