using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace flanne
{
		public class InputDetector : MonoBehaviour
	{
								public bool usingGamepad { get; private set; }

				private void OnKBMUsed(InputAction.CallbackContext context)
		{
			UnityEvent unityEvent = this.onControllerActive;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.usingGamepad = false;
			MonoBehaviour.print(this.usingGamepad);
		}

				private void OnGamepadUsed(InputAction.CallbackContext context)
		{
			UnityEvent unityEvent = this.onControllerInactive;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.usingGamepad = true;
			MonoBehaviour.print(this.usingGamepad);
		}

				private void Awake()
		{
			if (InputDetector.Instance == null)
			{
				InputDetector.Instance = this;
			}
			else if (InputDetector.Instance != this)
			{
				Object.Destroy(base.gameObject);
			}
			Object.DontDestroyOnLoad(base.gameObject);
		}

				private void Start()
		{
			this._kbmAction = this.inputs.FindActionMap("InputDetector", false).FindAction("KBMUsed", false);
			this._gamepadAction = this.inputs.FindActionMap("InputDetector", false).FindAction("GamepadUsed", false);
			this._kbmAction.started += this.OnKBMUsed;
			this._gamepadAction.started += this.OnGamepadUsed;
		}

				private void OnDestory()
		{
			this._kbmAction.started -= this.OnKBMUsed;
			this._gamepadAction.started -= this.OnGamepadUsed;
		}

				public static InputDetector Instance;

				[SerializeField]
		private InputActionAsset inputs;

				public UnityEvent onControllerActive;

				public UnityEvent onControllerInactive;

				private InputAction _kbmAction;

				private InputAction _gamepadAction;
	}
}
