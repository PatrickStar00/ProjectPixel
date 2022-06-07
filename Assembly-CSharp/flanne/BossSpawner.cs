using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class BossSpawner : MonoBehaviour
	{
				public void LoadSpawners(List<BossSpawn> spawners)
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.arenaMonsterPrefab.name, this.arenaMonsterPrefab, 1, true);
			foreach (BossSpawn bossSpawn in spawners)
			{
				this.OP.AddObject(bossSpawn.bossPrefab.name, bossSpawn.bossPrefab, 1, true);
				base.StartCoroutine(this.WaitToSpawnCR(bossSpawn));
			}
		}

				private IEnumerator WaitToSpawnCR(BossSpawn spawner)
		{
			yield return new WaitForSeconds(spawner.timeToSpawn);
			GameObject pooledObject = this.OP.GetPooledObject(spawner.bossPrefab.name);
			pooledObject.transform.position = this.playerTransform.position + spawner.spawnPosition;
			pooledObject.SetActive(true);
			GameObject pooledObject2 = this.OP.GetPooledObject(this.arenaMonsterPrefab.name);
			pooledObject2.transform.position = this.playerTransform.position;
			pooledObject2.SetActive(true);
			yield break;
		}

				[SerializeField]
		private Transform playerTransform;

				[SerializeField]
		private GameObject arenaMonsterPrefab;

				private ObjectPooler OP;
	}
}
