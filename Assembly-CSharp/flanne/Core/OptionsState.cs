using System;
using UnityEngine.Events;

namespace flanne.Core
{
		public class OptionsState : GameState
	{
				private void OnBack()
		{
			this.owner.ChangeState<PauseState>();
		}

				public override void Enter()
		{
			base.optionsMenu.Show();
			base.optionsBackButton.onClick.AddListener(new UnityAction(this.OnBack));
		}

				public override void Exit()
		{
			base.optionsMenu.Hide();
			base.optionsBackButton.onClick.RemoveListener(new UnityAction(this.OnBack));
		}
	}
}
