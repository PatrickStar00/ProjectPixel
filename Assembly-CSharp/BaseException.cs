using System;

public class BaseException
{
    public bool toggle { get; private set; }

    public BaseException(bool defaultToggle)
    {
        this.defaultToggle = defaultToggle;
        this.toggle = defaultToggle;
    }

    public void FlipToggle()
    {
        this.toggle = !this.defaultToggle;
    }

    public readonly bool defaultToggle;
}
