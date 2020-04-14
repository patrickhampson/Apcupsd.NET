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

            var message = File.ReadAllText("TestData/TCPSocket.txt", Encoding.ASCII);
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
    }
}