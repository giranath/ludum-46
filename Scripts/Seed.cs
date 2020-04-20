using System;

class Seed
{
    public static ulong Create()
    {
        Guid guid = Guid.NewGuid();

        ulong seed = 0;

        foreach (char c in guid.ToString())
        {
            seed <<= 8;
            seed += (ulong)c;
        }

        return seed;
    }
}
