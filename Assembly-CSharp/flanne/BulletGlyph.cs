using System;
using System.Collections;
using UnityEngine;

namespace flanne
{
    public class BulletGlyph : Spawn
    {
        private float damageMultiplier
        {
            get
            {
                return this.player.stats[StatType.SummonDamage].Modify(this.baseDamageMultiplier);
            }
        }

        private void Start()
        {
            this.PF = ProjectileFactory.SharedInstance;
        }

        private void OnEnable()
        {
            base.StartCoroutine(this.LifetimeCR());
        }

        private void ShootBullets()
        {
            Vector2 zero = Vector2.zero;
            while (zero == Vector2.zero)
            {
                zero..ctor(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            for (int i = 0; i < this.numOfBullets; i++)
            {
                float degrees = (float)i / (float)this.numOfBullets * 360f;
                Vector2 direction = zero.Rotate(degrees);
                ProjectileRecipe projectileRecipe = this.player.gun.GetProjectileRecipe();
                projectileRecipe.size *= 0.5f;
                projectileRecipe.knockback *= 0.1f;
                this.PF.SpawnProjectile(projectileRecipe, direction, base.transform.position, this.damageMultiplier, true);
            }
        }

        private IEnumerator LifetimeCR()
        {
            yield return new WaitForSeconds(this.timeToActivate - 0.1f);
            this.knockbackObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            this.ShootBullets();
            this.knockbackObject.SetActive(false);
            SoundEffectSO soundEffectSO = this.soundFX;
            if (soundEffectSO != null)
            {
                soundEffectSO.Play(null);
            }
            base.gameObject.SetActive(false);
            yield break;
        }

        [SerializeField]
        private float timeToActivate;

        [SerializeField]
        private int numOfBullets;

        [SerializeField]
        private float baseDamageMultiplier;

        [SerializeField]
        private GameObject knockbackObject;

        [SerializeField]
        private SoundEffectSO soundFX;

        private ProjectileFactory PF;
    }
}
