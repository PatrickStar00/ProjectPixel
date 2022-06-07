using System;
using UnityEngine;

namespace flanne.Pickups
{
    public class DevilDealPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                this.PostNotification(DevilDealPickup.DevilDealPickupEvent, null);
                base.gameObject.SetActive(false);
            }
        }

        public static string DevilDealPickupEvent = "DevilDealPickup.DevilDealPickupEvent";
    }
}
