using System;
using UnityEngine;

namespace flanne.RuneSystem
{
		[CreateAssetMenu(fileName = "RuneData", menuName = "RuneData")]
	public class RuneData : ScriptableObject
	{
						public string nameString
		{
			get
			{
				return LocalizationSystem.GetLocalizedValue(this.nameStringID.key);
			}
		}

						public virtual string description
		{
			get
			{
				return LocalizationSystem.GetLocalizedValue(this.descriptionStringID.key);
			}
		}

				public void Apply(PlayerController player)
		{
			Object.Instantiate<GameObject>(this.runePrefab.gameObject).GetComponent<Rune>().Attach(player, this.level);
		}

				public Sprite icon;

				public LocalizedString nameStringID;

				public LocalizedString descriptionStringID;

				public int costPerLevel;

				public Rune runePrefab;

				[NonSerialized]
		public int level;
	}
}
