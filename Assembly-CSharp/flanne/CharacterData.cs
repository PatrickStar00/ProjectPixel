using System;
using UnityEngine;

namespace flanne
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public string nameString
        {
            get
            {
                return LocalizationSystem.GetLocalizedValue(this.nameStringID.key);
            }
        }

        public string description
        {
            get
            {
                return LocalizationSystem.GetLocalizedValue(this.descriptionStringID.key);
            }
        }

        public LocalizedString nameStringID;

        public LocalizedString descriptionStringID;

        public RuntimeAnimatorController animController;

        public Sprite portrait;

        public int startHP;

        public GameObject passivePrefab;
    }
}
