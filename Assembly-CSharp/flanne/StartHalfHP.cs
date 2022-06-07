using System;
using UnityEngine;

namespace flanne
{
		public class StartHalfHP : MonoBehaviour
	{
				private void Start()
		{
			PlayerController componentInParent = base.GetComponentInParent<PlayerController>();
			componentInParent.playerHealth.HPChange(-1 * componentInParent.playerHealth.maxHP / 2);
		}
	}
}
