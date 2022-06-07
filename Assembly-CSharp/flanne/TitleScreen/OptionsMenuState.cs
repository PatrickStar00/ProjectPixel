using System;
using flanne.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace flanne.TitleScreen
{
		public class OptionsMenuState : TitleScreenState
	{
				public void OnClick()
		{
			this.owner.ChangeState<TitleMainMenuState>();
		}

				public override void Enter()
		{
			base.optionsBackButton.onClick.AddListener(new UnityAction(this.OnClick));
			base.optionsMenuPanel.GetComponent<OptionsSetter>().Refresh();
			base.optionsMenuPanel.Show();
			if (Gamepad.current != null)
			{
				base.optionsBackButton.Select();
			}
		}

				public override void Exit()
		{
			base.optionsBackButton.onClick.RemoveListener(new UnityAction(this.OnClick));
			base.optionsMenuPanel.Hide();
		}
	}
}
