using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrail : MonoBehaviour
{
		private void Start()
	{
		this.mSpawnInterval = this.mTrailTime / (float)this.mTrailSegments;
		this.mTrailObjectsInUse = new List<GameObject>();
		this.mTrailObjectsNotInUse = new Queue<GameObject>();
		for (int i = 0; i < this.mTrailSegments + 1; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.mTrailObject);
			gameObject.transform.SetParent(base.transform);
			this.mTrailObjectsNotInUse.Enqueue(gameObject);
		}
		this.mbEnabled = false;
	}

		private void Update()
	{
		if (this.mbEnabled)
		{
			this.mSpawnTimer += Time.deltaTime;
			if (this.mSpawnTimer >= this.mSpawnInterval)
			{
				GameObject gameObject = this.mTrailObjectsNotInUse.Dequeue();
				if (gameObject != null)
				{
					gameObject.GetComponent<SpriteTrailObject>().Initiate(this.mTrailTime, this.mLeadingSprite.sprite, base.transform.position, this);
					this.mTrailObjectsInUse.Add(gameObject);
					this.mSpawnTimer = 0f;
				}
			}
		}
	}

		public void RemoveTrailObject(GameObject obj)
	{
		this.mTrailObjectsInUse.Remove(obj);
		this.mTrailObjectsNotInUse.Enqueue(obj);
	}

		public void SetEnabled(bool enabled)
	{
		this.mbEnabled = enabled;
		if (enabled)
		{
			this.mSpawnTimer = this.mSpawnInterval;
		}
	}

		public SpriteRenderer mLeadingSprite;

		public int mTrailSegments;

		public float mTrailTime;

		public GameObject mTrailObject;

		private float mSpawnInterval;

		private float mSpawnTimer;

		private bool mbEnabled;

		private List<GameObject> mTrailObjectsInUse;

		private Queue<GameObject> mTrailObjectsNotInUse;
}
