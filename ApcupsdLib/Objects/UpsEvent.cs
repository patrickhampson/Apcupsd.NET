using System;
namespace ApcupsdLib.Objects
{
    public class UpsEvent
    {
        public UpsEvent()
        {
        }

        public DateTime Timestamp;
        public string Message;

        public override string ToString()
        {
            return $"{Timestamp} {Message}";
        }
    }
}
