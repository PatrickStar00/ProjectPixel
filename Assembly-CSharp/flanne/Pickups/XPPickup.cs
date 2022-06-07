using System;
using flanne.Player;
using UnityEngine;

namespace flanne.Pickups
{
    public class XPPickup : Pickup
    {
        protected override void UsePickup(GameObject pickupper)
        {
            PlayerXP componentInChildren = pickupper.transform.root.GetComponentInChildren<PlayerXP>();
            if (componentInChildren != null)
            {
                componentInChildren.GainXP(this.amount);
            }
            this.PostNotification(XPPickup.XPPickupEvent, null);
        }

        public static string XPPickupEvent = "XPPickup.XPPickupEvent";

        public int amount;
    }
}
