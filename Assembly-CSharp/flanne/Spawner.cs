using System;
using UnityEngine;

namespace flanne
{
		public class Spawner : MonoBehaviour
	{
				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.spawnedObject.name, this.spawnedObject, 100, true);
		}

				public void Spawn()
		{
			foreach (Transform transform in this.spawnPoints)
			{
				GameObject pooledObject = this.OP.GetPooledObject(this.spawnedObject.name);
				pooledObject.transform.position = transform.position;
				pooledObject.SetActive(true);
				pooledObject.GetComponent<MoveComponent2D>().vector = (pooledObject.transform.position - base.transform.position).normalized * this.initialSpeed;
			}
		}

				[SerializeField]
		private float initialSpeed;

				[SerializeField]
		private GameObject spawnedObject;

				[SerializeField]
		private Transform[] spawnPoints;

				private ObjectPooler OP;
	}
}
