using System;
using CameraShake;
using UnityEngine;

namespace flanne
{
		public class ShortShake2D : MonoBehaviour
	{
				public void Shake()
		{
			CameraShaker.Presets.ShortShake2D(this.positionStrength, this.rotationStrength, 25f, 5);
		}

				[SerializeField]
		private float positionStrength;

				[SerializeField]
		private float rotationStrength;
	}
}
