using System;

public class InfoEventArgs<T> : EventArgs
{
		public InfoEventArgs()
	{
		this.info = default(T);
	}

		public InfoEventArgs(T info)
	{
		this.info = info;
	}

		public T info;
}
