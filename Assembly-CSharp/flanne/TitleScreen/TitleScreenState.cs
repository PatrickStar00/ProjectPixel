using System;
using flanne.UI;
using UnityEngine;
using UnityEngine.UI;

namespace flanne.TitleScreen
{
		public abstract class TitleScreenState : State
	{
						protected GameObject gamepadCursor
		{
			get
			{
				return this.owner.gamepadCursor;
			}
		}

						protected Image screenCover
		{
			get
			{
				return this.owner.screenCover;
			}
		}

						protected Panel titlePanel
		{
			get
			{
				return this.owner.titlePanel;
			}
		}

						protected Panel selectPanel
		{
			get
			{
				return this.owner.selectPanel;
			}
		}

						protected Panel mainMenuPanel
		{
			get
			{
				return this.owner.mainMenuPanel;
			}
		}

						protected Panel languageMenuPanel
		{
			get
			{
				return this.owner.languageMenuPanel;
			}
		}

						protected Panel optionsMenuPanel
		{
			get
			{
				return this.owner.optionsMenuPanel;
			}
		}

						protected Panel runeMenuPanel
		{
			get
			{
				return this.owner.runeMenuPanel;
			}
		}

						protected Animator eyes
		{
			get
			{
				return this.owner.eyes;
			}
		}

						protected AudioClip titleScreenMusic
		{
			get
			{
				return this.owner.titleScreenMusic;
			}
		}

						protected CharacterMenu characterSelectMenu
		{
			get
			{
				return this.owner.characterSelectMenu;
			}
		}

						protected GunMenu gunSelectMenu
		{
			get
			{
				return this.owner.gunSelectMenu;
			}
		}

						protected UnlockablesManager characterUnlocker
		{
			get
			{
				return this.owner.characterUnlocker;
			}
		}

						protected UnlockablesManager gunUnlocker
		{
			get
			{
				return this.owner.gunUnlocker;
			}
		}

						protected TieredUnlockManager runeUnlocker
		{
			get
			{
				return this.owner.runeUnlocker;
			}
		}

						protected RuneTreeUI swordRuneTree
		{
			get
			{
				return this.owner.swordRuneTree;
			}
		}

						protected RuneTreeUI shieldRuneTree
		{
			get
			{
				return this.owner.shieldRuneTree;
			}
		}

						protected Button[] changeLanguageButtons
		{
			get
			{
				return this.owner.changeLanguageButtons;
			}
		}

						protected Button startButton
		{
			get
			{
				return this.owner.startButton;
			}
		}

						protected Button languageButton
		{
			get
			{
				return this.owner.languageButton;
			}
		}

						protected Button optionsButton
		{
			get
			{
				return this.owner.optionsButton;
			}
		}

						protected Button optionsBackButton
		{
			get
			{
				return this.owner.optionsBackButton;
			}
		}

						protected Button loadoutPlayButton
		{
			get
			{
				return this.owner.loadoutPlayButton;
			}
		}

						protected Button loadoutBackButton
		{
			get
			{
				return this.owner.loadoutBackButton;
			}
		}

						protected Button runesButton
		{
			get
			{
				return this.owner.runesButton;
			}
		}

						protected Button runeConfirmButton
		{
			get
			{
				return this.owner.runeConfirmButton;
			}
		}

				private void Awake()
		{
			this.owner = base.GetComponentInParent<TitleScreenController>();
		}

				protected TitleScreenController owner;
	}
}
