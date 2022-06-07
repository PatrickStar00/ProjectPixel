using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.UI
{
		public class RuneUnlocker : TieredUnlockable, ISelectHandler, IEventSystemHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
	{
								public override int level
		{
			get
			{
				return this.rune.data.level;
			}
			set
			{
				this.rune.data.level = value;
				this.CheckRuneLevelReq();
				this.rune.Refresh();
				UnityEvent unityEvent = this.onLevel;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

								public bool toggleOn
		{
			get
			{
				return this.toggle.isOn;
			}
			set
			{
				this.toggle.isOn = value;
			}
		}

								public bool locked
		{
			get
			{
				return !this.button.interactable;
			}
			set
			{
				this.button.interactable = !value;
			}
		}

						public int costPerLevel
		{
			get
			{
				return this.rune.data.costPerLevel;
			}
		}

				public void OnPointerEnter(PointerEventData eventData)
		{
			this._isPointerOn = true;
		}

				public void OnPointerExit(PointerEventData eventData)
		{
			this._isPointerOn = false;
		}

				public void OnSelect(BaseEventData eventData)
		{
			this._isSelected = true;
		}

				public void OnDeselect(BaseEventData data)
		{
			this._isSelected = false;
			this.ReleasePress();
		}

				public void OnSubmitChanged(InputAction.CallbackContext context)
		{
			if (context.ReadValue<float>() == 0f)
			{
				this.ReleasePress();
				return;
			}
			this.StartPress();
		}

				public void OnClickChanged(InputAction.CallbackContext context)
		{
			if (context.ReadValue<float>() == 0f)
			{
				this.ReleasePress();
				return;
			}
			if (this._isPointerOn)
			{
				this.StartPress();
			}
		}

				private void Start()
		{
			this.CheckRuneLevelReq();
			this._submitAction = this.inputs.FindActionMap("UI", false).FindAction("Submit", false);
			this._clickAction = this.inputs.FindActionMap("UI", false).FindAction("Click", false);
			this._submitAction.performed += this.OnSubmitChanged;
			this._clickAction.performed += this.OnClickChanged;
		}

				private void OnDestroy()
		{
			this._submitAction.performed -= this.OnSubmitChanged;
			this._clickAction.performed -= this.OnClickChanged;
		}

				private void CheckRuneLevelReq()
		{
			if (this.rune.data.level == 0)
			{
				this.toggle.interactable = false;
				return;
			}
			this.toggle.interactable = true;
		}

				private void LevelUp()
		{
			int level = this.level;
			this.level = level + 1;
			this.rune.Refresh();
			this.toggle.isOn = true;
			this.levelupSlider.value = 0f;
			this.levelupParticles.Play();
			PointsTracker.pts -= this.costPerLevel;
			UnityEvent unityEvent = this.onLevel;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

				private void StartPress()
		{
			this.ReleasePress();
			if (this.locked)
			{
				return;
			}
			if (this._isSelected && this.toggle.interactable)
			{
				this.toggle.isOn = true;
			}
			if (this._isSelected && this.level < this.maxLevel && this.costPerLevel < PointsTracker.pts)
			{
				this._pressAndHoldCoroutine = this.PressAndHoldCR();
				base.StartCoroutine(this._pressAndHoldCoroutine);
			}
		}

				private void ReleasePress()
		{
			if (this._pressAndHoldCoroutine != null)
			{
				base.StopCoroutine(this._pressAndHoldCoroutine);
				this._pressAndHoldCoroutine = null;
			}
			this.levelupSlider.value = 0f;
		}

				private IEnumerator PressAndHoldCR()
		{
			float timer = 0f;
			while (timer < this.holdToUnlockTime)
			{
				yield return null;
				timer += Time.deltaTime;
				this.levelupSlider.value = timer / this.holdToUnlockTime;
			}
			this.LevelUp();
			this._pressAndHoldCoroutine = null;
			yield break;
		}

				public RuneIcon rune;

				[SerializeField]
		private InputActionAsset inputs;

				[SerializeField]
		private Toggle toggle;

				[SerializeField]
		private Button button;

				[SerializeField]
		private Slider levelupSlider;

				[SerializeField]
		private ParticleSystem levelupParticles;

				[SerializeField]
		private int maxLevel = 5;

				[SerializeField]
		private float holdToUnlockTime;

				public UnityEvent onLevel;

				private InputAction _submitAction;

				private InputAction _clickAction;

				private IEnumerator _pressAndHoldCoroutine;

				private bool _isSelected;

				private bool _isPointerOn;
	}
}
