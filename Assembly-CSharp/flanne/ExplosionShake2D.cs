using System;
using CameraShake;
using UnityEngine;

namespace flanne
{
		public class ExplosionShake2D : MonoBehaviour
	{
				public void Shake()
		{
			CameraShaker.Presets.Explosion2D(this.positionStrength, this.rotationStrength, 0.5f);
		}

				[SerializeField]
		private float positionStrength;

				[SerializeField]
		private float rotationStrength;
	}
}
