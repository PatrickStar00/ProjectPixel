using System;
using UnityEngine;

public class ClampValueModifier : ValueModifier
{
    public ClampValueModifier(int sortOrder, float min, float max) : base(sortOrder)
    {
        this.min = min;
        this.max = max;
    }

    public override float Modify(float fromValue, float toValue)
    {
        return Mathf.Clamp(toValue, this.min, this.max);
    }

    public readonly float min;

    public readonly float max;
}
