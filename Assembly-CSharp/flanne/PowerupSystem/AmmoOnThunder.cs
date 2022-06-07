using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class AmmoOnThunder : MonoBehaviour
	{
				private void Start()
		{
			this.ammo = base.transform.parent.GetComponentInChildren<Ammo>();
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.staticObjectPoolTag, this.staticPrefab, 5, true);
			base.transform.localPosition = Vector3.zero;
			this.AddObserver(new Action<object, object>(this.OnThunderHit), ThunderGenerator.ThunderHitEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnThunderHit), ThunderGenerator.ThunderHitEvent);
		}

				private void OnThunderHit(object sender, object args)
		{
			if (Random.Range(0f, 1f) < this.chanceToActivate)
			{
				if (this.ammo != null)
				{
					this.ammo.GainAmmo(this.ammoRefillAmount);
					GameObject pooledObject = this.OP.GetPooledObject(this.staticObjectPoolTag);
					pooledObject.transform.SetParent(base.transform);
					pooledObject.transform.localPosition = Vector3.zero;
					pooledObject.SetActive(true);
					this.sfx.Play(null);
					return;
				}
				Debug.LogWarning("No ammo component found");
			}
		}

				[SerializeField]
		private GameObject staticPrefab;

				[SerializeField]
		private string staticObjectPoolTag;

				[SerializeField]
		private SoundEffectSO sfx;

				[Range(0f, 1f)]
		[SerializeField]
		private float chanceToActivate;

				[SerializeField]
		private int ammoRefillAmount;

				private ObjectPooler OP;

				private Ammo ammo;
	}
}
