using System;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class SpreadStatusRune : Rune
	{
				private void OnDeath(object sender, object args)
		{
			if (Random.Range(0f, 1f) > this.spreadChancePerLevel * (float)this.level)
			{
				return;
			}
			GameObject gameObject = (sender as Health).gameObject;
			if (this.BurnSys.IsBurning(gameObject))
			{
				GameObject pooledObject = this.OP.GetPooledObject(this.burnSpreadPrefab.name);
				pooledObject.transform.position = gameObject.transform.position;
				pooledObject.SetActive(true);
			}
			if (this.FreezeSys.IsFrozen(gameObject))
			{
				GameObject pooledObject2 = this.OP.GetPooledObject(this.freezeSpreadPrefab.name);
				pooledObject2.transform.position = gameObject.transform.position;
				pooledObject2.SetActive(true);
			}
		}

				private void Start()
		{
			this.BurnSys = BurnSystem.SharedInstance;
			this.FreezeSys = FreezeSystem.SharedInstance;
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.burnSpreadPrefab.name, this.burnSpreadPrefab, 100, true);
			this.OP.AddObject(this.freezeSpreadPrefab.name, this.freezeSpreadPrefab, 100, true);
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				[Range(0f, 1f)]
		[SerializeField]
		private float spreadChancePerLevel;

				[SerializeField]
		private GameObject burnSpreadPrefab;

				[SerializeField]
		private GameObject freezeSpreadPrefab;

				private BurnSystem BurnSys;

				private FreezeSystem FreezeSys;

				private ObjectPooler OP;
	}
}
