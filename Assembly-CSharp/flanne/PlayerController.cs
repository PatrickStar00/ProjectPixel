using System;
using flanne.Core;
using flanne.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne
{
		public class PlayerController : StateMachine
	{
				private void OnMove(InputAction.CallbackContext obj)
		{
			Vector2 vector = obj.ReadValue<Vector2>();
			this.moveVec = new Vector3(vector.x, vector.y, 0f).normalized;
		}

				private void Awake()
		{
			this.stats = new StatsHolder();
			this.disableMove = new BoolToggle(false);
			this.disableAction = new BoolToggle(false);
			this.disableAnimation = new BoolToggle(false);
			this.ChangeState<IdleState>();
			this._moveAction = this.playerInput.actions["Move"];
		}

				private void Start()
		{
			this.SC = ShootingCursor.Instance;
		}

				private void Update()
		{
			if (PauseController.isPaused || this.playerHealth.isDead)
			{
				return;
			}
			this.MovePlayer();
			this.UpdateSprite();
		}

				private void OnEnable()
		{
			this._moveAction.performed += this.OnMove;
		}

				private void OnDisable()
		{
			this._moveAction.performed -= this.OnMove;
			this._currentState.Exit();
		}

				private void MovePlayer()
		{
			if (!this.disableMove.value)
			{
				base.transform.position += this.moveSpeedMultiplier * this.moveVec * this.stats[StatType.MoveSpeed].Modify(this.movementSpeed) * Time.deltaTime;
			}
		}

				private void UpdateSprite()
		{
			if (this.disableAnimation.value)
			{
				return;
			}
			if (this.moveVec == Vector3.zero || this.disableMove.value)
			{
				this.playerAnimator.ResetTrigger("Run");
				this.playerAnimator.ResetTrigger("Walk");
				this.playerAnimator.SetTrigger("Idle");
			}
			else if (this.moveSpeedMultiplier >= 1f)
			{
				this.playerAnimator.ResetTrigger("Idle");
				this.playerAnimator.ResetTrigger("Walk");
				this.playerAnimator.SetTrigger("Run");
				this.timerFootStepSFX += Time.deltaTime;
				if (this.timerFootStepSFX > this.footstepSFXCooldown)
				{
					this.timerFootStepSFX -= this.footstepSFXCooldown;
					this.footstepSFX.Play(null);
				}
			}
			else
			{
				this.playerAnimator.ResetTrigger("Idle");
				this.playerAnimator.ResetTrigger("Run");
				this.playerAnimator.SetTrigger("Walk");
			}
			if (this.playerSprite != null)
			{
				if (this.faceMouse)
				{
					Vector3 vector = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition);
					Vector3 position = base.transform.position;
					Vector3 vector2 = vector - position;
					if (vector2.x < 0f)
					{
						this.playerSprite.flipX = true;
						return;
					}
					if (vector2.x > 0f)
					{
						this.playerSprite.flipX = false;
						return;
					}
				}
				else
				{
					if (this.moveVec.x < 0f)
					{
						this.playerSprite.flipX = true;
						return;
					}
					if (this.moveVec.x > 0f)
					{
						this.playerSprite.flipX = false;
						return;
					}
				}
			}
			else
			{
				Debug.LogError("No sprite renderer on player");
			}
		}

				public PlayerInput playerInput;

				public SpriteRenderer playerSprite;

				public Animator playerAnimator;

				public Health playerHealth;

				public StatsHolder stats;

				public Gun gun;

				public Ammo ammo;

				public Slider reloadBar;

				public CharacterData loadedCharacter;

				public SoundEffectSO reloadStartSFX;

				public SoundEffectSO reloadEndSFX;

				public float movementSpeed = 8f;

				[SerializeField]
		private SoundEffectSO footstepSFX;

				[SerializeField]
		private float footstepSFXCooldown;

				private float timerFootStepSFX;

				[NonSerialized]
		public float moveSpeedMultiplier;

				[NonSerialized]
		public Vector3 moveVec;

				[NonSerialized]
		public bool faceMouse;

				public BoolToggle disableMove;

				public BoolToggle disableAction;

				public BoolToggle disableAnimation;

				private ShootingCursor SC;

				private InputAction _moveAction;
	}
}
