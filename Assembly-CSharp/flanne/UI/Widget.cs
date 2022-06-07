using System;
using UnityEngine;

namespace flanne.UI
{
		public abstract class Widget<T> : MonoBehaviour where T : IUIProperties
	{
				private void Start()
		{
			this.panel = base.GetComponent<Panel>();
		}

				public virtual void Show()
		{
			if (this.panel != null)
			{
				this.panel.Show();
			}
		}

				public virtual void Hide()
		{
			if (this.panel != null)
			{
				this.panel.Hide();
			}
		}

				public abstract void SetProperties(T properties);

				private Panel panel;
	}
}
