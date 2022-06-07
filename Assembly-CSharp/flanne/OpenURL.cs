using System;
using UnityEngine;

namespace flanne
{
		public class OpenURL : MonoBehaviour
	{
				public void GoToURL()
		{
			Application.OpenURL(this.url);
		}

				[SerializeField]
		private string url;
	}
}
