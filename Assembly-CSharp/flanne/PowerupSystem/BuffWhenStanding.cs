using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class BuffWhenStanding : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.stats = componentInParent.stats;
			this._lastFramePos = base.transform.position;
		}

				private void Update()
		{
			this._timer += Time.deltaTime;
			if (this._timer >= this.secsPerTick)
			{
				this._timer -= this.secsPerTick;
				this.IncrementBuff();
			}
			if (this._lastFramePos != base.transform.position)
			{
				this.ResetBuff();
			}
			this._lastFramePos = base.transform.position;
		}

				private void ResetBuff()
		{
			this.stats[StatType.BulletDamage].AddMultiplierBonus((float)(-1 * this._ticks) * this.damageBoostPerTick);
			this._ticks = 0;
			this._timer = 0f;
		}

				private void IncrementBuff()
		{
			if (this._ticks < this.maxTicks)
			{
				this.stats[StatType.BulletDamage].AddMultiplierBonus(this.damageBoostPerTick);
				this._ticks++;
			}
		}

				[SerializeField]
		private float damageBoostPerTick;

				[SerializeField]
		private float secsPerTick;

				[SerializeField]
		private int maxTicks;

				private Vector3 _lastFramePos;

				private StatsHolder stats;

				private int _ticks;

				private float _timer;
	}
}
