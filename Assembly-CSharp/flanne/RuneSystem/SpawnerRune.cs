using System;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class SpawnerRune : Rune
	{
						private float cooldown
		{
			get
			{
				return this.player.stats[StatType.SummonAttackSpeed].ModifyInverse(this.baseCooldown - this.cooldownReductionPerLevel * (float)this.level);
			}
		}

				protected override void Init()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.spawnPrefab.name, this.spawnPrefab, 10, true);
		}

				private void Update()
		{
			this._timer += Time.deltaTime;
			if (this._timer > this.cooldown)
			{
				this._timer -= this.cooldown;
				GameObject pooledObject = this.OP.GetPooledObject(this.spawnPrefab.name);
				pooledObject.transform.position = base.transform.position;
				Spawn component = pooledObject.GetComponent<Spawn>();
				if (component != null)
				{
					component.player = this.player;
				}
				pooledObject.SetActive(true);
				SoundEffectSO soundEffectSO = this.soundFX;
				if (soundEffectSO == null)
				{
					return;
				}
				soundEffectSO.Play(null);
			}
		}

				[SerializeField]
		private GameObject spawnPrefab;

				[SerializeField]
		private float cooldownReductionPerLevel;

				[SerializeField]
		private float baseCooldown;

				[SerializeField]
		private SoundEffectSO soundFX;

				private float _timer;

				private ObjectPooler OP;
	}
}
