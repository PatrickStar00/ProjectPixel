using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class PowerupGenerator : MonoBehaviour
	{
				private void Awake()
		{
			PowerupGenerator.CanReroll = false;
		}

				private void Start()
		{
			this.powerupPool = new List<Powerup>(this.profile.powerupPool);
			this.devilPool = new List<Powerup>(this.devilDealProfile.powerupPool);
			this.takenPowerups = new List<Powerup>();
			this._numRepeatables = 0;
			using (List<Powerup>.Enumerator enumerator = this.powerupPool.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isRepeatable)
					{
						this._numRepeatables++;
					}
				}
			}
		}

				public List<Powerup> GetRandomDevilProfile(int num)
		{
			return this.GetRandom(num, this.devilPool);
		}

				public List<Powerup> GetRandom(int num)
		{
			return this.GetRandom(num, this.powerupPool);
		}

				public List<Powerup> GetRandom(int num, List<Powerup> pool)
		{
			List<Powerup> list = new List<Powerup>();
			for (int i = 0; i < num; i++)
			{
				Powerup powerup = null;
				while (powerup == null)
				{
					Powerup powerup2 = pool[Random.Range(0, pool.Count)];
					if (!list.Contains(powerup2) && this.PrereqsMet(powerup2) && (!powerup2.isRepeatable || num > pool.Count - this._numRepeatables))
					{
						powerup = powerup2;
					}
				}
				list.Add(powerup);
			}
			return list;
		}

				public void RemoveFromDevilPool(Powerup powerup)
		{
			this.devilPool.Remove(powerup);
		}

				public void RemoveFromPool(Powerup powerup)
		{
			this.powerupPool.Remove(powerup);
			this.takenPowerups.Add(powerup);
		}

				private bool PrereqsMet(Powerup powerup)
		{
			foreach (Powerup item in powerup.prereqs)
			{
				bool flag = this.takenPowerups.Contains(item);
				if (powerup.anyPrereqFulfill && flag)
				{
					return true;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

				public static bool CanReroll;

				[SerializeField]
		private PowerupPoolProfile profile;

				[SerializeField]
		private PowerupPoolProfile devilDealProfile;

				private List<Powerup> powerupPool;

				private List<Powerup> devilPool;

				private List<Powerup> takenPowerups;

				private int _numRepeatables;
	}
}
