using System;
using System.Collections.Generic;
using System.Text;

namespace ApcupsdLib.Objects
{
    public class UpsEventArgs : EventArgs
    {
        public UpsEventArgs(UpsEvent[] events)
        {
            this.Events = events;
        }
        
        public UpsEvent[] Events { get; }
    }
}
