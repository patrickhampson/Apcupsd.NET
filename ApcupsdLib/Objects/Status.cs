using System;
namespace ApcupsdLib.Objects
{
    [Flags]
    public enum Status
    {
        Unknown,
        Cal = 1,
        Trim = 2,
        Boost = 4,
        Online = 8,
        OnBatt = 16,
        Overload = 32,
        LowBatt = 64,
        ReplaceBatt = 128,
        NoBatt = 256,
        Slave = 512,
        SlaveDown = 1024,
        CommLost = 2048,
        ShuttingDown = 4096
    }
}
