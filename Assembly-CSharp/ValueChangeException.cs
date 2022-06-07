using System;
using System.Collections.Generic;

public class ValueChangeException : BaseException
{
			public float delta
	{
		get
		{
			return this.toValue - this.fromValue;
		}
	}

		public ValueChangeException(float fromValue, float toValue) : base(true)
	{
		this.fromValue = fromValue;
		this.toValue = toValue;
	}

		public void AddModifier(ValueModifier m)
	{
		if (this.modifiers == null)
		{
			this.modifiers = new List<ValueModifier>();
		}
		this.modifiers.Add(m);
	}

		public float GetModifiedValue()
	{
		if (this.modifiers == null)
		{
			return this.toValue;
		}
		float result = this.toValue;
		this.modifiers.Sort(new Comparison<ValueModifier>(this.Compare));
		for (int i = 0; i < this.modifiers.Count; i++)
		{
			result = this.modifiers[i].Modify(this.fromValue, result);
		}
		return result;
	}

		private int Compare(ValueModifier x, ValueModifier y)
	{
		return x.sortOrder.CompareTo(y.sortOrder);
	}

		public readonly float fromValue;

		public readonly float toValue;

		private List<ValueModifier> modifiers;
}
