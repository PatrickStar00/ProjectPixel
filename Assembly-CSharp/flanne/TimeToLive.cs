using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
		public class TimeToLive : MonoBehaviour
	{
				public void Refresh()
		{
			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
				this._coroutine = null;
			}
			this._coroutine = this.Deactivate();
			base.StartCoroutine(this._coroutine);
		}

				private void OnEnable()
		{
			this.Refresh();
		}

				private void OnDisable()
		{
			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
			}
		}

				private IEnumerator Deactivate()
		{
			yield return new WaitForSeconds(this.lifetime);
			base.gameObject.SetActive(false);
			this._coroutine = null;
			yield break;
		}

				[SerializeField]
		private float lifetime;

				private IEnumerator _coroutine;
	}
}
