using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class IceShatterOnDeath : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.shatterPrefab.name, this.shatterPrefab, 25, true);
			this.FreezeSys = FreezeSystem.SharedInstance;
			this.player = base.GetComponentInParent<PlayerController>();
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDeath(object sender, object args)
		{
			Health health = sender as Health;
			GameObject gameObject = health.gameObject;
			if (gameObject.tag == "Enemy" && this.FreezeSys.IsFrozen(gameObject))
			{
				GameObject pooledObject = ObjectPooler.SharedInstance.GetPooledObject(this.shatterPrefab.name);
				pooledObject.transform.position = gameObject.transform.position;
				pooledObject.GetComponent<Harmful>().damageAmount = Mathf.FloorToInt((float)health.maxHP * this.shatterPercentDamage);
				pooledObject.SetActive(true);
				SoundEffectSO soundEffectSO = this.soundFX;
				if (soundEffectSO == null)
				{
					return;
				}
				soundEffectSO.Play(null);
			}
		}

				[SerializeField]
		private GameObject shatterPrefab;

				[Range(0f, 1f)]
		[SerializeField]
		private float shatterPercentDamage;

				[SerializeField]
		private SoundEffectSO soundFX;

				private ObjectPooler OP;

				private FreezeSystem FreezeSys;

				private PlayerController player;
	}
}
