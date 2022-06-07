using System;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class ColorTintOnToggle : MonoBehaviour
	{
				private void Awake()
		{
			this.toggle.onValueChanged.AddListener(delegate(bool <p0>)
			{
				this.ToggleValueChanged(this.toggle);
			});
			this._originalColor = this.targetGraphic.color;
		}

				private void ToggleValueChanged(Toggle change)
		{
			if (this.toggle.isOn)
			{
				this.targetGraphic.color = this.toggleOnColor;
				return;
			}
			this.targetGraphic.color = this._originalColor;
		}

				[SerializeField]
		private Toggle toggle;

				[SerializeField]
		private Graphic targetGraphic;

				[SerializeField]
		private Color toggleOnColor = Color.white;

				private Color _originalColor;
	}
}
