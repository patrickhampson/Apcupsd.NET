# Apcupsd.NET
[![Build status](https://ci.appveyor.com/api/projects/status/9cf6tptxbnrdv7i1?svg=true)](https://ci.appveyor.com/project/patdaman45/apcupsd-net)

A C# implementation of the apcupsd NIS protocol (http://www.apcupsd.org/manual/manual.html#nis-network-server-protocol).  In the future I plan on adding a configuration parser / manager, a GetEvents NIS call, as well as C# event handlers to subscribe to events from apcupsd.

### Where to get it

Run the following command in the NuGet Package Manager console to install the library or search in Visual Studio for:

    PM> Install-Package Apcupsd.NET
    
### How to use
Make sure your /etc/apcupsd/apcupsd.conf has the following set:

    NETSERVER on
    NISIP 127.0.0.1 #Set this to 0.0.0.0 to bind to all interfaces or a specific ip
    NISPORT 3551

Use the simple client in C#:

    var client = new ApcupsdClient("127.0.0.1", 3551);
    client.Connect();
    var resp = client.GetStatus();
    
    
### Bugs or suggestions?

Open an issue.

