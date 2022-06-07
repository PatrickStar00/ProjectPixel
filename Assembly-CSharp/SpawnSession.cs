using System;

[Serializable]
public class SpawnSession
{
		public string objectPoolTag;

		public int HP = 1;

		public int maximum;

		public int numPerSpawn;

		public float spawnCooldown;

		public float startTime;

		public float duration;

		[NonSerialized]
	public float timer;
}
