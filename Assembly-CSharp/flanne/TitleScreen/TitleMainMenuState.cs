using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.TitleScreen
{
		public class TitleMainMenuState : TitleScreenState
	{
				public void OnPlayClick()
		{
			this.owner.ChangeState<LoadoutSelectState>();
			base.titlePanel.Hide();
			base.eyes.ResetTrigger("Open");
			base.eyes.SetTrigger("Close");
		}

				public void OnLanguageClick()
		{
			this.owner.ChangeState<LangaugeState>();
		}

				public void OnOptionsClick()
		{
			this.owner.ChangeState<OptionsMenuState>();
		}

				public override void Enter()
		{
			Cursor.visible = true;
			base.StartCoroutine(this.WaitToShowMenuCR());
		}

				public override void Exit()
		{
			base.mainMenuPanel.Hide();
			base.startButton.onClick.RemoveListener(new UnityAction(this.OnPlayClick));
			base.languageButton.onClick.RemoveListener(new UnityAction(this.OnLanguageClick));
			base.optionsButton.onClick.RemoveListener(new UnityAction(this.OnOptionsClick));
		}

				private IEnumerator WaitToShowMenuCR()
		{
			base.eyes.SetTrigger("Open");
			yield return new WaitForSeconds(0.3f);
			base.titlePanel.Show();
			base.mainMenuPanel.Show();
			base.startButton.onClick.AddListener(new UnityAction(this.OnPlayClick));
			base.languageButton.onClick.AddListener(new UnityAction(this.OnLanguageClick));
			base.optionsButton.onClick.AddListener(new UnityAction(this.OnOptionsClick));
			yield break;
		}
	}
}
