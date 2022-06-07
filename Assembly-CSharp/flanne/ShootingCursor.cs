using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace flanne
{
		public class ShootingCursor : MonoBehaviour
	{
								public Vector2 cursorPosition { get; private set; }

				private void OnPoint(InputAction.CallbackContext context)
		{
			this.cursorPosition = context.ReadValue<Vector2>();
			this.AnchorCursor(this.cursorPosition);
		}

				private void OnAim(InputAction.CallbackContext context)
		{
			this._lastGamepadVector = context.ReadValue<Vector2>();
		}

				private void OnAimCancel(InputAction.CallbackContext context)
		{
			this._lastGamepadVector = Vector2.zero;
		}

				private void OnControlsChanged(PlayerInput input)
		{
			if (input.currentControlScheme == "Keyboard&Mouse" && this.previousControlScheme != input.currentControlScheme)
			{
				this._usingGamepad = false;
				if (this._disableGamepadCursor)
				{
					this.cursorTransform.gameObject.SetActive(true);
					return;
				}
			}
			else if (input.currentControlScheme == "Gamepad" && this.previousControlScheme != input.currentControlScheme)
			{
				this._usingGamepad = true;
			}
		}

				private void Awake()
		{
			if (ShootingCursor.Instance == null)
			{
				ShootingCursor.Instance = this;
				return;
			}
			if (ShootingCursor.Instance != this)
			{
				Object.Destroy(base.gameObject);
			}
		}

				private void OnEnable()
		{
			Cursor.visible = false;
			this._pointAction = this.inputs.FindActionMap("PlayerMap", false).FindAction("Point", false);
			this._aimAction = this.inputs.FindActionMap("PlayerMap", false).FindAction("Aim", false);
			this._pointAction.performed += this.OnPoint;
			this._aimAction.performed += this.OnAim;
			this._aimAction.canceled += this.OnAimCancel;
		}

				private void OnDisable()
		{
			this._pointAction.performed -= this.OnPoint;
			this._aimAction.performed -= this.OnAim;
			this._aimAction.canceled -= this.OnAimCancel;
		}

				private void Update()
		{
			if (this.previousControlScheme != this.playerInput.currentControlScheme)
			{
				this.OnControlsChanged(this.playerInput);
			}
			if (!this._usingGamepad || this._disableGamepadCursor)
			{
				return;
			}
			Vector2 vector;
			vector..ctor((float)(Screen.width / 2), (float)(Screen.height / 2));
			if (this._lastGamepadVector != Vector2.zero)
			{
				Vector2 vector2 = vector - this.cursorPosition;
				this.cursorPosition += this._lastGamepadVector.normalized * this.cursorSpeed * Time.deltaTime + vector2.normalized * (this.cursorSpeed / 8f) * Time.deltaTime;
				Vector2 vector3 = this.cursorPosition - vector;
				vector3 = vector3.normalized * Mathf.Clamp(vector3.magnitude, -1f * this.fixedDistance, this.fixedDistance);
				this.cursorPosition = vector + vector3;
				this.AnchorCursor(this.cursorPosition);
				return;
			}
			Vector2 vector4 = (vector - this.cursorPosition).normalized * (this.cursorSpeed / 20f) * Time.deltaTime;
			this.cursorPosition += vector4;
			this.AnchorCursor(this.cursorPosition);
		}

				public void EnableGamepadCusor()
		{
			this.cursorTransform.gameObject.SetActive(true);
			this._disableGamepadCursor = false;
		}

				public void DisableGamepadCursor()
		{
			if (this._usingGamepad)
			{
				this.cursorTransform.gameObject.SetActive(false);
			}
			this._disableGamepadCursor = true;
		}

				private void AnchorCursor(Vector2 position)
		{
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.transform as RectTransform, position, this.canvas.worldCamera, ref vector);
			this.cursorTransform.position = this.canvas.transform.TransformPoint(vector);
		}

				public static ShootingCursor Instance;

				[SerializeField]
		private PlayerInput playerInput;

				[SerializeField]
		private InputActionAsset inputs;

				[SerializeField]
		private RectTransform cursorTransform;

				[SerializeField]
		private Canvas canvas;

				[SerializeField]
		private float fixedDistance = 100f;

				[SerializeField]
		private float cursorSpeed = 1000f;

				private const string gamepadScheme = "Gamepad";

				private const string mouseScheme = "Keyboard&Mouse";

				private string previousControlScheme = "";

				private InputAction _pointAction;

				private InputAction _aimAction;

				private Vector2 _lastGamepadVector = Vector2.zero;

				private bool _usingGamepad;

				private bool _disableGamepadCursor;
	}
}
