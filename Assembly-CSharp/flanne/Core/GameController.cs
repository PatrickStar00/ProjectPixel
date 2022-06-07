using System;
using flanne.Player;
using flanne.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.Core
{
    public class GameController : StateMachine
    {
        private void Start()
        {
            LeanTween.init(5000000);
            this.ChangeState<InitState>();
        }

        public PlayerInput playerInput;

        public PauseController pauseController;

        public PowerupGenerator powerupGenerator;

        public GameObject player;

        public Health playerHealth;

        public PlayerXP playerXP;

        public GameObject playerFogRevealer;

        public CameraRig playerCameraRig;

        public AudioClip battleMusic;

        public SoundEffectSO youSurvivedSFX;

        public SoundEffectSO youDiedSFX;

        public SoundEffectSO powerupMenuSFX;

        public SoundEffectSO levelUpSFX;

        public Animator levelupAnimator;

        public ScreenFlash screenFlash;

        public ShootingCursor shootingCursor;

        public Panel hud;

        public ChestUIController chestUIController;

        public Panel powerupMenuPanel;

        public PowerupMenu powerupMenu;

        public Button powerupRerollButton;

        public Panel devilDealMenuPanel;

        public PowerupMenu devilDealMenu;

        public Button devilDealLeaveButton;

        public EndScreenUIC endScreenUIC;

        public Button quitToTitleButton;

        public Panel pauseMenu;

        public Button pauseResumeButton;

        public Button optionsButton;

        public Button giveupButton;

        public Panel optionsMenu;

        public Button optionsBackButton;

        public Panel mouseAmmoUI;

        public Panel powerupListUI;
    }
}
