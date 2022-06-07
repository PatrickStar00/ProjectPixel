using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace flanne
{
		public class DamagePopup : MonoBehaviour
	{
				private void Awake()
		{
			this._startColor = this.tmp.color;
			this.tmp.outlineWidth = 25f;
			this.tmp.outlineColor = this.fadeColor2;
		}

				private void OnEnable()
		{
			this.tmp.color = this._startColor;
			this._coroutine = this.LifeTime();
			base.StartCoroutine(this._coroutine);
		}

				private void OnDisable()
		{
			base.StopCoroutine(this._coroutine);
			LeanTween.cancel(base.gameObject);
		}

				private IEnumerator LifeTime()
		{
			base.transform.position = new Vector3(base.transform.position.x + Random.Range(-0.5f, 0.5f), base.transform.position.y + Random.Range(-0.5f, 0.5f));
			Vector3 newPos = new Vector3(base.transform.position.x + Random.Range(-0.5f, 0.5f), base.transform.position.y + 0.5f, 0f);
			base.transform.localScale = Vector3.one;
			float timer = 0f;
			while (timer < this.lifetime)
			{
				timer += Time.deltaTime;
				yield return null;
				base.transform.position = Vector3.MoveTowards(base.transform.position, newPos, (1f - timer / this.lifetime) / 2f * Time.deltaTime);
				if (timer < this.lifetime * 0.3f)
				{
					base.transform.localScale = Vector3.one + Vector3.one * (timer / 0.3f);
				}
				if (timer > this.lifetime * 0.8f && timer < this.lifetime * 0.89f)
				{
					this.tmp.color = this.fadeColor1;
				}
				if (timer > this.lifetime * 0.9f)
				{
					this.tmp.color = this.fadeColor2;
				}
			}
			base.gameObject.SetActive(false);
			yield break;
		}

				[SerializeField]
		private TextMeshPro tmp;

				[SerializeField]
		private float lifetime;

				[SerializeField]
		private Color fadeColor1;

				[SerializeField]
		private Color fadeColor2;

				private Color _startColor;

				private IEnumerator _coroutine;
	}
}
