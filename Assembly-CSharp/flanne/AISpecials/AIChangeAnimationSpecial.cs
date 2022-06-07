using System;
using UnityEngine;

namespace flanne.AISpecials
{
    [CreateAssetMenu(fileName = "AIChangeAnimationSpecial", menuName = "AISpecials/AIChangeAnimationSpecial")]
    public class AIChangeAnimationSpecial : AISpecial
    {
        public override void Use(AIComponent ai, Transform target)
        {
            Animator animator = ai.animator;
            if (animator == null)
            {
                return;
            }
            animator.SetTrigger("Special");
        }
    }
}
