using System;
using System.Collections;
using flanne.UI;
using UnityEngine;

namespace flanne
{
		public class ScreenFlash : MonoBehaviour
	{
				public void Flash(int numTimes)
		{
			if (this.flashhCoroutine != null)
			{
				base.StopCoroutine(this.flashhCoroutine);
				this.flashhCoroutine = null;
				return;
			}
			this.flashhCoroutine = this.FlashCR(numTimes);
			base.StartCoroutine(this.flashhCoroutine);
		}

				private IEnumerator FlashCR(int numTimes)
		{
			int num;
			for (int i = 0; i < numTimes; i = num + 1)
			{
				this.flashPanel.Show();
				yield return new WaitForSecondsRealtime(0.05f);
				this.flashPanel.Hide();
				yield return new WaitForSecondsRealtime(0.05f);
				num = i;
			}
			this.flashhCoroutine = null;
			yield break;
		}

				[SerializeField]
		private Panel flashPanel;

				private IEnumerator flashhCoroutine;
	}
}
