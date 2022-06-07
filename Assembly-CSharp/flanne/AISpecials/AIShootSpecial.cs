using System;
using UnityEngine;

namespace flanne.AISpecials
{
		[CreateAssetMenu(fileName = "AIShootSpecial", menuName = "AISpecials/AIShootSpecial")]
	public class AIShootSpecial : AISpecial
	{
				public override void Use(AIComponent ai, Transform target)
		{
			GameObject pooledObject = ObjectPooler.SharedInstance.GetPooledObject(this.projectileOPTag);
			pooledObject.SetActive(true);
			pooledObject.transform.position = ai.specialPoint.position;
			Vector3 vector = target.position - ai.specialPoint.position;
			Projectile component = pooledObject.GetComponent<Projectile>();
			component.vector = this.projectileSpeed * vector.normalized;
			if (this.rotateProjectile)
			{
				component.angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			}
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO == null)
			{
				return;
			}
			soundEffectSO.Play(null);
		}

				[SerializeField]
		private string projectileOPTag;

				[SerializeField]
		private float projectileSpeed;

				[SerializeField]
		private bool rotateProjectile;

				[SerializeField]
		private SoundEffectSO soundFX;
	}
}
