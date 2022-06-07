using System;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter
{
		private NotificationCenter()
	{
	}

		public void AddObserver(Action<object, object> handler, string notificationName)
	{
		this.AddObserver(handler, notificationName, null);
	}

		public void AddObserver(Action<object, object> handler, string notificationName, object sender)
	{
		if (handler == null)
		{
			Debug.LogError("Can't add a null event handler for notification, " + notificationName);
			return;
		}
		if (string.IsNullOrEmpty(notificationName))
		{
			Debug.LogError("Can't observe an unnamed notification");
			return;
		}
		if (!this._table.ContainsKey(notificationName))
		{
			this._table.Add(notificationName, new Dictionary<object, List<Action<object, object>>>());
		}
		Dictionary<object, List<Action<object, object>>> dictionary = this._table[notificationName];
		object key = (sender != null) ? sender : this;
		if (!dictionary.ContainsKey(key))
		{
			dictionary.Add(key, new List<Action<object, object>>());
		}
		List<Action<object, object>> list = dictionary[key];
		if (!list.Contains(handler))
		{
			if (this._invoking.Contains(list))
			{
				list = (dictionary[key] = new List<Action<object, object>>(list));
			}
			list.Add(handler);
		}
	}

		public void RemoveObserver(Action<object, object> handler, string notificationName)
	{
		this.RemoveObserver(handler, notificationName, null);
	}

		public void RemoveObserver(Action<object, object> handler, string notificationName, object sender)
	{
		if (handler == null)
		{
			Debug.LogError("Can't remove a null event handler for notification, " + notificationName);
			return;
		}
		if (string.IsNullOrEmpty(notificationName))
		{
			Debug.LogError("A notification name is required to stop observation");
			return;
		}
		if (!this._table.ContainsKey(notificationName))
		{
			return;
		}
		Dictionary<object, List<Action<object, object>>> dictionary = this._table[notificationName];
		object key = (sender != null) ? sender : this;
		if (!dictionary.ContainsKey(key))
		{
			return;
		}
		List<Action<object, object>> list = dictionary[key];
		int num = list.IndexOf(handler);
		if (num != -1)
		{
			if (this._invoking.Contains(list))
			{
				list = (dictionary[key] = new List<Action<object, object>>(list));
			}
			list.RemoveAt(num);
		}
	}

		public void Clean()
	{
		string[] array = new string[this._table.Keys.Count];
		this._table.Keys.CopyTo(array, 0);
		for (int i = array.Length - 1; i >= 0; i--)
		{
			string key = array[i];
			Dictionary<object, List<Action<object, object>>> dictionary = this._table[key];
			object[] array2 = new object[dictionary.Keys.Count];
			dictionary.Keys.CopyTo(array2, 0);
			for (int j = array2.Length - 1; j >= 0; j--)
			{
				object key2 = array2[j];
				if (dictionary[key2].Count == 0)
				{
					dictionary.Remove(key2);
				}
			}
			if (dictionary.Count == 0)
			{
				this._table.Remove(key);
			}
		}
	}

		public void PostNotification(string notificationName)
	{
		this.PostNotification(notificationName, null);
	}

		public void PostNotification(string notificationName, object sender)
	{
		this.PostNotification(notificationName, sender, null);
	}

		public void PostNotification(string notificationName, object sender, object e)
	{
		if (string.IsNullOrEmpty(notificationName))
		{
			Debug.LogError("A notification name is required");
			return;
		}
		if (!this._table.ContainsKey(notificationName))
		{
			return;
		}
		Dictionary<object, List<Action<object, object>>> dictionary = this._table[notificationName];
		if (sender != null && dictionary.ContainsKey(sender))
		{
			List<Action<object, object>> list = dictionary[sender];
			this._invoking.Add(list);
			for (int i = 0; i < list.Count; i++)
			{
				list[i](sender, e);
			}
			this._invoking.Remove(list);
		}
		if (dictionary.ContainsKey(this))
		{
			List<Action<object, object>> list2 = dictionary[this];
			this._invoking.Add(list2);
			for (int j = 0; j < list2.Count; j++)
			{
				list2[j](sender, e);
			}
			this._invoking.Remove(list2);
		}
	}

		private Dictionary<string, Dictionary<object, List<Action<object, object>>>> _table = new Dictionary<string, Dictionary<object, List<Action<object, object>>>>();

		private HashSet<List<Action<object, object>>> _invoking = new HashSet<List<Action<object, object>>>();

		public static readonly NotificationCenter instance = new NotificationCenter();
}
