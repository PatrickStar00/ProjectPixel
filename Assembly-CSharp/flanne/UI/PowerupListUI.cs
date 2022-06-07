using System;
using UnityEngine;

namespace flanne.UI
{
		public class PowerupListUI : MonoBehaviour
	{
				private void OnPowerupApplied(object sender, object args)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.powerupIconPrefab);
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.localScale = Vector3.one;
			Powerup data = sender as Powerup;
			gameObject.GetComponent<PowerupIcon>().data = data;
		}

				private void Start()
		{
			this.AddObserver(new Action<object, object>(this.OnPowerupApplied), Powerup.AppliedNotifcation);
		}

				private void OnDestroy()
		{
			this.RemoveObserver(new Action<object, object>(this.OnPowerupApplied), Powerup.AppliedNotifcation);
		}

				[SerializeField]
		private GameObject powerupIconPrefab;
	}
}
