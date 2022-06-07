using System;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class Rune : MonoBehaviour
	{
				public void Attach(PlayerController player, int level)
		{
			this.player = player;
			base.transform.SetParent(player.transform);
			this.level = level;
			this.Init();
		}

				protected virtual void Init()
		{
		}

				protected PlayerController player;

				protected int level;
	}
}
