using System;
using UnityEngine;

namespace flanne
{
    public class BurnOnFreeze : MonoBehaviour
    {
        private void Start()
        {
            this.AddObserver(new Action<object, object>(this.OnFreeze), FreezeSystem.InflictFreezeEvent);
            this.BurnSys = BurnSystem.SharedInstance;
        }

        private void OnDestroy()
        {
            this.RemoveObserver(new Action<object, object>(this.OnFreeze), FreezeSystem.InflictFreezeEvent);
        }

        private void OnFreeze(object sender, object args)
        {
            GameObject gameObject = args as GameObject;
            if (gameObject.tag.Contains("Enemy"))
            {
                this.BurnSys.Burn(gameObject, this.burnDamage);
            }
        }

        [SerializeField]
        private int burnDamage;

        private BurnSystem BurnSys;
    }
}
