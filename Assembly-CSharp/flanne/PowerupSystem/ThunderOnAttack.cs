using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class ThunderOnAttack : AttackOnShoot
	{
				protected override void Init()
		{
			this.TGen = ThunderGenerator.SharedInstance;
		}

				public override void Attack()
		{
			for (int i = 0; i < this.numThunderPerAttack; i++)
			{
				GameObject randomEnemy = EnemyFinder.GetRandomEnemy(base.transform.position, this.range);
				if (randomEnemy != null)
				{
					this.TGen.GenerateAt(randomEnemy, this.baseDamage);
				}
			}
		}

				[SerializeField]
		private int baseDamage;

				[SerializeField]
		private int numThunderPerAttack = 1;

				[SerializeField]
		private Vector2 range;

				private ThunderGenerator TGen;
	}
}
