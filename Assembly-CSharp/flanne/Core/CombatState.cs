using System;
using flanne.Pickups;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace flanne.Core
{
    public class CombatState : GameState
    {
        private void OnLevelUP(int level)
        {
            base.levelUpSFX.Play(null);
            this.owner.ChangeState<PowerupMenuState>();
        }

        private void OnDeath()
        {
            this.owner.ChangeState<PlayerDeadState>();
        }

        private void OnChestPickup(object sender, object args)
        {
            this.owner.ChangeState<ChestState>();
        }

        private void OnDevilDealPickup(object sender, object args)
        {
            this.owner.ChangeState<DevilDealState>();
        }

        private void OnTimerReached(object sender, object args)
        {
            this.owner.ChangeState<PlayerSurvivedState>();
        }

        private void OnPauseAction(InputAction.CallbackContext obj)
        {
            this.owner.ChangeState<PauseState>();
        }

        public override void Enter()
        {
            base.playerXP.OnLevelChanged.AddListener(new UnityAction<int>(this.OnLevelUP));
            base.playerHealth.onDeath.AddListener(new UnityAction(this.OnDeath));
            this.AddObserver(new Action<object, object>(this.OnChestPickup), ChestPickup.ChestPickupEvent);
            this.AddObserver(new Action<object, object>(this.OnDevilDealPickup), DevilDealPickup.DevilDealPickupEvent);
            this.AddObserver(new Action<object, object>(this.OnTimerReached), GameTimer.TimeLimitNotification);
            base.playerInput.actions["Pause"].started += this.OnPauseAction;
            base.mouseAmmoUI.Show();
            base.shootingCursor.EnableGamepadCusor();
            EventSystem.current.SetSelectedGameObject(null);
        }

        public override void Exit()
        {
            base.playerXP.OnLevelChanged.RemoveListener(new UnityAction<int>(this.OnLevelUP));
            base.playerHealth.onDeath.RemoveListener(new UnityAction(this.OnDeath));
            this.RemoveObserver(new Action<object, object>(this.OnChestPickup), ChestPickup.ChestPickupEvent);
            this.RemoveObserver(new Action<object, object>(this.OnDevilDealPickup), DevilDealPickup.DevilDealPickupEvent);
            this.RemoveObserver(new Action<object, object>(this.OnTimerReached), GameTimer.TimeLimitNotification);
            base.playerInput.actions["Pause"].started -= this.OnPauseAction;
            base.mouseAmmoUI.Hide();
            base.shootingCursor.DisableGamepadCursor();
        }
    }
}
