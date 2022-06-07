using System;
using UnityEngine;

namespace flanne.Core
{
		public class Minipause : MonoBehaviour
	{
				public void Pause(float duration)
		{
			PauseController.SharedInstance.Pause(duration);
		}
	}
}
