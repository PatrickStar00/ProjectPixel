using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace flanne.Player
{
		public class ShootingState : PlayerState
	{
				private void OnAmmoChanged(int amount)
		{
			if (amount == 0)
			{
				base.StartCoroutine(this.WaitToChangeState<ReloadState>());
			}
		}

				private void OnReloadAction(InputAction.CallbackContext obj)
		{
			this.owner.ChangeState<ManualReloadState>();
		}

				private void OnReFire(InputAction.CallbackContext obj)
		{
			if (base.ammo.outOfAmmo)
			{
				return;
			}
			if (this._changeStateCoroutine != null)
			{
				base.StopCoroutine(this._changeStateCoroutine);
			}
			base.playerInput.actions["Fire"].canceled += this.OnCancelFire;
			base.playerInput.actions["Reload"].started += this.OnReloadAction;
			base.ammo.OnAmmoChanged.AddListener(new UnityAction<int>(this.OnAmmoChanged));
			base.playerInput.actions["Fire"].performed -= this.OnReFire;
		}

				private void OnCancelFire(InputAction.CallbackContext obj)
		{
			this._changeStateCoroutine = this.WaitToChangeState<IdleState>();
			base.StartCoroutine(this._changeStateCoroutine);
		}

				private void OnDisableToggleChange(object sender, bool isDisabled)
		{
			if (isDisabled)
			{
				this.owner.ChangeState<DisabledState>();
			}
		}

				public override void Enter()
		{
			base.playerInput.actions["Fire"].canceled += this.OnCancelFire;
			base.playerInput.actions["Reload"].started += this.OnReloadAction;
			base.ammo.OnAmmoChanged.AddListener(new UnityAction<int>(this.OnAmmoChanged));
			this.owner.disableAction.ToggleEvent += this.OnDisableToggleChange;
			base.gun.StartShooting();
			this.owner.moveSpeedMultiplier = Mathf.Min(1f, base.stats[StatType.WalkSpeed].Modify(0.35f));
			this.owner.faceMouse = true;
		}

				public override void Exit()
		{
			base.playerInput.actions["Fire"].canceled -= this.OnCancelFire;
			base.playerInput.actions["Reload"].started -= this.OnReloadAction;
			base.ammo.OnAmmoChanged.RemoveListener(new UnityAction<int>(this.OnAmmoChanged));
			this.owner.disableAction.ToggleEvent -= this.OnDisableToggleChange;
			base.gun.StopShooting();
		}

				private IEnumerator WaitToChangeState<T>() where T : PlayerState
		{
			base.playerInput.actions["Fire"].canceled -= this.OnCancelFire;
			base.playerInput.actions["Reload"].started -= this.OnReloadAction;
			base.ammo.OnAmmoChanged.RemoveListener(new UnityAction<int>(this.OnAmmoChanged));
			base.playerInput.actions["Fire"].performed += this.OnReFire;
			while (!base.gun.shotReady)
			{
				yield return null;
			}
			base.playerInput.actions["Fire"].performed -= this.OnReFire;
			this.owner.ChangeState<T>();
			this._changeStateCoroutine = null;
			yield break;
		}

				private IEnumerator _changeStateCoroutine;
	}
}
