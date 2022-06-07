using System;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class UIBar : MonoBehaviour
	{
				public void SetValue(int value)
		{
			this.slider.value = (float)value;
		}

				public void SetMax(int maxValue)
		{
			this.slider.maxValue = (float)maxValue;
		}

				[SerializeField]
		private Slider slider;
	}
}
