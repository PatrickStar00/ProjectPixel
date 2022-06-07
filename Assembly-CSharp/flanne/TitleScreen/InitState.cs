using System;
using System.Collections;
using UnityEngine;

namespace flanne.TitleScreen
{
		public class InitState : TitleScreenState
	{
				public override void Enter()
		{
			base.StartCoroutine(this.WaitToLoadCR());
			AudioManager.Instance.PlayMusic(base.titleScreenMusic);
			AudioManager.Instance.FadeInMusic(5f);
			SaveSystem.Load();
			base.characterUnlocker.LoadData(SaveSystem.data.characterUnlocks);
			base.gunUnlocker.LoadData(SaveSystem.data.gunUnlocks);
			base.runeUnlocker.LoadData(SaveSystem.data.runeUnlocks);
			PointsTracker.pts = SaveSystem.data.points;
			base.swordRuneTree.SetSelections(SaveSystem.data.swordRuneSelections);
			base.shieldRuneTree.SetSelections(SaveSystem.data.shieldRuneSelections);
		}

				private IEnumerator WaitToLoadCR()
		{
			yield return new WaitForSeconds(0.5f);
			base.screenCover.enabled = false;
			this.owner.ChangeState<TitleMainMenuState>();
			yield break;
		}
	}
}
