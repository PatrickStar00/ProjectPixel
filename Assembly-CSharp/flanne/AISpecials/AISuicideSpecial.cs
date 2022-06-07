﻿using System;
using System.Collections;
using UnityEngine;

namespace flanne.AISpecials
{
		[CreateAssetMenu(fileName = "AISuicideSpecial", menuName = "AISpecials/AISuicideSpecial")]
	public class AISuicideSpecial : AISpecial
	{
				public override void Use(AIComponent ai, Transform target)
		{
			Health component = ai.GetComponent<Health>();
			if (component != null)
			{
				ai.StartCoroutine(this.WaitToSuicide(component));
				SoundEffectSO soundEffectSO = this.soundFX;
				if (soundEffectSO != null)
				{
					soundEffectSO.Play(null);
				}
				FlashSprite component2 = ai.GetComponent<FlashSprite>();
				if (component2 != null)
				{
					ai.StartCoroutine(this.FlashWarning(component2));
					return;
				}
			}
			else
			{
				Debug.LogWarning("AI is missing health component: " + ai.name);
			}
		}

				private IEnumerator FlashWarning(FlashSprite flasher)
		{
			float flashTime = this.timeToActivate - 0.2f;
			for (float timer = 0f; timer < flashTime; timer += 0.1f)
			{
				flasher.Flash();
				yield return new WaitForSeconds(0.2f);
			}
			yield break;
		}

				private IEnumerator WaitToSuicide(Health health)
		{
			yield return new WaitForSeconds(this.timeToActivate);
			health.AutoKill();
			yield break;
		}

				[SerializeField]
		private float timeToActivate;

				[SerializeField]
		private SoundEffectSO soundFX;
	}
}
