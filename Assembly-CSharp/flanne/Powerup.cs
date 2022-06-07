using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public abstract class Powerup : ScriptableObject
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

				public void ApplyAndNotify(GameObject target)
		{
			this.Apply(target);
			this.PostNotification(Powerup.AppliedNotifcation, null);
		}

				protected abstract void Apply(GameObject target);

				public static string AppliedNotifcation = "Powerup.AppliedNotifcation";

				public Sprite icon;

				public LocalizedString nameStringID;

				public LocalizedString descriptionStringID;

				public bool isRepeatable;

				public bool anyPrereqFulfill;

				public List<Powerup> prereqs;

				public PowerupTreeUIData powerupTreeUIData;
	}
}
