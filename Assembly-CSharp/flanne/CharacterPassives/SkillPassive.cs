using System;
using flanne.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace flanne.CharacterPassives
{
		public abstract class SkillPassive : MonoBehaviour
	{
				private void PerformSkillCallback(InputAction.CallbackContext context)
		{
			if (this._timer <= 0f && !PauseController.isPaused)
			{
				this._timer += this.cooldown;
				this.PerformSkill();
				SoundEffectSO soundEffectSO = this.soundFX;
				if (soundEffectSO == null)
				{
					return;
				}
				soundEffectSO.Play(null);
			}
		}

				private void Start()
		{
			this._skillAction = this.inputs.FindActionMap("PlayerMap", false).FindAction("Skill", false);
			this._skillAction.performed += this.PerformSkillCallback;
			this.Init();
		}

				private void OnDestroy()
		{
			this._skillAction.performed -= this.PerformSkillCallback;
		}

				private void Update()
		{
			if (this._timer > 0f)
			{
				this._timer -= Time.deltaTime;
			}
		}

				protected virtual void Init()
		{
		}

				protected abstract void PerformSkill();

				[SerializeField]
		private InputActionAsset inputs;

				[SerializeField]
		private float cooldown;

				[SerializeField]
		private SoundEffectSO soundFX;

				private InputAction _skillAction;

				private float _timer;
	}
}
