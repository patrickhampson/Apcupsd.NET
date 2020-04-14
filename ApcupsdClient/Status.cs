using System;
namespace ApcupsdLib
{
    [Flags]
    public enum Status
    {
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
