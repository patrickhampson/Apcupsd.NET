using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ApcupsdLib.Objects;

namespace ApcupsdLib
{
    public class ApcupsdClient
    {
        private static readonly byte[] GetStatusMessage = new byte[] { 0x00, 0x06, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73 };
        private static readonly byte[] GetEventsMessage = new byte[] { 0x00, 0x06, 0x65, 0x76, 0x65, 0x6e, 0x74, 0x73 };
        private static readonly byte[] EndBytes = new byte[] { 0x00, 0x00 };

        private string host;
        private int port;
        private ApcupsdMessageProcessor messageProcessor;

        public ApcupsdClient(string host, int port)
        {
            this.host = host;
            this.port = port;

            this.messageProcessor = new ApcupsdMessageProcessor();
        }

        public UpsStatus GetStatus()
        {
            var arr = this.ExecuteClientAction(GetStatusMessage);
            var ret = this.messageProcessor.ParseUpsStatusMessage(arr);
            return ret;
        }

        public UpsEvent[] GetEvents()
        {
            var arr = this.ExecuteClientAction(GetEventsMessage);
            var ret = this.messageProcessor.ParseUpsEventMessages(arr);
            return ret;
        }

        public UpsEvent[] GetEventsFromFile(string filePath = "/var/log/apcupsd.events")
        {
            var events = new List<UpsEvent>();

            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();
                    var evt = this.messageProcessor.ParseUpsEventMessage(line);
                    events.Add(evt);
                }
            }

            return events.ToArray();
        }

        private string[] ExecuteClientAction(byte[] sendMessage)
        {
            var ret = new List<string>();
            using (TcpClient client = new TcpClient())
            {
                client.Connect(this.host, this.port);
                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(sendMessage, 0, sendMessage.Length);
                    Thread.Sleep(20); // TODO: Properly handle not enough data being in the buffer.

                    var start = new byte[2];
                    stream.Read(start, 0, 2);
                    while(!start.SequenceEqual(EndBytes))
                    {
                        var line = new byte[start[1]];
                        stream.Read(line, 0, start[1]);
                        var lineStr = Encoding.ASCII.GetString(line);
                        ret.Add(lineStr);
                        stream.Read(start, 0, 2);
                    } while (!start.SequenceEqual(EndBytes));
                }
            }

            return ret.ToArray();
        }
    }
}
