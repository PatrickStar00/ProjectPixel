using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace flanne
{
		public class ColorStrobber : MonoBehaviour
	{
				private void OnEnable()
		{
			base.StartCoroutine(this.StartStrobeCR());
		}

				private IEnumerator StartStrobeCR()
		{
			int index = 0;
			for (;;)
			{
				this.targetGraphic.color = this.strobeColors[index];
				int num = index;
				index = num + 1;
				if (index >= this.strobeColors.Length)
				{
					index = 0;
				}
				yield return new WaitForSecondsRealtime(this.timeBetweenColors);
			}
			yield break;
		}

				[SerializeField]
		private Graphic targetGraphic;

				[SerializeField]
		private Color[] strobeColors;

				[SerializeField]
		private float timeBetweenColors;
	}
}
