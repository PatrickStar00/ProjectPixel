using System;
using System.Collections;
using System.Collections.Generic;
using flanne.UI;
using UnityEngine;

namespace flanne.Core
{
    public class ChestState : GameState
    {
        private void OnTakeClick(object sender, EventArgs e)
        {
            this._currPowerup.ApplyAndNotify(base.player);
            if (!this._currPowerup.isRepeatable)
            {
                base.powerupGenerator.RemoveFromPool(this._currPowerup);
            }
            base.StartCoroutine(this.WaitToLeaveMenu());
        }

        private void OnLeaveClick(object sender, EventArgs e)
        {
            base.StartCoroutine(this.WaitToLeaveMenu());
        }

        public override void Enter()
        {
            List<Powerup> random = base.powerupGenerator.GetRandom(1);
            this._currPowerup = random[0];
            base.chestUIController.SetToPowerup(this._currPowerup);
            base.chestUIController.Show();
            ChestUIController chestUIController = base.chestUIController;
            chestUIController.TakeClickEvent = (EventHandler)Delegate.Combine(chestUIController.TakeClickEvent, new EventHandler(this.OnTakeClick));
            ChestUIController chestUIController2 = base.chestUIController;
            chestUIController2.LeaveClickEvent = (EventHandler)Delegate.Combine(chestUIController2.LeaveClickEvent, new EventHandler(this.OnLeaveClick));
            base.pauseController.Pause();
            AudioManager.Instance.SetLowPassFilter(true);
        }

        public override void Exit()
        {
            base.pauseController.UnPause();
            AudioManager.Instance.SetLowPassFilter(false);
        }

        private IEnumerator WaitToLeaveMenu()
        {
            ChestUIController chestUIController = base.chestUIController;
            chestUIController.TakeClickEvent = (EventHandler)Delegate.Remove(chestUIController.TakeClickEvent, new EventHandler(this.OnTakeClick));
            ChestUIController chestUIController2 = base.chestUIController;
            chestUIController2.LeaveClickEvent = (EventHandler)Delegate.Remove(chestUIController2.LeaveClickEvent, new EventHandler(this.OnLeaveClick));
            base.chestUIController.Hide();
            yield return new WaitForSecondsRealtime(0.5f);
            if (!base.playerHealth.isDead)
            {
                this.owner.ChangeState<CombatState>();
            }
            else
            {
                this.owner.ChangeState<PlayerDeadState>();
            }
            yield break;
        }

        private Powerup _currPowerup;
    }
}
