using System;
using flanne.Player;
using flanne.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.Core
{
    public abstract class GameState : State
    {
        protected PlayerInput playerInput
        {
            get
            {
                return this.owner.playerInput;
            }
        }

        protected PauseController pauseController
        {
            get
            {
                return this.owner.pauseController;
            }
        }

        protected PowerupGenerator powerupGenerator
        {
            get
            {
                return this.owner.powerupGenerator;
            }
        }

        protected GameObject player
        {
            get
            {
                return this.owner.player;
            }
        }

        protected Health playerHealth
        {
            get
            {
                return this.owner.playerHealth;
            }
        }

        protected PlayerXP playerXP
        {
            get
            {
                return this.owner.playerXP;
            }
        }

        protected GameObject playerFogRevealer
        {
            get
            {
                return this.owner.playerFogRevealer;
            }
        }

        protected CameraRig playerCameraRig
        {
            get
            {
                return this.owner.playerCameraRig;
            }
        }

        protected AudioClip battleMusic
        {
            get
            {
                return this.owner.battleMusic;
            }
        }

        protected SoundEffectSO youSurvivedSFX
        {
            get
            {
                return this.owner.youSurvivedSFX;
            }
        }

        protected SoundEffectSO youDiedSFX
        {
            get
            {
                return this.owner.youDiedSFX;
            }
        }

        protected SoundEffectSO powerupMenuSFX
        {
            get
            {
                return this.owner.powerupMenuSFX;
            }
        }

        protected SoundEffectSO levelUpSFX
        {
            get
            {
                return this.owner.levelUpSFX;
            }
        }

        protected Animator levelupAnimator
        {
            get
            {
                return this.owner.levelupAnimator;
            }
        }

        protected ScreenFlash screenFlash
        {
            get
            {
                return this.owner.screenFlash;
            }
        }

        protected ShootingCursor shootingCursor
        {
            get
            {
                return this.owner.shootingCursor;
            }
        }

        protected Panel hud
        {
            get
            {
                return this.owner.hud;
            }
        }

        protected Panel powerupMenuPanel
        {
            get
            {
                return this.owner.powerupMenuPanel;
            }
        }

        protected PowerupMenu powerupMenu
        {
            get
            {
                return this.owner.powerupMenu;
            }
        }

        protected Button powerupRerollButton
        {
            get
            {
                return this.owner.powerupRerollButton;
            }
        }

        protected Panel devilDealMenuPanel
        {
            get
            {
                return this.owner.devilDealMenuPanel;
            }
        }

        protected PowerupMenu devilDealMenu
        {
            get
            {
                return this.owner.devilDealMenu;
            }
        }

        protected Button devilDealLeaveButton
        {
            get
            {
                return this.owner.devilDealLeaveButton;
            }
        }

        protected ChestUIController chestUIController
        {
            get
            {
                return this.owner.chestUIController;
            }
        }

        protected EndScreenUIC endScreenUIC
        {
            get
            {
                return this.owner.endScreenUIC;
            }
        }

        protected Button quitToTitleButton
        {
            get
            {
                return this.owner.quitToTitleButton;
            }
        }

        protected Panel pauseMenu
        {
            get
            {
                return this.owner.pauseMenu;
            }
        }

        protected Button pauseResumeButton
        {
            get
            {
                return this.owner.pauseResumeButton;
            }
        }

        protected Button optionsButton
        {
            get
            {
                return this.owner.optionsButton;
            }
        }

        protected Button giveupButton
        {
            get
            {
                return this.owner.giveupButton;
            }
        }

        protected Panel optionsMenu
        {
            get
            {
                return this.owner.optionsMenu;
            }
        }

        protected Button optionsBackButton
        {
            get
            {
                return this.owner.optionsBackButton;
            }
        }

        protected Panel mouseAmmoUI
        {
            get
            {
                return this.owner.mouseAmmoUI;
            }
        }

        protected Panel powerupListUI
        {
            get
            {
                return this.owner.powerupListUI;
            }
        }

        private void Awake()
        {
            this.owner = base.GetComponentInParent<GameController>();
        }

        protected GameController owner;
    }
}
