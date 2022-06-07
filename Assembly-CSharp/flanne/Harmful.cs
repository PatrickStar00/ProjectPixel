using System;
using UnityEngine;

namespace flanne
{
		public class Harmful : MonoBehaviour
	{
				private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag.Contains(this.hitTag))
			{
				Health component = other.gameObject.GetComponent<Health>();
				if (component != null)
				{
					component.HPChange(-1 * this.damageAmount);
				}
			}
		}

				[SerializeField]
		private string hitTag;

				public int damageAmount;
	}
}
