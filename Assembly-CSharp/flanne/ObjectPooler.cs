using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class ObjectPooler : MonoBehaviour
	{
				private void Awake()
		{
			ObjectPooler.SharedInstance = this;
			this.itemDictionary = new Dictionary<string, ObjectPoolItem>();
			this.pooledObjectsDictionary = new Dictionary<string, List<GameObject>>();
			this.pooledObjects = new List<GameObject>();
			this.positions = new Dictionary<string, int>();
			for (int i = 0; i < this.itemsToPool.Count; i++)
			{
				this.ObjectPoolItemToPooledObject(i);
				this.itemDictionary.Add(this.itemsToPool[i].tag, this.itemsToPool[i]);
			}
		}

				public GameObject GetPooledObject(string tag)
		{
			int count = this.pooledObjectsDictionary[tag].Count;
			for (int i = this.positions[tag] + 1; i < this.positions[tag] + this.pooledObjectsDictionary[tag].Count; i++)
			{
				if (!this.pooledObjectsDictionary[tag][i % count].activeInHierarchy)
				{
					this.positions[tag] = i % count;
					return this.pooledObjectsDictionary[tag][i % count];
				}
			}
			if (this.itemDictionary[tag].shouldExpand)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemDictionary[tag].objectToPool);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(base.transform);
				this.pooledObjectsDictionary[tag].Add(gameObject);
				return gameObject;
			}
			return null;
		}

				public List<GameObject> GetAllPooledObjects(string tag)
		{
			return this.pooledObjectsDictionary[tag];
		}

				public void AddObject(string tag, GameObject GO, int amt = 3, bool exp = true)
		{
			ObjectPoolItem item = new ObjectPoolItem(tag, GO, amt, exp);
			int count = this.itemsToPool.Count;
			this.itemsToPool.Add(item);
			if (!this.pooledObjectsDictionary.ContainsKey(tag))
			{
				this.ObjectPoolItemToPooledObject(count);
				this.itemDictionary.Add(this.itemsToPool[count].tag, this.itemsToPool[count]);
			}
		}

				private void ObjectPoolItemToPooledObject(int index)
		{
			ObjectPoolItem objectPoolItem = this.itemsToPool[index];
			this.pooledObjects = new List<GameObject>();
			for (int i = 0; i < objectPoolItem.amountToPool; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(objectPoolItem.objectToPool);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(base.transform);
				this.pooledObjects.Add(gameObject);
			}
			this.pooledObjectsDictionary.Add(objectPoolItem.tag, this.pooledObjects);
			this.positions.Add(objectPoolItem.tag, 0);
		}

				public static ObjectPooler SharedInstance;

				public List<ObjectPoolItem> itemsToPool;

				public Dictionary<string, ObjectPoolItem> itemDictionary;

				public Dictionary<string, List<GameObject>> pooledObjectsDictionary;

				public List<GameObject> pooledObjects;

				private Dictionary<string, int> positions;
	}
}
