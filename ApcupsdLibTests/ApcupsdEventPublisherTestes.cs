using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using ApcupsdLib;
using NUnit.Framework;

namespace ApcupsdLibTests
{
    [TestFixture]
    public class ApcupsdEventPublisherTestes
    {
        [Test]
        public void Test_ReceiveUpsEvent_FromSubscription()
        {
            var pause = new ManualResetEvent(false);

            var testFilePath = "TestData/apcupsd.events.test";
            File.Copy("TestData/apcupsd.events", testFilePath, true);
            var listener = new ApcupsdEventListener();

            var messagesReceived = 0;
            listener.UpsEventReceived += (sender, args) =>
            {
                messagesReceived++;
                Assert.AreEqual(1, args.Events.Length);
                Assert.AreEqual(DateTime.Parse("2020-05-11 00:14:59 +0100"), args.Events[0].Timestamp);
                Assert.AreEqual("Power is back. UPS running on mains.", args.Events[0].Message);
                pause.Set();
            };
            listener.SubscribeToEventsFile(testFilePath);
            File.AppendAllLines(testFilePath, new List<string>()
            {
                "2020-05-11 00:14:59 +0100  Power is back. UPS running on mains."
            });

            Assert.True(pause.WaitOne(500));
            Assert.AreEqual(1, messagesReceived);

            listener.UnsubscribeFromEventsFile();
        }
    }
}
