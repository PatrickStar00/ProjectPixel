using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace flanne.Core
{
    public class DevilDealState : GameState
    {
        private void OnConfirm(object sender, Powerup e)
        {
            e.ApplyAndNotify(base.player);
            if (!e.isRepeatable)
            {
                base.powerupGenerator.RemoveFromDevilPool(e);
            }
            if (!base.playerHealth.isDead)
            {
                this.owner.ChangeState<CombatState>();
                return;
            }
            this.owner.ChangeState<PlayerDeadState>();
        }

        private void OnLeave()
        {
            this.owner.ChangeState<CombatState>();
        }

        public override void Enter()
        {
            base.pauseController.Pause();
            this.GeneratePowerups();
            base.devilDealMenuPanel.Show();
            base.devilDealMenu.ConfirmEvent += this.OnConfirm;
            base.devilDealLeaveButton.onClick.AddListener(new UnityAction(this.OnLeave));
            AudioManager.Instance.SetLowPassFilter(true);
        }

        public override void Exit()
        {
            base.pauseController.UnPause();
            base.devilDealMenuPanel.Hide();
            base.devilDealMenu.ConfirmEvent -= this.OnConfirm;
            base.devilDealLeaveButton.onClick.RemoveListener(new UnityAction(this.OnLeave));
            AudioManager.Instance.SetLowPassFilter(false);
        }

        private void GeneratePowerups()
        {
            this.powerupChoices = base.powerupGenerator.GetRandomDevilProfile(3);
            for (int i = 0; i < this.powerupChoices.Count; i++)
            {
                base.devilDealMenu.SetData(i, this.powerupChoices[i]);
            }
        }

        private List<Powerup> powerupChoices;
    }
}
