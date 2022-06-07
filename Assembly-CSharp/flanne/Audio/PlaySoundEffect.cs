using System;
using UnityEngine;

namespace flanne.Audio
{
    public class PlaySoundEffect : MonoBehaviour
    {
        public void Play()
        {
            SoundEffectSO soundEffectSO = this.soundFX;
            if (soundEffectSO == null)
            {
                return;
            }
            soundEffectSO.Play(null);
        }

        [SerializeField]
        private SoundEffectSO soundFX;
    }
}
