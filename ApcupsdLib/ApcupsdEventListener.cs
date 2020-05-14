using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using ApcupsdLib.Objects;

namespace ApcupsdLib
{
    public class ApcupsdEventListener
    {
        private class ThreadStartObject
        {
            public string FilePath { get; set; }
            public ManualResetEvent ManualResetEvent { get; set; }
        }

        private bool subscribed = false;
        private bool shouldStop = false;
        private Thread fileSubscriptionThread;
        private ApcupsdMessageProcessor messageProcessor;

        public EventHandler<UpsEventArgs> UpsEventReceived;

        public ApcupsdEventListener()
        {
            this.messageProcessor = new ApcupsdMessageProcessor();
        }

        ~ApcupsdEventListener()
        {
            this.shouldStop = true;
        }

        public void SubscribeToEventsFile(string filePath = "/var/log/apcupsd.events", int fileOpenTimeoutMs = 500)
        {
            if (this.subscribed)
            {
                throw new Exception("Already subscribed to a file.  You must first unsubscribe before subscribing again.");
            }

            this.shouldStop = false;
            this.fileSubscriptionThread = new Thread(eventFileThread);
            var manualResetEvent = new ManualResetEvent(false);
            var threadStartObject = new ThreadStartObject() 
            {
                    FilePath = filePath,
                    ManualResetEvent = manualResetEvent
            };
            this.fileSubscriptionThread.Start(threadStartObject);

            if (!manualResetEvent.WaitOne(fileOpenTimeoutMs))
            {
                this.shouldStop = true;
                this.fileSubscriptionThread.Abort();
                throw new TimeoutException($"Thread did not start reading within specified timeout {fileOpenTimeoutMs}ms");
            }
            this.subscribed = true;
        }

        public void UnsubscribeFromEventsFile()
        {
            this.shouldStop = true;
            this.subscribed = false;
        }

        private void eventFileThread(object data)
        {
            try
            {
                var threadStart = (ThreadStartObject) data;
                using (FileStream stream = new FileStream(threadStart.FilePath, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(stream))
                {
                    long lastMaxOffset = reader.BaseStream.Length;

                    threadStart.ManualResetEvent.Set();
                    while (!this.shouldStop)
                    {
                        Thread.Sleep(100);

                        if (reader.BaseStream.Length == lastMaxOffset)
                        {
                            continue;
                        }

                        var events = new List<UpsEvent>();
                        reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var evt = this.messageProcessor.ParseUpsEventMessage(line);
                            events.Add(evt);
                        }

                        lastMaxOffset = reader.BaseStream.Position;

                        var eventArgs = new UpsEventArgs(events.ToArray());
                        UpsEventReceived?.Invoke(this, eventArgs);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                // Ignore thread abort.
            }
        }
    }
}
