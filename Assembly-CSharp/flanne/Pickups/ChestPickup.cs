using System.Collections;
using UnityEngine;

namespace flanne.Pickups
{
    public class ChestPickup : MonoBehaviour
    {
        private void Start()
        {
            this._xpFountainCoroutine = null;
            this.OP = ObjectPooler.SharedInstance;
        }

        private void OnEnable()
        {
            this.spriteRenderer.sprite = this.chestClosed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && this._xpFountainCoroutine == null)
            {
                this.PostNotification(ChestPickup.ChestPickupEvent, null);
                this._xpFountainCoroutine = this.XPFountainCR();
                base.StartCoroutine(this._xpFountainCoroutine);
            }
        }

        private IEnumerator XPFountainCR()
        {
            yield return new WaitForSeconds(0.1f);
            this.spriteRenderer.sprite = this.chestOpen;
            int num;
            for (int i = 0; i < this.amountOfXP; i = num + 1)
            {
                GameObject pooledObject = this.OP.GetPooledObject(this.xpOPTag);
                pooledObject.transform.position = base.transform.position;
                pooledObject.SetActive(true);
                Vector3 to = new Vector3(pooledObject.transform.position.x + Random.Range(-1f, 1f), pooledObject.transform.position.y + Random.Range(-1f, 1f), 0f);
                LeanTween.move(pooledObject, to, 0.5f);
                SoundEffectSO soundEffectSO = this.xpSpawnSFX;
                if (soundEffectSO != null)
                {
                    soundEffectSO.Play(null);
                }
                yield return new WaitForSeconds(0.1f);
                num = i;
            }
            this._xpFountainCoroutine = null;
            base.gameObject.SetActive(false);
            yield break;
        }

        public static string ChestPickupEvent = "ChestPickup.ChestPickupEvent";

        [SerializeField]
        private int amountOfXP;

        [SerializeField]
        private string xpOPTag;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Sprite chestOpen;

        [SerializeField]
        private Sprite chestClosed;

        [SerializeField]
        private SoundEffectSO xpSpawnSFX;

        private IEnumerator _xpFountainCoroutine;

        private ObjectPooler OP;
    }
}
