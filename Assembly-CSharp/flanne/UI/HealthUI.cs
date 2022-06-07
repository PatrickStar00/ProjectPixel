using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne.UI
{
		public class HealthUI : MonoBehaviour
	{
				public void OnHpChanged(int value)
		{
			this.hp = value;
			this.Refresh();
		}

				public void OnMaxHPChanged(int value)
		{
			this.mhp = value;
			this.Refresh();
		}

				private void Refresh()
		{
			if (this.hearts == null)
			{
				this.hearts = new List<GameObject>();
			}
			foreach (GameObject gameObject in this.hearts)
			{
				Object.Destroy(gameObject);
			}
			this.hearts.Clear();
			int i;
			for (i = 0; i < this.hp; i++)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.heartPrefab);
				gameObject2.transform.SetParent(base.transform);
				gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
				this.hearts.Add(gameObject2);
			}
			while (i < this.mhp)
			{
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.emptyHeartPrefab);
				gameObject3.transform.SetParent(base.transform);
				gameObject3.transform.localScale = new Vector3(1f, 1f, 1f);
				this.hearts.Add(gameObject3);
				i++;
			}
		}

				[SerializeField]
		private GameObject heartPrefab;

				[SerializeField]
		private GameObject emptyHeartPrefab;

				private List<GameObject> hearts;

				private int hp;

				private int mhp;
	}
}
