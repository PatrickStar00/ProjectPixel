using System;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.UI
{
		public class TriggerAnimationOnToggle : MonoBehaviour
	{
				private void Start()
		{
			this.toggle.onValueChanged.AddListener(delegate(bool <p0>)
			{
				this.ToggleValueChanged(this.toggle);
			});
		}

				private void ToggleValueChanged(Toggle change)
		{
			this.TriggerAnimation();
		}

				private void TriggerAnimation()
		{
			if (this.toggle.isOn)
			{
				this.animator.ResetTrigger(this.toggleOffTrigger);
				this.animator.SetTrigger(this.toggleOnTrigger);
				return;
			}
			this.animator.ResetTrigger(this.toggleOnTrigger);
			this.animator.SetTrigger(this.toggleOffTrigger);
		}

				[SerializeField]
		private Toggle toggle;

				[SerializeField]
		private Animator animator;

				[SerializeField]
		private string toggleOnTrigger;

				[SerializeField]
		private string toggleOffTrigger;
	}
}
