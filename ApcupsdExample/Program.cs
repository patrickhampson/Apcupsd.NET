using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ApcupsdLib;

namespace ApcupsdExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ApcupsdClient("10.1.0.190", 3551);
            Console.WriteLine("UPS Status");
            Console.WriteLine("----------");
            var resp = client.GetStatus();
            Console.WriteLine(resp);

            Console.WriteLine("UPS Events");
            Console.WriteLine("----------");
            var events = client.GetEvents();
            foreach(var evt in events)
            {
                Console.WriteLine(evt);
            }
        }
    }
}
