using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystem
{
		public abstract class AttackOnShoot : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.myGun = componentInParent.gun;
			this.myGun.OnShoot.AddListener(new UnityAction(this.IncrementCounter));
			this.Init();
		}

				private void OnDestroy()
		{
			this.myGun.OnShoot.RemoveListener(new UnityAction(this.IncrementCounter));
		}

				public void IncrementCounter()
		{
			this._counter++;
			if (this._counter >= this.shotsPerAttack)
			{
				this._counter = 0;
				this.Attack();
				SoundEffectSO soundEffectSO = this.soundFX;
				if (soundEffectSO == null)
				{
					return;
				}
				soundEffectSO.Play(null);
			}
		}

				public abstract void Attack();

				protected virtual void Init()
		{
		}

				[SerializeField]
		private SoundEffectSO soundFX;

				[SerializeField]
		private int shotsPerAttack;

				private int _counter;

				private Gun myGun;
	}
}
