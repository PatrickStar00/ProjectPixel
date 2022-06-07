using System;

public abstract class Modifier
{
		public Modifier(int sortOrder)
	{
		this.sortOrder = sortOrder;
	}

		public readonly int sortOrder;
}
