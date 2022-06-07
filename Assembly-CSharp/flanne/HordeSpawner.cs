using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class HordeSpawner : MonoBehaviour
	{
				private void Awake()
		{
			this.activeSpawners = new List<SpawnSession>();
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
		}

				private void Update()
		{
			for (int i = 0; i < this.activeSpawners.Count; i++)
			{
				this.activeSpawners[i].timer -= Time.deltaTime;
				if (this.activeSpawners[i].timer < 0f)
				{
					if (this.CountActiveObjects(this.activeSpawners[i].objectPoolTag) < this.activeSpawners[i].maximum)
					{
						for (int j = 0; j < this.activeSpawners[i].numPerSpawn; j++)
						{
							this.Spawn(this.activeSpawners[i].objectPoolTag, this.activeSpawners[i].HP);
						}
					}
					this.activeSpawners[i].timer += this.activeSpawners[i].spawnCooldown;
				}
			}
		}

				public void LoadSpawners(List<SpawnSession> spawnSessions)
		{
			foreach (SpawnSession spawner in spawnSessions)
			{
				base.StartCoroutine(this.SpawnerLifeCycleCR(spawner));
			}
		}

				private void Spawn(string objectPoolTag, int HP)
		{
			GameObject pooledObject = this.OP.GetPooledObject(objectPoolTag);
			Vector2 vector = Random.insideUnitCircle.normalized * this.spawnRadius;
			pooledObject.transform.position = this.playerTransform.position + new Vector3(vector.x, vector.y, 0f);
			Health component = pooledObject.GetComponent<Health>();
			if (component != null)
			{
				component.maxHP = HP;
			}
			pooledObject.SetActive(true);
		}

				private int CountActiveObjects(string objectPoolTag)
		{
			int num = 0;
			List<GameObject> allPooledObjects = this.OP.GetAllPooledObjects(objectPoolTag);
			for (int i = 0; i < allPooledObjects.Count; i++)
			{
				if (allPooledObjects[i].activeInHierarchy)
				{
					num++;
				}
			}
			return num;
		}

				private IEnumerator SpawnerLifeCycleCR(SpawnSession spawner)
		{
			yield return new WaitForSeconds(spawner.startTime);
			this.activeSpawners.Add(spawner);
			yield return new WaitForSeconds(spawner.duration);
			this.activeSpawners.Remove(spawner);
			yield break;
		}

				[SerializeField]
		private Transform playerTransform;

				[SerializeField]
		private float spawnRadius;

				private List<SpawnSession> activeSpawners;

				private ObjectPooler OP;
	}
}
