using System;

namespace flanne
{
		[Serializable]
	public struct LocalizedString
	{
				public LocalizedString(string key)
		{
			this.key = key;
		}

				public static implicit operator LocalizedString(string key)
		{
			return new LocalizedString(key);
		}

				public string key;
	}
}
