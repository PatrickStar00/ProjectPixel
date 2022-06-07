using System;
using System.Collections;
using System.Collections.Generic;
using flanne.RuneSystem;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.TitleScreen
{
		public class LoadoutSelectState : TitleScreenState
	{
				public void OnClickPlay()
		{
			this.SetLoadout();
			this.owner.ChangeState<WaitToLoadIntoBattleState>();
			base.selectPanel.Hide();
		}

				public void OnClickBack()
		{
			this.owner.ChangeState<TitleMainMenuState>();
			base.selectPanel.Hide();
		}

				public void OnClickRunes()
		{
			this.owner.ChangeState<RuneMenuState>();
			base.selectPanel.interactable = false;
		}

				public override void Enter()
		{
			base.StartCoroutine(this.WaitToShowCR());
			base.characterSelectMenu.RefreshDescription();
			base.gunSelectMenu.RefreshDescription();
			base.loadoutPlayButton.onClick.AddListener(new UnityAction(this.OnClickPlay));
			base.loadoutBackButton.onClick.AddListener(new UnityAction(this.OnClickBack));
			base.runesButton.onClick.AddListener(new UnityAction(this.OnClickRunes));
			base.gamepadCursor.SetActive(true);
		}

				public override void Exit()
		{
			base.loadoutPlayButton.onClick.RemoveListener(new UnityAction(this.OnClickPlay));
			base.loadoutBackButton.onClick.RemoveListener(new UnityAction(this.OnClickBack));
			base.runesButton.onClick.RemoveListener(new UnityAction(this.OnClickRunes));
			base.gamepadCursor.SetActive(false);
			this.Save();
		}

				private void Save()
		{
			SaveSystem.data.points = PointsTracker.pts;
			SaveSystem.data.characterUnlocks = base.characterUnlocker.unlockData;
			SaveSystem.data.gunUnlocks = base.gunUnlocker.unlockData;
			SaveSystem.data.runeUnlocks = base.runeUnlocker.unlockData;
			SaveSystem.data.swordRuneSelections = base.swordRuneTree.GetSelections();
			SaveSystem.data.shieldRuneSelections = base.shieldRuneTree.GetSelections();
			SaveSystem.Save();
		}

				private void SetLoadout()
		{
			Loadout.CharacterSelection = base.characterSelectMenu.toggledData;
			Loadout.GunSelection = base.gunSelectMenu.toggledData;
			List<RuneData> activeRunes = base.swordRuneTree.GetActiveRunes();
			activeRunes.AddRange(base.shieldRuneTree.GetActiveRunes());
			Loadout.RuneSelection = activeRunes;
		}

				private IEnumerator WaitToShowCR()
		{
			yield return new WaitForSeconds(0.2f);
			base.selectPanel.Show();
			yield break;
		}
	}
}
