using System;
using UnityEngine;
using UnityEngine.Events;

namespace flanne
{
    public class ActivateGameobjectOnReload : MonoBehaviour
    {
        private void OnReload()
        {
            this.obj.SetActive(true);
        }

        private void Start()
        {
            PlayerController componentInParent = base.transform.GetComponentInParent<PlayerController>();
            this.ammo = componentInParent.ammo;
            this.ammo.OnReload.AddListener(new UnityAction(this.OnReload));
        }

        private void OnDestroy()
        {
            this.ammo.OnReload.RemoveListener(new UnityAction(this.OnReload));
        }

        [SerializeField]
        private GameObject obj;

        private Ammo ammo;
    }
}
