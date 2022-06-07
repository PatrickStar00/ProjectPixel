using System;
using UnityEngine;

namespace flanne
{
		public class DropHPOverTime : MonoBehaviour
	{
				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.hpPrefab.name, this.hpPrefab, 100, true);
			base.InvokeRepeating("DropHP", 0f, this.timeToDrop);
		}

				private void OnDestroy()
		{
			base.CancelInvoke();
		}

				private void DropHP()
		{
			GameObject pooledObject = this.OP.GetPooledObject(this.hpPrefab.name);
			pooledObject.transform.position = base.transform.position;
			pooledObject.SetActive(true);
		}

				[SerializeField]
		private float timeToDrop;

				[SerializeField]
		private GameObject hpPrefab;

				private ObjectPooler OP;
	}
}
