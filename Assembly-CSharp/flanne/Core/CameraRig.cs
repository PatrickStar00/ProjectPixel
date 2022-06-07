using System;
using UnityEngine;

namespace flanne.Core
{
    public class CameraRig : MonoBehaviour
    {
        private void Awake()
        {
            this.parent = base.transform.parent;
        }

        private void Start()
        {
            this.SC = ShootingCursor.Instance;
        }

        private void Update()
        {
            if (!PauseController.isPaused)
            {
                Vector2 vector = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition);
                Vector2 vector2 = this.parent.position;
                Vector2 vector3 = vector - vector2;
                vector3 /= 12f;
                if (this.maxLookDistance < vector3.magnitude)
                {
                    vector3 = vector3.normalized;
                    vector3 = this.maxLookDistance * vector3;
                }
                base.transform.localPosition = vector3;
            }
        }

        [SerializeField]
        private float maxLookDistance;

        private Transform parent;

        private ShootingCursor SC;
    }
}
