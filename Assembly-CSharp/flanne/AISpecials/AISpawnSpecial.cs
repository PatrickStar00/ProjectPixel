using System;
using System.Collections;
using UnityEngine;

namespace flanne.AISpecials
{
		[CreateAssetMenu(fileName = "AISpawnSpecial", menuName = "AISpecials/AISpawnSpecial")]
	public class AISpawnSpecial : AISpecial
	{
				public override void Use(AIComponent ai, Transform target)
		{
			Spawner componentInChildren = ai.GetComponentInChildren<Spawner>();
			ai.StartCoroutine(this.WaitToSpawn(this.numSpawns, componentInChildren));
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO != null)
			{
				soundEffectSO.Play(null);
			}
			Animator animator = ai.animator;
			if (animator == null)
			{
				return;
			}
			animator.SetTrigger("Special");
		}

				private IEnumerator WaitToSpawn(int numSpawns, Spawner spawner)
		{
			int num;
			for (int i = 0; i < numSpawns; i = num + 1)
			{
				yield return new WaitForSeconds(this.spawnWinduptime);
				if (spawner != null)
				{
					spawner.Spawn();
				}
				num = i;
			}
			yield break;
		}

				[SerializeField]
		private SoundEffectSO soundFX;

				[SerializeField]
		private float spawnWinduptime;

				[SerializeField]
		private int numSpawns;
	}
}
