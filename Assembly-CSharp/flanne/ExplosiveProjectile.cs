using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace flanne
{
		public class ExplosiveProjectile : Projectile
	{
						public override float damage
		{
			set
			{
				this._damage = Mathf.FloorToInt(value);
			}
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this._initialized = true;
			this._isQuitting = false;
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.ChangedActiveScene);
		}

				private void OnDestroy()
		{
			SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.ChangedActiveScene);
		}

				private void OnDisable()
		{
			if (this._initialized && !this._isQuitting)
			{
				this.SpawnExplosive();
			}
		}

				private void OnApplicationQuit()
		{
			this._isQuitting = true;
		}

				private void ChangedActiveScene(Scene current, Scene next)
		{
			this._isQuitting = true;
		}

				protected override void SetSize(float size)
		{
			this._explosionSize = new Vector3(size, size, 1f);
			base.SetSize(size);
		}

				private void SpawnExplosive()
		{
			GameObject pooledObject = this.OP.GetPooledObject(this.explosionOPTag);
			pooledObject.GetComponent<Harmful>().damageAmount = this._damage;
			pooledObject.transform.position = base.transform.position;
			pooledObject.transform.localScale = this._explosionSize;
			pooledObject.SetActive(true);
			ExplosionShake2D explosionShake2D = this.cameraShaker;
			if (explosionShake2D != null)
			{
				explosionShake2D.Shake();
			}
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO == null)
			{
				return;
			}
			soundEffectSO.Play(null);
		}

				private int _damage;

				[SerializeField]
		private string explosionOPTag;

				[SerializeField]
		private ExplosionShake2D cameraShaker;

				[SerializeField]
		private SoundEffectSO soundFX;

				private bool _initialized;

				private bool _isQuitting;

				private Vector3 _explosionSize;
	}
}
