using System;
using System.Collections;
using UnityEngine;

namespace flanne.Pickups
{
    public class Pickup : MonoBehaviour
    {
        protected virtual void UsePickup(GameObject pickupper)
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Pickupper" && this.pickUpCoroutine == null)
            {
                this.pickUpCoroutine = this.PickupCR(other.gameObject);
                base.StartCoroutine(this.pickUpCoroutine);
            }
        }

        private IEnumerator PickupCR(GameObject pickupper)
        {
            base.transform.SetParent(pickupper.transform);
            int tweenID = LeanTween.moveLocal(base.gameObject, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).id;
            while (LeanTween.isTweening(tweenID))
            {
                yield return null;
            }
            this.UsePickup(pickupper);
            if (this.soundFX != null)
            {
                this.soundFX.Play(null);
            }
            this.pickUpCoroutine = null;
            base.transform.SetParent(ObjectPooler.SharedInstance.transform);
            base.transform.localPosition = Vector3.zero;
            base.gameObject.SetActive(false);
            yield break;
        }

        [SerializeField]
        private SoundEffectSO soundFX;

        private IEnumerator pickUpCoroutine;
    }
}
