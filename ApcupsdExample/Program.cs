using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ApcupsdLib;

namespace ApcupsdExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ApcupsdClient("10.1.200.10");

            // Get status via NIS
            Console.WriteLine("UPS Status");
            Console.WriteLine("----------");
            var resp = client.GetStatus();
            Console.WriteLine(resp);

            // Get events via NIS
            Console.WriteLine("UPS Events");
            Console.WriteLine("----------");
            var events = client.GetEvents();
            foreach (var evt in events)
            {
                Console.WriteLine(evt);
            }

            // Get events from file
            events = client.GetEventsFromFile();
            foreach (var evt in events)
            {
                Console.WriteLine(evt);
            }

            // Subscribe to event file
            var listener = new ApcupsdEventListener();
            listener.UpsEventReceived += (sender, args) =>
            {
                // args contains an array of UpsEvent.
                foreach (var evt in args.Events)
                {
                    Console.WriteLine($"Received: {evt.Timestamp.ToString()} {evt.Message}");
                }
            };

            // This should match the EVENTSFILE key in the apcupsd.conf.
            listener.SubscribeToEventsFile();

            Thread.Sleep(10000);
            listener.UnsubscribeFromEventsFile();
        }
    }
}
