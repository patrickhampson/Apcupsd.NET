# Apcupsd.NET
[![Build status](https://ci.appveyor.com/api/projects/status/9cf6tptxbnrdv7i1?svg=true)](https://ci.appveyor.com/project/patdaman45/apcupsd-net)

A C# implementation of the apcupsd NIS protocol (http://www.apcupsd.org/manual/manual.html#nis-network-server-protocol) and event listener based on the apcupsd.events file generated.  In the future I plan on adding a configuration parser / manager.

### Where to get it

Run the following command in the NuGet Package Manager console to install the library or search in Visual Studio for:

    PM> Install-Package Apcupsd.NET
    
### How to use
Make sure your /etc/apcupsd/apcupsd.conf has the following set:

    #NIS Server configuration
    NETSERVER on
    NISIP 127.0.0.1 #Set this to 0.0.0.0 to bind to all interfaces or a specific ip
    NISPORT 3551

    #Events file
    EVENTSFILE /var/log/apcupsd.events

Use the simple client in C# to query NIS:

    var client = new ApcupsdClient("127.0.0.1", 3551);
    var upsStatusResponse = client.GetStatus();
    var upsEventsArray = client.GetEvents();
    
To subscribe to events from the apcupsd.events file:

    var listener = new ApcupsdEventListener();
    listener.UpsEventReceived += (sender, args) =>
    {
        // args contains an array of UpsEvent.
        foreach(var evt in args.Events)
        {
            Console.WriteLine($"Received: {evt.Timestamp.ToString()} {evt.Message}" );  
		}
    };

    // This should match the EVENTSFILE key in the apcupsd.conf.
    listener.SubscribeToEventsFile("/var/log/apcupsd.events");
    
    
### Bugs or suggestions?

Open an issue.

