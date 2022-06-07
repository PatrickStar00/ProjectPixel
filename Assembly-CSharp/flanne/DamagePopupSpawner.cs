using System;
using TMPro;
using UnityEngine;

namespace flanne
{
		public class DamagePopupSpawner : MonoBehaviour
	{
				public void OnDamageTaken(int amount)
		{
			GameObject pooledObject = ObjectPooler.SharedInstance.GetPooledObject(this.popupOpTag);
			pooledObject.transform.position = base.transform.position;
			pooledObject.SetActive(true);
			pooledObject.GetComponent<TextMeshPro>().text = Mathf.Abs(amount).ToString();
		}

				[SerializeField]
		private string popupOpTag;
	}
}
