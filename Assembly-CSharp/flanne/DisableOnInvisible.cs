using System;
using UnityEngine;

namespace flanne
{
		public class DisableOnInvisible : MonoBehaviour
	{
				private void OnBecameInvisible()
		{
			base.gameObject.SetActive(false);
		}
	}
}
