using System;
using System.IO;
using System.Text;
using ApcupsdLib;
using NUnit.Framework;

namespace ApcupsdLibTests
{
    public class ApcupsdMessageProcessorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ParseUpsStatusMessage_FromTcpSocket()
        {
            var processor = new ApcupsdMessageProcessor();

            var message = File.ReadAllLines("TestData/TCPSocket.txt", Encoding.ASCII);
            var status = processor.ParseUpsStatusMessage(message);
            Assert.AreEqual("001,036,0882",status.Apc);
            Assert.AreEqual(DateTime.Parse("2020-04-14 06:43:03 +0100"), status.Date);
            Assert.AreEqual("raspberrypi", status.Hostname);
            Assert.AreEqual("3.14.14 (31 May 2016) debian", status.Version);
            Assert.AreEqual("raspberrypi", status.UpsName);
            Assert.AreEqual("USB Cable", status.Cable);
            Assert.AreEqual("USB UPS Driver", status.Driver);
            Assert.AreEqual("Stand Alone", status.UpsMode);
            Assert.AreEqual(DateTime.Parse("2020-04-14 02:25:45 +0100"), status.StartTime);
            Assert.AreEqual("Back-UPS XS 1500M", status.Model);

            Assert.AreEqual(100.0, status.BCharge.Value);
        }

        [Test]
        public void Test_ParseUpsEventMessages_FromFile()
        {
            var processor = new ApcupsdMessageProcessor();

            var messages = File.ReadAllLines("TestData/apcupsd.events");
            var events = processor.ParseUpsEventMessages(messages);
            Assert.AreEqual(182, events.Length);
            Assert.AreEqual(DateTime.Parse("2019-10-04 20:39:01 +0100"), events[0].Timestamp);
            Assert.AreEqual("Communications with UPS lost.", events[0].Message);
        }

        [Test]
        public void Test_ParseUpsEventMessage_FromString()
        {
            var processor = new ApcupsdMessageProcessor();
            var evt = processor.ParseUpsEventMessage("2020-04-11 00:14:59 +0100  Power is back. UPS running on mains.");
            Assert.AreEqual(DateTime.Parse("2020-04-11 00:14:59 +0100"), evt.Timestamp);
            Assert.AreEqual("Power is back. UPS running on mains.", evt.Message);
        }
    }
}