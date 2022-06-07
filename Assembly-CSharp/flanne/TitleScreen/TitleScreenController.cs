using System;
using flanne.UI;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.TitleScreen
{
		public class TitleScreenController : StateMachine
	{
				private void Awake()
		{
			LeanTween.init(500000);
		}

				private void Start()
		{
			this.ChangeState<InitState>();
		}

				public GameObject gamepadCursor;

				public Image screenCover;

				public Panel titlePanel;

				public Panel selectPanel;

				public Panel mainMenuPanel;

				public Panel languageMenuPanel;

				public Panel optionsMenuPanel;

				public Panel runeMenuPanel;

				public Animator eyes;

				public AudioClip titleScreenMusic;

				public CharacterMenu characterSelectMenu;

				public GunMenu gunSelectMenu;

				public UnlockablesManager characterUnlocker;

				public UnlockablesManager gunUnlocker;

				public TieredUnlockManager runeUnlocker;

				public RuneTreeUI swordRuneTree;

				public RuneTreeUI shieldRuneTree;

				public Button[] changeLanguageButtons;

				public Button startButton;

				public Button languageButton;

				public Button optionsButton;

				public Button optionsBackButton;

				public Button loadoutPlayButton;

				public Button loadoutBackButton;

				public Button runesButton;

				public Button runeConfirmButton;
	}
}
