using System;
using UnityEngine;

namespace flanne
{
		public abstract class AttackingSummon : Summon
	{
						private float finalAttackCooldown
		{
			get
			{
				return base.summonAtkSpdMod.ModifyInverse(this.attackCooldown);
			}
		}

				private void Update()
		{
			this._timer += Time.deltaTime;
			if (this._timer >= this.finalAttackCooldown)
			{
				this._timer -= this.finalAttackCooldown;
				if (this.Attack())
				{
					if (this.animator != null)
					{
						this.animator.SetTrigger("Attack");
					}
					if (this.attackSoundFX != null)
					{
						this.attackSoundFX.Play(null);
					}
				}
			}
		}

				protected abstract bool Attack();

				public float attackCooldown;

				[SerializeField]
		private Animator animator;

				[SerializeField]
		private SoundEffectSO attackSoundFX;

				private float _timer;
	}
}
