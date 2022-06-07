using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.RuneSystem
{
		public class ActivateGameObjOnLastAmmoRune : Rune
	{
				protected override void Init()
		{
			this.ammo = this.player.ammo;
			this.ammo.OnAmmoChanged.AddListener(new UnityAction<int>(this.OnAmmoChanged));
		}

				private void OnDestroy()
		{
			this.ammo.OnAmmoChanged.RemoveListener(new UnityAction<int>(this.OnAmmoChanged));
		}

				private void OnAmmoChanged(int ammoAmount)
		{
			if (ammoAmount == 0)
			{
				base.StartCoroutine(this.DelayToActivateCR());
			}
		}

				private IEnumerator DelayToActivateCR()
		{
			yield return new WaitForSeconds(this.delayAfterLastAmmo);
			this.harm.damageAmount = Mathf.FloorToInt(this.percentBulletDamagePerLevel * this.player.gun.damage * (float)this.level);
			this.harm.gameObject.SetActive(true);
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO != null)
			{
				soundEffectSO.Play(null);
			}
			yield break;
		}

				[Range(0f, 1f)]
		[SerializeField]
		private float percentBulletDamagePerLevel;

				[SerializeField]
		private Harmful harm;

				[SerializeField]
		private float delayAfterLastAmmo;

				[SerializeField]
		private SoundEffectSO soundFX;

				private Ammo ammo;
	}
}
