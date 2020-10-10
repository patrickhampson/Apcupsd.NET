using System;
namespace ApcupsdLib.Objects
{
    public class UpsEvent
    {
        public UpsEvent()
        {
            this.EventType = EventType.Unknown;
        }

        public DateTime Timestamp;
        public string Message;
        public EventType EventType;

        public override string ToString()
        {
            return $"{Timestamp} {Message}";
        }
    }
}
