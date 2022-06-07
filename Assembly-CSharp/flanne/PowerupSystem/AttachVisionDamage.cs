using System;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class AttachVisionDamage : MonoBehaviour
	{
								public PersistentHarm visionDamage { get; private set; }

				private void Start()
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerVision");
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.visionDamagePrefab.gameObject);
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.SetActive(true);
			this.visionDamage = gameObject2.GetComponent<PersistentHarm>();
		}

				[SerializeField]
		private PersistentHarm visionDamagePrefab;
	}
}
