using System;
using UnityEngine;

namespace flanne
{
		public class SpawnOnCollision : MonoBehaviour
	{
				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
		}

				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				foreach (ContactPoint2D contactPoint2D in other.contacts)
				{
					GameObject pooledObject = this.OP.GetPooledObject(this.objPoolTag);
					pooledObject.SetActive(true);
					pooledObject.transform.position = contactPoint2D.point;
				}
			}
		}

				[SerializeField]
		private string hitTag;

				[SerializeField]
		private string objPoolTag;

				private ObjectPooler OP;
	}
}
