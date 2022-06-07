using System;
using System.Collections;
using UnityEngine;

namespace flanne.Core
{
		public class PauseController : MonoBehaviour
	{
				private void Awake()
		{
			PauseController.isPaused = false;
			PauseController.SharedInstance = this;
		}

				private void Start()
		{
			this.pauseCoroutine = null;
		}

				public void Pause()
		{
			Time.timeScale = 0f;
			PauseController.isPaused = true;
		}

				public void Pause(float duration)
		{
			if (this.pauseCoroutine != null)
			{
				base.StopCoroutine(this.pauseCoroutine);
			}
			this.pauseCoroutine = this.PauseCR(duration);
			base.StartCoroutine(this.pauseCoroutine);
		}

				public void UnPause()
		{
			Time.timeScale = 1f;
			PauseController.isPaused = false;
		}

				private IEnumerator PauseCR(float duration)
		{
			this.Pause();
			yield return new WaitForSecondsRealtime(duration);
			this.UnPause();
			this.pauseCoroutine = null;
			yield break;
		}

				public static PauseController SharedInstance;

				public static bool isPaused;

				private IEnumerator pauseCoroutine;
	}
}
