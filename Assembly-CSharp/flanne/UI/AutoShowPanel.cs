using System;
using System.Collections;
using UnityEngine;

namespace flanne.UI
{
		[RequireComponent(typeof(Panel))]
	public class AutoShowPanel : MonoBehaviour
	{
				private void Start()
		{
			this.panel = base.GetComponent<Panel>();
			base.StartCoroutine(this.AutoShowCR());
		}

				private IEnumerator AutoShowCR()
		{
			yield return new WaitForSecondsRealtime(this.startTime);
			this.panel.Show();
			yield return new WaitForSecondsRealtime(this.duration);
			this.panel.Hide();
			yield break;
		}

				[SerializeField]
		private float startTime;

				[SerializeField]
		private float duration;

				private Panel panel;
	}
}
