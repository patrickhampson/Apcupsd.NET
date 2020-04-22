using System;
namespace ApcupsdLib.Objects
{
    [Flags]
    public enum Status
    {
        Unknown,
        Cal,
        Trim,
        Boost,
        Online,
        OnBatt,
        Overload,
        LowBatt,
        ReplaceBatt,
        NoBatt,
        Slave,
        SlaveDown,
        CommLost,
        ShuttingDown
    }
}
