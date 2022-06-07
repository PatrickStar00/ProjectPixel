using System;
using UnityEngine;

namespace flanne
{
		public class ThunderAttackSummon : AttackingSummon
	{
				protected override void Init()
		{
			this.TGen = ThunderGenerator.SharedInstance;
		}

				protected override bool Attack()
		{
			Vector2 center = base.transform.position;
			GameObject gameObject = null;
			for (int i = 0; i < this.thundersPerAttack; i++)
			{
				gameObject = EnemyFinder.GetRandomEnemy(center, this.range);
				if (gameObject != null)
				{
					this.TGen.GenerateAt(gameObject, Mathf.FloorToInt(base.summonDamageMod.Modify((float)this.baseDamage)));
				}
			}
			return gameObject != null;
		}

				[SerializeField]
		private int thundersPerAttack;

				[SerializeField]
		private Vector2 range;

				[SerializeField]
		private int baseDamage;

				private ThunderGenerator TGen;
	}
}
