using System;

public class Info<T0, T1> : Info<T0>
{
		public Info(T0 arg0, T1 arg1) : base(arg0)
	{
		this.arg1 = arg1;
	}

		public T1 arg1;
}
