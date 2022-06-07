using System;
using UnityEngine;

public abstract class UITweener : MonoBehaviour
{
		public abstract void Show();

		public abstract void Hide();

		public abstract void SetOff();

		public float duration = 0.1f;
}
