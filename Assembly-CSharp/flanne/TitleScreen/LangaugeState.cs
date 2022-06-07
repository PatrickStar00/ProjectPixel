using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.TitleScreen
{
		public class LangaugeState : TitleScreenState
	{
				public void OnClick()
		{
			this.owner.ChangeState<TitleMainMenuState>();
		}

				public override void Enter()
		{
			Button[] changeLanguageButtons = base.changeLanguageButtons;
			for (int i = 0; i < changeLanguageButtons.Length; i++)
			{
				changeLanguageButtons[i].onClick.AddListener(new UnityAction(this.OnClick));
			}
			base.languageMenuPanel.Show();
			if (Gamepad.current != null)
			{
				base.changeLanguageButtons[0].Select();
			}
		}

				public override void Exit()
		{
			Button[] changeLanguageButtons = base.changeLanguageButtons;
			for (int i = 0; i < changeLanguageButtons.Length; i++)
			{
				changeLanguageButtons[i].onClick.RemoveListener(new UnityAction(this.OnClick));
			}
			base.languageMenuPanel.Hide();
		}
	}
}
