using System;
using ApcupsdLib;

namespace ApcupsdExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApcupsdClient("10.1.0.190", 3551);
            client.Connect();
            var resp = client.GetStatus();
            Console.WriteLine(resp);
        }
    }
}
