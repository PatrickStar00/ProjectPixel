using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace flanne
{
		public class GamepadCursor : MonoBehaviour
	{
				private void UpdateMotion()
		{
			if (this._virtualMouse == null || Gamepad.current == null)
			{
				return;
			}
			Vector2 vector = Gamepad.current.leftStick.ReadValue();
			vector *= this.cursorSpeed * Time.deltaTime;
			Vector2 vector2 = this._virtualMouse.position.ReadValue() + vector;
			vector2.x = Mathf.Clamp(vector2.x, 0f, (float)Screen.width);
			vector2.y = Mathf.Clamp(vector2.y, 0f, (float)Screen.height);
			InputState.Change<Vector2>(this._virtualMouse.position, vector2, 0, default(InputEventPtr));
			InputState.Change<Vector2>(this._virtualMouse.delta, vector, 0, default(InputEventPtr));
			bool flag = InputControlExtensions.IsPressed(Gamepad.current.aButton, 0f);
			if (this._previousMouseState != Gamepad.current.aButton.isPressed)
			{
				MouseState mouseState;
				InputControlExtensions.CopyState<MouseState>(this._virtualMouse, ref mouseState);
				mouseState.WithButton(0, flag);
				InputState.Change<MouseState>(this._virtualMouse, mouseState, 0, default(InputEventPtr));
				this._previousMouseState = flag;
			}
			this.AnchorCursor(vector2);
		}

				private void OnEnable()
		{
			if (this._currentMouse == null)
			{
				this._currentMouse = Mouse.current;
			}
			this.InitCursor();
			if (this._virtualMouse == null)
			{
				this._virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse", null, null);
			}
			else if (!this._virtualMouse.added)
			{
				InputSystem.AddDevice(this._virtualMouse);
			}
			InputUser.PerformPairingWithDevice(this._virtualMouse, this.playerInput.user, 0);
			Vector2 anchoredPosition;
			anchoredPosition..ctor((float)(Screen.width / 2), (float)(Screen.height / 2));
			this.cursorTransform.anchoredPosition = anchoredPosition;
			Vector2 anchoredPosition2 = this.cursorTransform.anchoredPosition;
			InputState.Change<Vector2>(this._virtualMouse.position, anchoredPosition2, 0, default(InputEventPtr));
			InputSystem.onAfterUpdate += this.UpdateMotion;
		}

				private void OnDisable()
		{
			Cursor.visible = true;
			if (this.playerInput != null)
			{
				InputUser user = this.playerInput.user;
				this.playerInput.user.UnpairDevice(this._virtualMouse);
			}
			if (this._virtualMouse != null && this._virtualMouse.added)
			{
				InputSystem.RemoveDevice(this._virtualMouse);
			}
			InputSystem.onAfterUpdate -= this.UpdateMotion;
		}

				private void Update()
		{
			if (this.previousControlScheme != this.playerInput.currentControlScheme)
			{
				this.OnControlsChanged(this.playerInput);
			}
		}

				private void AnchorCursor(Vector2 position)
		{
			Vector2 anchoredPosition;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasTransform, position, this.mainCamera, ref anchoredPosition);
			this.cursorTransform.anchoredPosition = anchoredPosition;
		}

				private void InitCursor()
		{
			if (this.playerInput.currentControlScheme == "Keyboard&Mouse")
			{
				this.cursorTransform.gameObject.SetActive(false);
				Cursor.visible = true;
			}
			else if (this.playerInput.currentControlScheme == "Gamepad")
			{
				this.cursorTransform.gameObject.SetActive(true);
				Cursor.visible = false;
			}
			this.previousControlScheme = this.playerInput.currentControlScheme;
		}

				private void OnControlsChanged(PlayerInput input)
		{
			if (input.currentControlScheme == "Keyboard&Mouse" && this.previousControlScheme != input.currentControlScheme)
			{
				this.cursorTransform.gameObject.SetActive(false);
				Cursor.visible = true;
				if (this._currentMouse == null)
				{
					this._currentMouse = Mouse.current;
				}
				this._currentMouse.WarpCursorPosition(this._virtualMouse.position.ReadValue());
				this.previousControlScheme = "Keyboard&Mouse";
				return;
			}
			if (input.currentControlScheme == "Gamepad" && this.previousControlScheme != input.currentControlScheme)
			{
				this.cursorTransform.gameObject.SetActive(true);
				Cursor.visible = false;
				InputState.Change<Vector2>(this._virtualMouse.position, this._currentMouse.position.ReadValue(), 0, default(InputEventPtr));
				this.AnchorCursor(this._currentMouse.position.ReadValue());
				this.previousControlScheme = "Gamepad";
			}
		}

				[SerializeField]
		private PlayerInput playerInput;

				[SerializeField]
		private RectTransform cursorTransform;

				[SerializeField]
		private RectTransform canvasTransform;

				[SerializeField]
		private Camera mainCamera;

				[SerializeField]
		private float cursorSpeed = 1000f;

				private const string gamepadScheme = "Gamepad";

				private const string mouseScheme = "Keyboard&Mouse";

				private string previousControlScheme = "";

				private bool _previousMouseState;

				private Mouse _virtualMouse;

				private Mouse _currentMouse;
	}
}
