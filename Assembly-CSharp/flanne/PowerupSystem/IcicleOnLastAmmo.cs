using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.PowerupSystem
{
		public class IcicleOnLastAmmo : MonoBehaviour
	{
				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.iciclePrefab.name, this.iciclePrefab, 10, true);
			this.player = base.GetComponentInParent<PlayerController>();
			this.ammo = this.player.ammo;
			this.ammo.OnAmmoChanged.AddListener(new UnityAction<int>(this.OnAmmoChanged));
		}

				private void OnDestroy()
		{
			this.ammo.OnAmmoChanged.RemoveListener(new UnityAction<int>(this.OnAmmoChanged));
		}

				private void OnAmmoChanged(int ammoAmount)
		{
			if (ammoAmount == 0)
			{
				this.SpawnIcicle();
			}
		}

				private void SpawnIcicle()
		{
			for (int i = 0; i < this.numIcicles; i++)
			{
				GameObject pooledObject = ObjectPooler.SharedInstance.GetPooledObject(this.iciclePrefab.name);
				pooledObject.transform.position = this.player.transform.position;
				SeekEnemy component = pooledObject.GetComponent<SeekEnemy>();
				if (component != null)
				{
					component.player = this.player.transform;
				}
				MoveComponent2D component2 = pooledObject.GetComponent<MoveComponent2D>();
				if (component2 != null)
				{
					component2.vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				}
				pooledObject.SetActive(true);
			}
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO == null)
			{
				return;
			}
			soundEffectSO.Play(null);
		}

				[SerializeField]
		private GameObject iciclePrefab;

				[SerializeField]
		private SoundEffectSO soundFX;

				[SerializeField]
		private int numIcicles;

				private ObjectPooler OP;

				private PlayerController player;

				private Ammo ammo;
	}
}
