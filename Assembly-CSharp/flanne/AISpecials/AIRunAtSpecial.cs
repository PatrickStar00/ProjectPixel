using System;
using System.Collections;
using UnityEngine;

namespace flanne.AISpecials
{
    [CreateAssetMenu(fileName = "AIRunAtSpecial", menuName = "AISpecials/AIRunAtSpecial")]
    public class AIRunAtSpecial : AISpecial
    {
        public override void Use(AIComponent ai, Transform target)
        {
            Vector2 direction = target.position - ai.transform.position;
            ai.StartCoroutine(this.RunAtCR(ai, direction));
        }

        private IEnumerator RunAtCR(AIComponent ai, Vector2 direction)
        {
            Animator animator = ai.animator;
            if (animator != null)
            {
                animator.SetTrigger("Windup");
            }
            SoundEffectSO soundEffectSO = this.windupSFX;
            if (soundEffectSO != null)
            {
                soundEffectSO.Play(null);
            }
            yield return new WaitForSeconds(this.windupTime);
            Animator animator2 = ai.animator;
            if (animator2 != null)
            {
                animator2.SetTrigger("Special");
            }
            ai.moveComponent.vector = direction.normalized * this.runSpeed;
            SoundEffectSO soundEffectSO2 = this.runningSFX;
            if (soundEffectSO2 != null)
            {
                soundEffectSO2.Play(null);
            }
            yield break;
        }

        [SerializeField]
        private float runSpeed;

        [SerializeField]
        private float windupTime;

        [SerializeField]
        private SoundEffectSO runningSFX;

        [SerializeField]
        private SoundEffectSO windupSFX;
    }
}
