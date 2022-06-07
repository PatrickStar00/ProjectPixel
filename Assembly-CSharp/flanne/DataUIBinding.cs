using System;
using UnityEngine;

namespace flanne
{
		public abstract class DataUIBinding<T> : MonoBehaviour where T : ScriptableObject
	{
								public T data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
				this.Refresh();
			}
		}

				private void Start()
		{
			this.Refresh();
		}

				public abstract void Refresh();

				[SerializeField]
		private T _data;
	}
}
