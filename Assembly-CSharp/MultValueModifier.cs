using System;

public class MultValueModifier : ValueModifier
{
		public MultValueModifier(int sortOrder, float toMultiply) : base(sortOrder)
	{
		this.toMultiply = toMultiply;
	}

		public override float Modify(float fromValue, float toValue)
	{
		return toValue * this.toMultiply;
	}

		public readonly float toMultiply;
}
