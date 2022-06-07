using System;
using UnityEngine;

namespace flanne
{
		public class Spinning : MonoBehaviour
	{
				private void Update()
		{
			base.transform.Rotate(this.spin.x, this.spin.y, this.spin.z * Time.deltaTime);
		}

				[SerializeField]
		private Vector3 spin = Vector3.zero;
	}
}
