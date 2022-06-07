using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class ThunderDuringHolyShield : MonoBehaviour
	{
				private void Start()
		{
			this.TGen = ThunderGenerator.SharedInstance;
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			this.playerTransform = componentInParent.transform;
			this.holyShield = componentInParent.GetComponentInChildren<PreventDamage>();
		}

				private void Update()
		{
			if (this.holyShield.isActive)
			{
				this._timer += Time.deltaTime;
			}
			if (this._timer > this.cooldown)
			{
				this._timer -= this.cooldown;
				for (int i = 0; i < this.thundersPerWave; i++)
				{
					GameObject randomEnemy = EnemyFinder.GetRandomEnemy(this.playerTransform.position, this.range);
					if (randomEnemy != null)
					{
						this.TGen.GenerateAt(randomEnemy, this.baseDamage);
					}
				}
			}
		}

				[SerializeField]
		private int baseDamage;

				[SerializeField]
		private float cooldown;

				[SerializeField]
		private int thundersPerWave;

				[SerializeField]
		private Vector2 range;

				private ThunderGenerator TGen;

				private Transform playerTransform;

				private PreventDamage holyShield;

				private float _timer;
	}
}
