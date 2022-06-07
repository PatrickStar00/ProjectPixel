using System;
using UnityEngine;

namespace flanne
{
		[CreateAssetMenu(fileName = "GunData", menuName = "GunData", order = 1)]
	public class GunData : ScriptableObject
	{
						public string nameString
		{
			get
			{
				return LocalizationSystem.GetLocalizedValue(this.nameStringID.key);
			}
		}

						public string description
		{
			get
			{
				return LocalizationSystem.GetLocalizedValue(this.descriptionStringID.key);
			}
		}

						public string bulletOPTag
		{
			get
			{
				return this.bullet.name;
			}
		}

				public LocalizedString nameStringID;

				public LocalizedString descriptionStringID;

				public GameObject model;

				public SoundEffectSO gunshotSFX;

				public float damage;

				public float shotCooldown;

				public int maxAmmo;

				public float reloadDuration;

				public int numOfProjectiles;

				public float spread;

				public float knockback;

				public float projectileSpeed;

				public int bounce;

				public int piercing;

				public float burnChance;

				public GameObject bullet;

				public bool isSummonGun;
	}
}
