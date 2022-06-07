using System;
using UnityEngine;

namespace flanne
{
    public class AIComponent : MonoBehaviour
    {
        private void Start()
        {
            AIController.SharedInstance.Register(this);
        }

        private void OnEnable()
        {
            this.specialTimer = 0f;
        }

        public MoveComponent2D moveComponent;

        public float maxMoveSpeed;

        public float acceleration;

        public bool frozen;

        public bool ignoreFlock;

        public bool rotateTowardsPlayer;

        public bool dontLookAtPlayer;

        public Animator animator;

        public AISpecial specialSO;

        public float specialRangeSqr = -1f;

        public float specialCooldown;

        public float specialTimer;

        public Transform specialPoint;

        public bool dontFaceDuringSpecial;
    }
}
