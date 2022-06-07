using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class ProjectileSummon : Summon
	{
				protected override void Init()
		{
			SeekEnemy component = base.GetComponent<SeekEnemy>();
			if (component != null)
			{
				component.player = this.player.transform;
			}
		}

				private void OnEnable()
		{
			base.StartCoroutine(this.WaitToUnParentCR());
		}

				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag == "Enemy")
			{
				Health component = other.gameObject.GetComponent<Health>();
				if (component != null)
				{
					component.HPChange(Mathf.FloorToInt(-1f * base.summonDamageMod.Modify((float)this.baseDamage)));
				}
				base.gameObject.SetActive(false);
			}
		}

				private IEnumerator WaitToUnParentCR()
		{
			yield return null;
			base.transform.SetParent(null);
			yield break;
		}

				[SerializeField]
		private int baseDamage = 1;
	}
}
