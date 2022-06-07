using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.Player
{
    public class PlayerCollider : MonoBehaviour
    {
        private void OnDeath()
        {
            this.playerController.disableMove.Flip();
            this.playerController.disableAction.Flip();
            this.deathImpactSprite.SetActive(true);
            this.spriteRenderer.material = this.deathMaterial;
            this.deathParticles.Play();
            LeanTween.scale(base.gameObject, new Vector3(1f, 0f, 1f), 2f);
            this._deathAnimPlayed = true;
            ScreenFlash screenFlash = this.damageScreenFlash;
            if (screenFlash == null)
            {
                return;
            }
            screenFlash.Flash(2);
        }

        private void Start()
        {
            this.health.onDeath.AddListener(new UnityAction(this.OnDeath));
        }

        private void OnDestroy()
        {
            this.health.onDeath.RemoveListener(new UnityAction(this.OnDeath));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (this._deathAnimPlayed)
            {
                return;
            }
            if (this.health.isInvincible && other.gameObject.tag.Contains("HarmfulToPlayer"))
            {
                Vector2 vector = other.contacts[0].normal * 0.4f;
                this.playerController.transform.position += new Vector3(vector.x, vector.y, 0f);
            }
            if (other.gameObject.tag.Contains("Enemy") || other.gameObject.tag.Contains("HarmfulToPlayer"))
            {
                this.health.HPChange(-1);
                if (!this.health.isInvincible && !this.health.isDead)
                {
                    //base.transform.position;
                    Vector2 normal = other.contacts[0].normal;
                    Vector3 direction = new Vector3(normal.x, normal.y, 0f);
                    base.StartCoroutine(this.KnockbackCR(direction, 0.2f));
                    base.StartCoroutine(this.InvicibilityCR(1f));
                    ScreenFlash screenFlash = this.damageScreenFlash;
                    if (screenFlash == null)
                    {
                        return;
                    }
                    screenFlash.Flash(1);
                }
            }
        }

        private IEnumerator KnockbackCR(Vector3 direction, float duration)
        {
            this.playerController.disableMove.Flip();
            float timer = 0f;
            LeanTween.moveLocalY(base.gameObject, 0.35f, duration / 2f).setLoopPingPong(1);
            while (timer < duration)
            {
                this.playerController.transform.position += direction * 7f * Time.deltaTime;
                yield return null;
                timer += Time.deltaTime;
            }
            this.playerController.disableMove.UnFlip();
            yield break;
        }

        private IEnumerator InvicibilityCR(float duration)
        {
            float timer = 0f;
            this.health.isInvincible = true;
            while (timer < duration)
            {
                this.flasher.Flash();
                yield return new WaitForSeconds(0.2f);
                timer += 0.2f;
            }
            this.health.isInvincible = false;
            yield break;
        }

        [SerializeField]
        private PlayerController playerController;

        [SerializeField]
        private Health health;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private GameObject deathImpactSprite;

        [SerializeField]
        private FlashSprite flasher;

        [SerializeField]
        private Material deathMaterial;

        [SerializeField]
        private ParticleSystem deathParticles;

        [SerializeField]
        private ScreenFlash damageScreenFlash;

        private bool _deathAnimPlayed;
    }
}
