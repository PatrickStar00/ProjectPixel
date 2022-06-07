using System;

namespace flanne
{
		public class StatsHolder
	{
				public StatMod this[StatType s]
		{
			get
			{
				return this._data[(int)s];
			}
		}

				public StatsHolder()
		{
			for (int i = 0; i < this._data.Length; i++)
			{
				this._data[i] = new StatMod();
			}
		}

				private StatMod[] _data = new StatMod[19];
	}
}
