using System;
using System.Text;

namespace ApcupsdLib.Objects
{
    public class UpsStatus
    {
        public UpsStatus()
        {
        }

        public string Apc { get; set; }
        public DateTime Date { get; set; }
        public string Hostname { get; set; }
        public string Version { get; set; }
        public string UpsName { get; set; }
        public string Cable { get; set; }
        public string Driver { get; set; }
        public string Model { get; set; }
        public string UpsMode { get; set; }
        public DateTime StartTime { get; set; }
        public Status Status { get; set; }
        public DateTime? MasterUpd { get; set; }
        public DateTime EndApc { get; set; }

        public double? LineV { get; set; }
        public double? LoadPct { get; set; }
        public double? BCharge { get; set; }
        public double? TimeLeft { get; set; }
        public double? MBattChg { get; set; }
        public double? MinTimeL { get; set; }
        public int? MaxTime { get; set; }
        public double? MaxLineV { get; set; }
        public double? OutputV { get; set; }
        public Sense Sense { get; set; }

        public double? LoTrans { get; set; }
        public double? HiTrans { get; set; }
        public double? RetPct { get; set; }
        public double? ITemp { get; set; }
        public int? AlarmDel { get; set; }
        public double? BattV { get; set; }
        public double? LineFreq { get; set; }
        public string LastXfer { get; set; }
        public int? NumXfers { get; set; }
        public int? TOnBatt { get; set; }
        public int? CumOnBatt { get; set; }
        public DateTime? XOffBatt { get; set; }
        public bool? SelfTest { get; set; }
        public string StatFlag { get; set; }
        public string SerialNo { get; set; }
        public DateTime? BattDate { get; set; }
        public double? NomInV { get; set; }
        public double? NomBattV { get; set; }
        public int? NomPower { get; set; }
        public string Firmware { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("APC      : {0}\n", this.Apc);
            sb.AppendFormat("DATE     : {0}\n", this.Date);
            sb.AppendFormat("HOSTNAME : {0}\n", this.Hostname);
            sb.AppendFormat("VERSION  : {0}\n", this.Version);
            sb.AppendFormat("UPSNAME  : {0}\n", this.UpsName);
            sb.AppendFormat("CABLE    : {0}\n", this.Cable);
            sb.AppendFormat("DRIVER   : {0}\n", this.Driver);
            sb.AppendFormat("MODEL    : {0}\n", this.Model);
            sb.AppendFormat("UPSMODE  : {0}\n", this.UpsMode);
            sb.AppendFormat("STARTTIME: {0}\n", this.StartTime);
            sb.AppendFormat("STATUS   : {0}\n", this.Status);
            _ = this.MasterUpd.HasValue ? sb.AppendFormat("MASTERUPD: {0}\n", this.MasterUpd) : null;
            sb.AppendFormat("LINEV    : {0}\n", this.LineV);
            sb.AppendFormat("LOADPCT  : {0}\n", this.LoadPct);
            sb.AppendFormat("BCHARGE  : {0}\n", this.BCharge);
            sb.AppendFormat("TIMELEFT : {0}\n", this.TimeLeft);
            sb.AppendFormat("MBATTCHG : {0}\n", this.MBattChg);
            sb.AppendFormat("MINTIMEL : {0}\n", this.MinTimeL);
            sb.AppendFormat("MAXTIME  : {0}\n", this.MaxTime);
            sb.AppendFormat("SENSE    : {0}\n", this.Sense);
            sb.AppendFormat("LOTRANS  : {0}\n", this.LoTrans);
            sb.AppendFormat("HITRANS  : {0}\n", this.HiTrans);
            sb.AppendFormat("ALARMDEL : {0}\n", this.AlarmDel);
            sb.AppendFormat("BATTV    : {0}\n", this.BattV);
            sb.AppendFormat("LASTXFER : {0}\n", this.LastXfer);
            sb.AppendFormat("NUMXFERS : {0}\n", this.NumXfers);
            sb.AppendFormat("TONBATT  : {0}\n", this.TOnBatt);
            sb.AppendFormat("XOFFBATT : {0}\n", this.XOffBatt);
            sb.AppendFormat("SELFTEST : {0}\n", this.SelfTest);
            sb.AppendFormat("STATFLAG : {0}\n", this.StatFlag);
            sb.AppendFormat("SERIALNO : {0}\n", this.SerialNo);
            sb.AppendFormat("BATTDATE : {0}\n", this.BattDate);
            sb.AppendFormat("NOMINV   : {0}\n", this.NomInV);
            sb.AppendFormat("NOMBATTV : {0}\n", this.NomBattV);
            sb.AppendFormat("NOMPOWER : {0}\n", this.NomPower);
            sb.AppendFormat("FIRMWARE : {0}\n", this.Firmware);
            sb.AppendFormat("END APC  : {0}\n", this.EndApc);
            return sb.ToString();
        }
    }
}
