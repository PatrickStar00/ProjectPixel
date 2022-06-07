using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class PlayerStats : MonoBehaviour
	{
				private void Awake()
		{
			this.fireRate = new Multiplier();
			this.reloadRate = new Multiplier();
			this.damageMulti = new Multiplier();
			this.summonDamageMulti = new Multiplier();
			this.summonAtkSpdMulti = new Multiplier();
			this.projectileSpeedMulti = new Multiplier();
			this.projectileSizeMulti = new Multiplier();
			this.knockbackMulti = new Multiplier();
			this.movespeedMulti = new Multiplier();
			this.walkspeedMulti = new Multiplier();
		}

								public int ammoMod
		{
			get
			{
				return this._ammoMod;
			}
			set
			{
				this._ammoMod = value;
				this.maxAmmoChanged.Invoke();
			}
		}

				public Multiplier fireRate;

				public Multiplier reloadRate;

				public Multiplier damageMulti;

				public Multiplier summonDamageMulti;

				public Multiplier summonAtkSpdMulti;

				public Multiplier projectileSpeedMulti;

				public Multiplier projectileSizeMulti;

				public Multiplier knockbackMulti;

				public Multiplier movespeedMulti;

				public Multiplier walkspeedMulti;

				public int numProjectilesMod;

				public float spreadMod;

				public int bounceMod;

				public int pierceMod;

				[SerializeField]
		private int _ammoMod;

				public UnityEvent maxAmmoChanged;
	}
}
