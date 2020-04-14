using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ApcupsdLib
{
    public class ApcupsdClient
    {
        private static readonly byte[] GetStatusMessage = new byte[] { 0x00, 0x06, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73 };

        private string host;
        private int port;
        private int readTimeout;
        private TcpClient client;
        private ApcupsdMessageProcessor messageProcessor;

        public ApcupsdClient(string host, int port, int readTimeout = 30000)
        {
            this.host = host;
            this.port = port;
            this.readTimeout = readTimeout;
            this.client = new TcpClient();

            this.messageProcessor = new ApcupsdMessageProcessor();
        }

        public void Connect()
        {
            this.client.Connect(this.host, this.port);
        }

        public UpsStatus GetStatus()
        {
            if (!this.IsConnected)
            {
                throw new Exception("Client is not connected");
            }

            string str;
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = new byte[32];
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.ReadTimeout = this.readTimeout;
                    stream.Write(GetStatusMessage, 0, GetStatusMessage.Length);

                    int numBytesRead;
                    while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
                    {
                        ms.Write(data, 0, numBytesRead);
                    }
                    str = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
                }
            }

            var ret = this.messageProcessor.ParseUpsStatusMessage(str);
            return ret;
        }

        public bool IsConnected => this.client.Connected;
    }
}
