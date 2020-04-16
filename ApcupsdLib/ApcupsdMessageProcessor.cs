using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApcupsdLib
{
    public class ApcupsdMessageProcessor
    {
        private static readonly Regex ExtraCharacters = new Regex(@"\0[^\\]", RegexOptions.Compiled);
        private static readonly Regex ControlCharacters = new Regex(@"[\p{Cc}-[\r\n]]+", RegexOptions.Compiled);

        public ApcupsdMessageProcessor()
        {
        }

        public UpsStatus ParseUpsStatusMessage(string message)
        {
            //var regex = new Regex(@"\0[^\\]");
            message = ExtraCharacters.Replace(message, string.Empty);
            message = ControlCharacters.Replace(message, string.Empty); // todo: Collapse into one regex

            var dict = new Dictionary<string, string>();

            var lines = message.Split('\n').Select(line => line.Split(new[] { ':' }, 2));
            foreach (var line in lines)
            {
                if (line.Length == 2)
                {
                    dict.Add(line[0].Trim(), line[1].Trim());
                }
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
                // todo: Status = (Status)Enum.Parse(typeof(Status), dict.GetStringOrEmpty("STATUS")), // it is possible to have multiple values for status "ONLINE REPLACEBATT"
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
    }
}
