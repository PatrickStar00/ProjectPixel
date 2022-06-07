using System;
using UnityEngine;

namespace flanne
{
    public class BurnOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag.Contains(this.hitTag))
            {
                BurnSystem.SharedInstance.Burn(other.gameObject, this.burnDamage);
            }
        }

        [SerializeField]
        private string hitTag;

        [SerializeField]
        private int burnDamage;
    }
}
