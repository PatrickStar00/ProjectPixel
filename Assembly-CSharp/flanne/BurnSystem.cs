using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class BurnSystem : MonoBehaviour
	{
				private void Awake()
		{
			BurnSystem.SharedInstance = this;
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.burnFXOPTag, this.burnFXPrefab, 100, true);
			this.burnDamageMultiplier = new Multiplier();
			this._currentTargets = new List<BurnSystem.BurnTarget>();
		}

				public bool IsBurning(GameObject target)
		{
			return this._currentTargets.Find((BurnSystem.BurnTarget bt) => bt.target == target) != null;
		}

				public void Burn(GameObject target, int burnDamage)
		{
			BurnSystem.BurnTarget burnTarget = this._currentTargets.Find((BurnSystem.BurnTarget bt) => bt.target == target);
			if (burnTarget == null)
			{
				Health component = target.GetComponent<Health>();
				if (component != null)
				{
					base.StartCoroutine(this.StartBurnCR(component, burnDamage));
				}
				else
				{
					Debug.LogWarning("No health attached to burn target: " + target);
				}
			}
			else
			{
				base.StartCoroutine(this.AddBurnCR(burnTarget, burnDamage, this.burnDuration));
			}
			this.PostNotification(BurnSystem.InflictBurnEvent, null);
		}

				private IEnumerator StartBurnCR(Health targetHealth, int burnDamage)
		{
			BurnSystem.BurnTarget burnTarget = new BurnSystem.BurnTarget(targetHealth.gameObject, 0);
			base.StartCoroutine(this.AddBurnCR(burnTarget, burnDamage, this.burnDuration));
			this._currentTargets.Add(burnTarget);
			GameObject burnObj = this.OP.GetPooledObject(this.burnFXOPTag);
			burnObj.transform.SetParent(targetHealth.transform);
			burnObj.transform.localPosition = Vector3.zero;
			burnObj.SetActive(true);
			yield return null;
			while (targetHealth.gameObject.activeInHierarchy && burnTarget.damage > 0)
			{
				yield return new WaitForSeconds(1f);
				targetHealth.HPChange(-1 * Mathf.FloorToInt((float)burnTarget.damage * this.burnDamageMultiplier.value));
			}
			burnObj.transform.SetParent(this.OP.transform);
			burnObj.SetActive(false);
			this._currentTargets.Remove(burnTarget);
			yield break;
		}

				private IEnumerator AddBurnCR(BurnSystem.BurnTarget burnTarget, int burnDamage, float duration)
		{
			burnTarget.damage += burnDamage;
			yield return new WaitForSeconds(duration);
			burnTarget.damage -= burnDamage;
			yield break;
		}

				public static BurnSystem SharedInstance;

				public static string InflictBurnEvent = "BurnSystem.InflictBurnEvent";

				[SerializeField]
		private GameObject burnFXPrefab;

				[SerializeField]
		private string burnFXOPTag;

				public Multiplier burnDamageMultiplier;

				private float burnDuration = 4.1f;

				private ObjectPooler OP;

				private List<BurnSystem.BurnTarget> _currentTargets;

				private class BurnTarget
		{
						public BurnTarget(GameObject t, int d)
			{
				this.target = t;
				this.damage = d;
			}

						public GameObject target;

						public int damage;
		}
	}
}
