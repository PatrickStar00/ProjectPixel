using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class SetTMPToInt : MonoBehaviour
	{
				public void SetToInt(int value)
		{
			string text = "";
			for (int i = 0; i < this.minDigits; i++)
			{
				text += "0";
			}
			this.tmp.text = value.ToString(text);
		}

				[SerializeField]
		private TMP_Text tmp;

				[SerializeField]
		private int minDigits;
	}
}
