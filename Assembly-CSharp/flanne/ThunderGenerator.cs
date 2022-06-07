using System;
using UnityEngine;

namespace flanne
{
		public class ThunderGenerator : MonoBehaviour
	{
				private void Awake()
		{
			ThunderGenerator.SharedInstance = this;
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.objectPoolTag, this.thunderPrefab, 100, true);
			this.sizeMultiplier = 1f;
			this.damageMod = new StatMod();
		}

				public void GenerateAt(GameObject target, int damage)
		{
			GameObject pooledObject = this.OP.GetPooledObject(this.objectPoolTag);
			pooledObject.transform.position = target.transform.position + new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0f);
			pooledObject.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(-45f, 45f));
			Harmful componentInChildren = pooledObject.GetComponentInChildren<Harmful>();
			componentInChildren.damageAmount = Mathf.FloorToInt(this.damageMod.Modify((float)damage));
			componentInChildren.transform.localScale = this.sizeMultiplier * Vector3.one;
			pooledObject.SetActive(true);
			pooledObject.GetComponent<SpriteRenderer>().flipX = (Random.Range(0, 1) == 0);
			this.soundFX.Play(null);
			this.PostNotification(ThunderGenerator.ThunderHitEvent, target);
		}

				public static ThunderGenerator SharedInstance;

				public static string ThunderHitEvent = "ThunderGenerator.ThunderHitEvent";

				[SerializeField]
		private GameObject thunderPrefab;

				[SerializeField]
		private SoundEffectSO soundFX;

				[SerializeField]
		private string objectPoolTag;

				public StatMod damageMod;

				public float sizeMultiplier;

				private ObjectPooler OP;
	}
}
