using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using ApcupsdLib.Objects;

namespace ApcupsdLib
{
    public class ApcupsdMessageProcessor
    {
        public ApcupsdMessageProcessor()
        {
        }

        public UpsStatus ParseUpsStatusMessage(string[] lines)
        {

            var dict = new Dictionary<string, string>();

            var splitLines = lines.Select(line => line.Split(new[] { ':' }, 2));
            foreach (var line in splitLines)
            {
                if (line.Length == 2)
                {
                    dict.Add(line[0].Trim(), line[1].Trim());
                }
            }

            var statusStr = dict.GetStringOrEmpty("STATUS");
            var status = Status.Unknown;
            if (!string.IsNullOrWhiteSpace(statusStr))
            {
                // it is possible to have multiple values for status "ONLINE REPLACEBATT"
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                statusStr = ti.ToTitleCase(ti.ToLower(statusStr));
                status = (Status)Enum.Parse(typeof(Status), statusStr.Replace(' ', ','));
            }

            var ret = new UpsStatus()
            {
                Apc = dict.GetStringOrEmpty("APC"),
                Date = dict.GetDateTime("DATE"),
                Hostname = dict.GetStringOrEmpty("HOSTNAME"),
                Version = dict.GetStringOrEmpty("VERSION"),
                UpsName = dict.GetStringOrEmpty("UPSNAME"),
                Cable = dict.GetStringOrEmpty("CABLE"),
                Driver = dict.GetStringOrEmpty("DRIVER"),
                Model = dict.GetStringOrEmpty("MODEL"),
                UpsMode = dict.GetStringOrEmpty("UPSMODE"),
                StartTime = dict.GetDateTime("STARTTIME"),
                Status = status,
                MasterUpd = dict.GetNullableDateTime("MASTERUPD"),
                EndApc = dict.GetDateTime("END APC"),

                LineV = dict.GetNullableDouble("LINEV"),
                LoadPct = dict.GetNullableDouble("LOADPCT"),
                BCharge = dict.GetNullableDouble("BCHARGE"),
                TimeLeft = dict.GetNullableDouble("TIMELEFT"),
                MBattChg = dict.GetNullableDouble("MBATTCHG"),
                MinTimeL = dict.GetNullableDouble("MINTIMEL"),
                MaxTime = dict.GetNullableInt("MAXTIME"),
                MaxLineV = dict.GetNullableDouble("MAXLINEV"),
                OutputV = dict.GetNullableDouble("OUTPUTV"),
                // todo: sense
                LoTrans = dict.GetNullableDouble("LOTRANS"),
                HiTrans = dict.GetNullableDouble("HITRANS"),
                RetPct = dict.GetNullableDouble("RETPCT"),
                ITemp = dict.GetNullableDouble("ITEMP"),
                AlarmDel = dict.GetNullableInt("ALARMDEL"),
                BattV = dict.GetNullableDouble("BATTV"),
                LineFreq = dict.GetNullableInt("LINEFREQ"),
                LastXfer = dict.GetStringOrEmpty("LASTXFER"),
                NumXfers = dict.GetNullableInt("NUMXFERS"),
                TOnBatt = dict.GetNullableInt("TOnBatt"),
                CumOnBatt = dict.GetNullableInt("CUMONBATT"),
                XOffBatt = dict.GetNullableDateTime("XOFFBATT"),
                SelfTest = dict.GetNullableBool("SELFTEST"),
                StatFlag = dict.GetStringOrEmpty("STATFLAG"),
                SerialNo = dict.GetStringOrEmpty("SERIALNO"),
                BattDate = dict.GetNullableDateTime("BATTDATE"),
                NomInV = dict.GetNullableDouble("NOMINV"),
                NomBattV = dict.GetNullableDouble("NOMBATTV"),
                Firmware = dict.GetStringOrEmpty("FIRMWARE")
            };

            return ret;
        }

        public UpsEvent[] ParseUpsEventMessages(string[] lines)
        {
            var events = new List<UpsEvent>();
            
            foreach(var line in lines)
            {
                var evt = this.ParseUpsEventMessage(line);
                events.Add(evt);
            }

            return events.ToArray();
        }

        public UpsEvent ParseUpsEventMessage(string line)
        {
            var evt = new UpsEvent()
            {
                Timestamp = DateTime.Parse(line.Substring(0, 25)),
                Message = line.Substring(27, (line.Length - 26) - 1)
            };

            return evt;
        }
    }
}
