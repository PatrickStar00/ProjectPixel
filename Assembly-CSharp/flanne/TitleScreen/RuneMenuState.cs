using System;
using UnityEngine.Events;

namespace flanne.TitleScreen
{
		public class RuneMenuState : TitleScreenState
	{
				private void OnConfirmClick()
		{
			this.owner.ChangeState<LoadoutSelectState>();
		}

				public override void Enter()
		{
			base.runeMenuPanel.Show();
			base.runeConfirmButton.onClick.AddListener(new UnityAction(this.OnConfirmClick));
		}

				public override void Exit()
		{
			base.runeMenuPanel.Hide();
			base.runeConfirmButton.onClick.RemoveListener(new UnityAction(this.OnConfirmClick));
		}
	}
}
