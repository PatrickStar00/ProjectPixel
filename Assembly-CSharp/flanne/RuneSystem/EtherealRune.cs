using System;
using System.Collections;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class EtherealRune : Rune
	{
				private void OnDeath(object sender, object args)
		{
			if (this._isActive)
			{
				return;
			}
			if ((sender as Health).gameObject.tag == "Enemy")
			{
				this._counter++;
				if (this._counter >= this.killsToActivate)
				{
					base.StartCoroutine(this.EtherealStateCR());
				}
			}
		}

				protected override void Init()
		{
			this._isActive = false;
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private IEnumerator EtherealStateCR()
		{
			this._isActive = true;
			this.player.ammo.infiniteAmmo.Flip();
			yield return new WaitForSeconds(this.durationPerLevel * (float)this.level);
			this._isActive = false;
			this.player.ammo.infiniteAmmo.UnFlip();
			yield break;
		}

				[SerializeField]
		private float durationPerLevel;

				[SerializeField]
		private int killsToActivate;

				private int _counter;

				private bool _isActive;
	}
}
