using System;
using System.Collections.Generic;
using flanne.RuneSystem;
using UnityEngine;

namespace flanne
{
		public class LoadoutInitializer : MonoBehaviour
	{
				private void Start()
		{
			CharacterData characterSelection = Loadout.CharacterSelection;
			if (characterSelection != null)
			{
				this.playerAnimator.runtimeAnimatorController = characterSelection.animController;
				this.playerHealth.maxHP = characterSelection.startHP;
				if (characterSelection.passivePrefab != null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(characterSelection.passivePrefab);
					gameObject.transform.SetParent(this.player.transform.root);
					gameObject.transform.localPosition = Vector3.zero;
				}
				this.player.loadedCharacter = characterSelection;
			}
			GunData gunSelection = Loadout.GunSelection;
			this.gun.LoadGun(gunSelection);
			List<RuneData> runeSelection = Loadout.RuneSelection;
			if (runeSelection != null)
			{
				foreach (RuneData runeData in runeSelection)
				{
					runeData.Apply(this.player);
				}
			}
		}

				[SerializeField]
		private PlayerController player;

				[SerializeField]
		private Animator playerAnimator;

				[SerializeField]
		private Health playerHealth;

				[SerializeField]
		private Gun gun;
	}
}
