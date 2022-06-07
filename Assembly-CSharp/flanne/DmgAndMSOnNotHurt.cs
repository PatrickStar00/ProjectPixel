using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
		public class DmgAndMSOnNotHurt : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.transform.GetComponentInParent<PlayerController>();
			this.spriteTrail = componentInParent.playerSprite.GetComponentInChildren<SpriteTrail>();
			this.stats = componentInParent.stats;
			this.health = componentInParent.playerHealth;
			this.health.onHurt.AddListener(new UnityAction<int>(this.OnHurt));
		}

				private void OnDestroy()
		{
			this.health.onHurt.RemoveListener(new UnityAction<int>(this.OnHurt));
		}

				private void Update()
		{
			if (this._ticks < this.maxTicks)
			{
				this._timer += Time.deltaTime;
			}
			if (this._timer >= this.secsPerTick)
			{
				this._timer -= this.secsPerTick;
				this._ticks++;
				this.stats[StatType.BulletDamage].AddMultiplierBonus(this.damageBoostPerTick);
				this.stats[StatType.MoveSpeed].AddMultiplierBonus(this.movespeedBoostPerTick);
				if (this._ticks >= this.maxTicks / 2)
				{
					this.spriteTrail.SetEnabled(true);
				}
			}
		}

				private void OnHurt(int i)
		{
			this.stats[StatType.BulletDamage].AddMultiplierBonus((float)(-1 * this._ticks) * this.damageBoostPerTick);
			this.stats[StatType.MoveSpeed].AddMultiplierBonus((float)(-1 * this._ticks) * this.movespeedBoostPerTick);
			this.spriteTrail.SetEnabled(false);
			this._ticks = 0;
			this._timer = 0f;
		}

				[SerializeField]
		private float damageBoostPerTick;

				[SerializeField]
		private float movespeedBoostPerTick;

				[SerializeField]
		private float secsPerTick;

				[SerializeField]
		private int maxTicks;

				private SpriteTrail spriteTrail;

				private StatsHolder stats;

				private Health health;

				private int _ticks;

				private float _timer;
	}
}
