using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class FreezeSystem : MonoBehaviour
	{
				private void Awake()
		{
			FreezeSystem.SharedInstance = this;
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
			this.OP.AddObject(this.freezeFXPrefab.name, this.freezeFXPrefab, 100, true);
			this.OP.AddObject(this.freezeFXLargePrefab.name, this.freezeFXLargePrefab, 100, true);
			this._currentTargets = new List<FreezeSystem.FreezeTarget>();
			this.durationMod = new StatMod();
		}

				public bool IsFrozen(GameObject target)
		{
			return this._currentTargets.Find((FreezeSystem.FreezeTarget bt) => bt.target == target) != null;
		}

				public void Freeze(GameObject target, float duration)
		{
			FreezeSystem.FreezeTarget freezeTarget = this._currentTargets.Find((FreezeSystem.FreezeTarget bt) => bt.target == target);
			float num;
			if (target.tag.Contains("Champion"))
			{
				num = duration / 10f;
			}
			else
			{
				num = duration;
			}
			if (freezeTarget == null)
			{
				freezeTarget = new FreezeSystem.FreezeTarget(target, this.durationMod.Modify(num));
				base.StartCoroutine(this.StartFreezeCR(freezeTarget));
			}
			else if (num > freezeTarget.duration)
			{
				freezeTarget.duration = this.durationMod.Modify(num);
			}
			SoundEffectSO soundEffectSO = this.soundFX;
			if (soundEffectSO == null)
			{
				return;
			}
			soundEffectSO.Play(null);
		}

				private IEnumerator StartFreezeCR(FreezeSystem.FreezeTarget freezeTarget)
		{
			this.PostNotification(FreezeSystem.InflictFreezeEvent, freezeTarget.target);
			freezeTarget.target.GetComponent<AIComponent>().frozen = true;
			Animator animator = freezeTarget.target.GetComponent<Animator>();
			if (animator != null)
			{
				animator.speed = 0f;
			}
			this._currentTargets.Add(freezeTarget);
			string name;
			if (freezeTarget.target.tag.Contains("Champion"))
			{
				name = this.freezeFXLargePrefab.name;
			}
			else
			{
				name = this.freezeFXPrefab.name;
			}
			GameObject freezeObj = this.OP.GetPooledObject(name);
			freezeObj.transform.SetParent(freezeTarget.target.transform);
			freezeObj.transform.localPosition = Vector3.zero;
			freezeObj.SetActive(true);
			while (freezeTarget.duration > 0f && freezeTarget.target.activeInHierarchy)
			{
				yield return null;
				freezeTarget.duration -= Time.deltaTime;
			}
			freezeTarget.target.GetComponent<AIComponent>().frozen = false;
			if (animator != null)
			{
				animator.speed = 1f;
			}
			this._currentTargets.Remove(freezeTarget);
			freezeObj.transform.SetParent(this.OP.transform);
			freezeObj.SetActive(false);
			yield break;
		}

				public static FreezeSystem SharedInstance;

				public static string InflictFreezeEvent = "FreezeSystem.InflictFreezeEvent";

				public StatMod durationMod;

				[SerializeField]
		private GameObject freezeFXPrefab;

				[SerializeField]
		private GameObject freezeFXLargePrefab;

				[SerializeField]
		private SoundEffectSO soundFX;

				private ObjectPooler OP;

				private List<FreezeSystem.FreezeTarget> _currentTargets;

				private class FreezeTarget
		{
						public FreezeTarget(GameObject t, float d)
			{
				this.target = t;
				this.duration = d;
			}

						public GameObject target;

						public float duration;
		}
	}
}
