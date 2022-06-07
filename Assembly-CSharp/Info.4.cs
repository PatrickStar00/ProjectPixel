using System;

public class Info<T0, T1, T2, T3> : Info<T0, T1, T2>
{
		public Info(T0 arg0, T1 arg1, T2 arg2, T3 arg3) : base(arg0, arg1, arg2)
	{
		this.arg3 = arg3;
	}

		public T3 arg3;
}
