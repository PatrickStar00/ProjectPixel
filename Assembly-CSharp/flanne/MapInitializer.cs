using System;
using UnityEngine;

namespace flanne
{
		public class MapInitializer : MonoBehaviour
	{
				private void Start()
		{
			MapData mapData = SelectedMap.MapData;
			if (mapData == null)
			{
				mapData = this.defaultMap;
			}
			this.hordeSpawner.LoadSpawners(mapData.spawnSessions);
			this.bossSpawner.LoadSpawners(mapData.bossSpawns);
			this.gameTimer.timeLimit = mapData.timeLimit;
		}

				[SerializeField]
		private HordeSpawner hordeSpawner;

				[SerializeField]
		private BossSpawner bossSpawner;

				[SerializeField]
		private MapData defaultMap;

				[SerializeField]
		private GameTimer gameTimer;
	}
}
