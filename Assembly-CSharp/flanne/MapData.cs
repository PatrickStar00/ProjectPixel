using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		[CreateAssetMenu(fileName = "MapData", menuName = "MapData")]
	public class MapData : ScriptableObject
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

				public bool darknessEnabled;

				public float timeLimit;

				public List<SpawnSession> spawnSessions;

				public List<BossSpawn> bossSpawns;
	}
}
