using System;
using UnityEngine;

namespace flanne.Pickups
{
    public class HPPickup : Pickup
    {
        protected override void UsePickup(GameObject pickupper)
        {
            Health componentInChildren = pickupper.transform.root.GetComponentInChildren<Health>();
            if (componentInChildren != null)
            {
                componentInChildren.HPChange(this.amount);
            }
        }

        public int amount;
    }
}
