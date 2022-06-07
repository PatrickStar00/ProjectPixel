using System;

namespace flanne.Player
{
    public class DisabledState : PlayerState
    {
        private void OnDisableToggleChange(object sender, bool isDisabled)
        {
            if (!isDisabled)
            {
                this.owner.ChangeState<IdleState>();
            }
        }

        public override void Enter()
        {
            this.owner.disableAction.ToggleEvent += this.OnDisableToggleChange;
            this.owner.moveSpeedMultiplier = 1f;
            this.owner.faceMouse = false;
        }

        public override void Exit()
        {
            this.owner.disableAction.ToggleEvent -= this.OnDisableToggleChange;
        }
    }
}
