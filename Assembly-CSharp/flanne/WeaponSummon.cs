using System;
using UnityEngine;

namespace flanne
{
		public class WeaponSummon : Summon
	{
				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag == "Enemy")
			{
				Health component = other.gameObject.GetComponent<Health>();
				if (component != null)
				{
					component.HPChange(Mathf.FloorToInt(-1f * base.summonDamageMod.Modify(this.player.gun.damage) * this.baseDamageMultiplier));
				}
			}
		}

				[SerializeField]
		private float baseDamageMultiplier = 1f;
	}
}
