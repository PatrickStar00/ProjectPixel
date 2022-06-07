using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class SetTMPAtRuntime : MonoBehaviour
	{
				private void Start()
		{
			this.tmp.text = this.text;
		}

				[SerializeField]
		private TMP_Text tmp;

				[SerializeField]
		private string text;
	}
}
