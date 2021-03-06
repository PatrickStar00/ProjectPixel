using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace flanne.Player
{
    public class ManualReloadState : PlayerState
    {
        private void OnFireAction(InputAction.CallbackContext obj)
        {
            if (!base.ammo.outOfAmmo)
            {
                this.owner.ChangeState<ShootingState>();
                base.StopCoroutine(this.reloadCoroutine);
                base.reloadBar.transform.parent.gameObject.SetActive(false);
            }
        }

        private void OnDisableToggleChange(object sender, bool isDisabled)
        {
            if (isDisabled)
            {
                this.owner.ChangeState<DisabledState>();
                base.StopCoroutine(this.reloadCoroutine);
                base.reloadBar.transform.parent.gameObject.SetActive(false);
            }
        }

        public override void Enter()
        {
            base.playerInput.actions["Fire"].started += this.OnFireAction;
            this.owner.disableAction.ToggleEvent += this.OnDisableToggleChange;
            this.owner.moveSpeedMultiplier = 1f;
            this.owner.faceMouse = false;
            this.reloadCoroutine = this.ReloadCR();
            base.StartCoroutine(this.reloadCoroutine);
            base.gun.SetAnimationTrigger("Reload");
            this.owner.reloadStartSFX.Play(null);
        }

        public override void Exit()
        {
            base.playerInput.actions["Fire"].started -= this.OnFireAction;
            this.owner.disableAction.ToggleEvent -= this.OnDisableToggleChange;
            base.gun.SetAnimationTrigger("Still");
            base.reloadBar.transform.parent.gameObject.SetActive(false);
        }

        private IEnumerator ReloadCR()
        {
            base.reloadBar.transform.parent.gameObject.SetActive(true);
            float timer = 0f;
            float reloadDuration = base.gun.reloadDuration;
            while (timer < reloadDuration)
            {
                base.reloadBar.value = timer / reloadDuration;
                yield return null;
                timer += Time.deltaTime;
            }
            base.ammo.Reload();
            this.owner.reloadEndSFX.Play(null);
            if (base.playerInput.actions["Fire"].ReadValue<float>() == 0f)
            {
                this.owner.ChangeState<IdleState>();
            }
            else
            {
                this.owner.ChangeState<ShootingState>();
            }
            yield break;
        }

        private IEnumerator reloadCoroutine;
    }
}
