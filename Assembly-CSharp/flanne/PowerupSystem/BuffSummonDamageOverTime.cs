using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class BuffSummonDamageOverTime : MonoBehaviour
	{
				private void Start()
		{
			foreach (ShootingSummon shootingSummon in base.GetComponentInParent<PlayerController>().GetComponentsInChildren<ShootingSummon>(true))
			{
				if (shootingSummon.SummonTypeID == this.SummonTypeID)
				{
					this._summon = shootingSummon;
				}
			}
		}

				private void Update()
		{
			if (this._timer > this.secondsPerBuff)
			{
				this._summon.baseDamage += this.damageBuff;
				this._timer -= this.secondsPerBuff;
			}
			this._timer += Time.deltaTime;
		}

				[SerializeField]
		private string SummonTypeID;

				[SerializeField]
		private int damageBuff;

				[SerializeField]
		private float secondsPerBuff;

				private ShootingSummon _summon;

				private float _timer;
	}
}
