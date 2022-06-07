using System;
using System.Collections;
using UnityEngine;

namespace flanne.AISpecials
{
    [CreateAssetMenu(fileName = "AILaserSpecial", menuName = "AISpecials/AILaserSpecial")]
    public class AILasersSpecial : AISpecial
    {
        public override void Use(AIComponent ai, Transform target)
        {
            GameObject windupObj = null;
            GameObject laserObj = null;
            for (int i = 0; i < ai.transform.childCount; i++)
            {
                Transform child = ai.transform.GetChild(i);
                if (child.tag == this.windupTag)
                {
                    windupObj = child.gameObject;
                }
                if (child.tag == this.laserTag)
                {
                    laserObj = child.gameObject;
                }
            }
            ai.StartCoroutine(this.LaserCR(windupObj, laserObj, ai));
        }

        private IEnumerator LaserCR(GameObject windupObj, GameObject laserObj, AIComponent ai)
        {
            windupObj.SetActive(true);
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
            laserObj.SetActive(true);
            Animator animator2 = ai.animator;
            if (animator2 != null)
            {
                animator2.SetTrigger("Special");
            }
            SoundEffectSO soundEffectSO2 = this.laserSFX;
            if (soundEffectSO2 != null)
            {
                soundEffectSO2.Play(null);
            }
            yield break;
        }

        [SerializeField]
        private float windupTime;

        [SerializeField]
        private string windupTag;

        [SerializeField]
        private string laserTag;

        [SerializeField]
        private SoundEffectSO laserSFX;

        [SerializeField]
        private SoundEffectSO windupSFX;
    }
}
