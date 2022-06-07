using System;
using UnityEngine;

namespace flanne
{
		public class DisableAction : MonoBehaviour
	{
				public void Disable()
		{
			base.gameObject.SetActive(false);
		}
	}
}
