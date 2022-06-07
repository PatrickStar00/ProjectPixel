using System;

public class AddValueModifier : ValueModifier
{
    public AddValueModifier(int sortOrder, float toAdd) : base(sortOrder)
    {
        this.toAdd = toAdd;
    }

    public override float Modify(float fromValue, float toValue)
    {
        return toValue + this.toAdd;
    }

    public readonly float toAdd;
}
