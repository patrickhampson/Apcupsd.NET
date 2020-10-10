using System;
using System.Collections.Generic;
using System.Text;

namespace ApcupsdLib.Objects
{
    public enum EventType
    {
        Unknown,
        CommLost,
        PowerFailure,
        OnBatt,
        PowerRestore,
        SelfTestStart,
        SelfTestComplete
    }
}
