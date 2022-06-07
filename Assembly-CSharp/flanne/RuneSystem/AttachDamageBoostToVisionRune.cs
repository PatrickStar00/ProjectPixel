using System;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class AttachDamageBoostToVisionRune : Rune
	{
				protected override void Init()
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerVision");
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.damageBoostPrefab.gameObject);
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.SetActive(true);
			gameObject2.GetComponent<DamageBoostInRange>().damageBoost = this.damageBoostPerLevel * (float)this.level;
		}

				[SerializeField]
		private DamageBoostInRange damageBoostPrefab;

				[SerializeField]
		private float damageBoostPerLevel;
	}
}
