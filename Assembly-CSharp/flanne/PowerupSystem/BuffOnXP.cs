using System;
using System.Collections;
using flanne.Pickups;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class BuffOnXP : MonoBehaviour
	{
				private void OnXPPickup(object sender, object args)
		{
			if (this._timer <= 0f)
			{
				base.StartCoroutine(this.StartBuffCR());
				return;
			}
			this._timer = this.duration;
		}

				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.stats = componentInParent.stats;
			this.AddObserver(new Action<object, object>(this.OnXPPickup), XPPickup.XPPickupEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnXPPickup), XPPickup.XPPickupEvent);
		}

				private IEnumerator StartBuffCR()
		{
			this._timer = this.duration;
			this.stats[StatType.MoveSpeed].AddMultiplierBonus(this.moveSpeedBoost);
			while (this._timer > 0f)
			{
				yield return null;
				this._timer -= Time.deltaTime;
			}
			this.stats[StatType.MoveSpeed].AddMultiplierBonus(-1f * this.moveSpeedBoost);
			this._timer = 0f;
			yield break;
		}

				[SerializeField]
		private float moveSpeedBoost;

				[SerializeField]
		private float duration;

				private StatsHolder stats;

				private float _timer;
	}
}
