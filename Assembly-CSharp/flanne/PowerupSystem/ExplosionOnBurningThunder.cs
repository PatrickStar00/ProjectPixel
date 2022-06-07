using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class ExplosionOnBurningThunder : MonoBehaviour
	{
				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.explosionPrefab.name, this.explosionPrefab, 5, true);
			this.BurnSys = BurnSystem.SharedInstance;
			this.AddObserver(new Action<object, object>(this.OnThunderHit), ThunderGenerator.ThunderHitEvent);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnThunderHit), ThunderGenerator.ThunderHitEvent);
		}

				private void OnThunderHit(object sender, object args)
		{
			GameObject gameObject = args as GameObject;
			if (this.BurnSys.IsBurning(gameObject))
			{
				GameObject pooledObject = ObjectPooler.SharedInstance.GetPooledObject(this.explosionPrefab.name);
				pooledObject.transform.position = gameObject.transform.position;
				pooledObject.SetActive(true);
				this.cameraShaker.Shake();
				SoundEffectSO soundEffectSO = this.soundFX;
				if (soundEffectSO == null)
				{
					return;
				}
				soundEffectSO.Play(null);
			}
		}

				[SerializeField]
		private GameObject explosionPrefab;

				[SerializeField]
		private ExplosionShake2D cameraShaker;

				[SerializeField]
		private SoundEffectSO soundFX;

				private ObjectPooler OP;

				private BurnSystem BurnSys;
	}
}
