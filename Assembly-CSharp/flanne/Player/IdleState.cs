using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace flanne.Player
{
    public class IdleState : PlayerState
    {
        private void OnFireAction(InputAction.CallbackContext obj)
        {
            if (base.ammo.outOfAmmo)
            {
                this.owner.ChangeState<ReloadState>();
                return;
            }
            this.owner.ChangeState<ShootingState>();
        }

        private void OnReloadAction(InputAction.CallbackContext obj)
        {
            if (!base.ammo.fullOnAmmo)
            {
                this.owner.ChangeState<ReloadState>();
            }
        }

        private void OnAmmoChange(int amountChanged)
        {
            if (base.ammo.outOfAmmo)
            {
                this.owner.ChangeState<ReloadState>();
            }
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
            base.playerInput.actions["Fire"].performed += this.OnFireAction;
            base.playerInput.actions["Reload"].started += this.OnReloadAction;
            base.ammo.OnAmmoChanged.AddListener(new UnityAction<int>(this.OnAmmoChange));
            this.owner.disableAction.ToggleEvent += this.OnDisableToggleChange;
            this.owner.moveSpeedMultiplier = 1f;
            this.owner.faceMouse = false;
        }

        public override void Exit()
        {
            base.playerInput.actions["Fire"].performed -= this.OnFireAction;
            base.playerInput.actions["Reload"].started -= this.OnReloadAction;
            base.ammo.OnAmmoChanged.RemoveListener(new UnityAction<int>(this.OnAmmoChange));
            this.owner.disableAction.ToggleEvent -= this.OnDisableToggleChange;
        }
    }
}
