using System;
namespace ApcupsdLib
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
        public int? LineFreq { get; set; }
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
    }
}
