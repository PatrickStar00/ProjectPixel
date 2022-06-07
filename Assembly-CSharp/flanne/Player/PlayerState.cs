using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.Player
{
    public abstract class PlayerState : State
    {
        protected PlayerInput playerInput
        {
            get
            {
                return this.owner.playerInput;
            }
        }

        protected SpriteRenderer playerSprite
        {
            get
            {
                return this.owner.playerSprite;
            }
        }

        protected Animator playerAnimator
        {
            get
            {
                return this.owner.playerAnimator;
            }
        }

        protected StatsHolder stats
        {
            get
            {
                return this.owner.stats;
            }
        }

        protected Gun gun
        {
            get
            {
                return this.owner.gun;
            }
        }

        protected Ammo ammo
        {
            get
            {
                return this.owner.ammo;
            }
        }

        protected Slider reloadBar
        {
            get
            {
                return this.owner.reloadBar;
            }
        }

        private void Awake()
        {
            this.owner = base.GetComponentInParent<PlayerController>();
        }

        protected PlayerController owner;
    }
}
