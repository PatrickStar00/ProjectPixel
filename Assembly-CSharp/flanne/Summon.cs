using System;
using UnityEngine;

namespace flanne
{
		public class Summon : MonoBehaviour
	{
						public StatMod summonDamageMod
		{
			get
			{
				return this.stats[StatType.SummonDamage];
			}
		}

						public StatMod summonAtkSpdMod
		{
			get
			{
				return this.stats[StatType.SummonAttackSpeed];
			}
		}

				private void Start()
		{
			this.player = base.GetComponentInParent<PlayerController>();
			this.stats = this.player.stats;
			if (this.dontParent)
			{
				base.transform.SetParent(null);
			}
			this.Init();
		}

				protected virtual void Init()
		{
		}

				public string SummonTypeID;

				[SerializeField]
		private bool dontParent;

				protected PlayerController player;

				private StatsHolder stats;
	}
}
