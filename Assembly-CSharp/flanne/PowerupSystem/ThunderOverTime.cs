using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne.PowerupSystem
{
		public class ThunderOverTime : MonoBehaviour
	{
				private void Start()
		{
			this.TGen = ThunderGenerator.SharedInstance;
		}

				private void Update()
		{
			this._timer += Time.deltaTime;
			if (this._timer > this.cooldown)
			{
				this._timer -= this.cooldown;
				for (int i = 0; i < this.thundersPerWave; i++)
				{
					GameObject newTarget = this.GetNewTarget();
					if (newTarget != null)
					{
						this.TGen.GenerateAt(newTarget, this.baseDamage);
					}
				}
			}
		}

				private GameObject GetNewTarget()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			List<GameObject> list = new List<GameObject>();
			foreach (GameObject gameObject in array)
			{
				if (this.IsWithinRange(gameObject.transform.position))
				{
					list.Add(gameObject);
				}
			}
			if (list.Count > 0)
			{
				return array[Random.Range(0, array.Length)];
			}
			return null;
		}

				private bool IsWithinRange(Vector2 pos)
		{
			return this.player != null && Mathf.Abs(this.player.position.x - pos.x) < this.rangeX && Mathf.Abs(this.player.position.y - pos.y) < this.rangeY;
		}

				[SerializeField]
		private int baseDamage;

				[SerializeField]
		private float cooldown;

				[SerializeField]
		private int thundersPerWave;

				[SerializeField]
		private float rangeX;

				[SerializeField]
		private float rangeY;

				private ThunderGenerator TGen;

				[SerializeField]
		private Transform player;

				private float _timer;
	}
}
