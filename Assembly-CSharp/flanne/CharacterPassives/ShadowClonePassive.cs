using System;
using System.Collections;
using UnityEngine;

namespace flanne.CharacterPassives
{
		public class ShadowClonePassive : SkillPassive
	{
				protected override void Init()
		{
			this.player = base.transform.root.GetComponent<PlayerController>();
			this.spriteTrail.mLeadingSprite = this.player.playerSprite;
			this.shadowClone.SetActive(false);
		}

				protected override void PerformSkill()
		{
			base.StartCoroutine(this.DashCR(this.player));
			this.SummonShadowClone();
		}

				private void SummonShadowClone()
		{
			this.shadowClone.transform.position = this.player.transform.position;
			this.shadowClone.SetActive(true);
			this.shadowCloneLifetime.Refresh();
		}

				private IEnumerator DashCR(PlayerController player)
		{
			float originalMoveSpeed = player.movementSpeed;
			player.movementSpeed *= this.dashSpeedMulti;
			SpriteTrail spriteTrail = this.spriteTrail;
			if (spriteTrail != null)
			{
				spriteTrail.SetEnabled(true);
			}
			yield return new WaitForSeconds(this.dashDuration);
			player.movementSpeed = originalMoveSpeed;
			SpriteTrail spriteTrail2 = this.spriteTrail;
			if (spriteTrail2 != null)
			{
				spriteTrail2.SetEnabled(false);
			}
			yield break;
		}

				[SerializeField]
		private float dashSpeedMulti;

				[SerializeField]
		private float dashDuration;

				[SerializeField]
		private GameObject shadowClone;

				[SerializeField]
		private TimeToLive shadowCloneLifetime;

				[SerializeField]
		private SpriteTrail spriteTrail;

				private PlayerController player;
	}
}
