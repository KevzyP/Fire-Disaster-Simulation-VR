using System;

internal class waituntil
{
    private Action p;

    public waituntil(Action p)
    {
        this.p = p;
    }
}