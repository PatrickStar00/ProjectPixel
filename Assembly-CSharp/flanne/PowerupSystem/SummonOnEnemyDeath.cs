using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class SummonOnEnemyDeath : MonoBehaviour
	{
				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.summonPrefab.name, this.summonPrefab, 200, true);
			this.player = base.GetComponentInParent<PlayerController>();
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnDeath), Health.DeathEvent);
		}

				private void OnDeath(object sender, object args)
		{
			GameObject gameObject = (sender as Health).gameObject;
			if (gameObject.tag == "Enemy")
			{
				GameObject pooledObject = ObjectPooler.SharedInstance.GetPooledObject(this.summonPrefab.name);
				pooledObject.transform.SetParent(this.player.transform);
				pooledObject.transform.position = gameObject.transform.position;
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
		private GameObject summonPrefab;

				[SerializeField]
		private SoundEffectSO soundFX;

				private ObjectPooler OP;

				private PlayerController player;
	}
}
