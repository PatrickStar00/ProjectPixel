using System;
using flanne.RuneSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class RuneIcon : DataUIBinding<RuneData>
	{
				public override void Refresh()
		{
			this.iconImage.sprite = base.data.icon;
			this.levelTMP.text = base.data.level.ToString();
		}

				[SerializeField]
		private Image iconImage;

				[SerializeField]
		private TMP_Text levelTMP;
	}
}
