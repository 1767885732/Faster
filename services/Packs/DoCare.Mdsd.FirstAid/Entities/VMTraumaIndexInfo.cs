
namespace DoCare.Mdsd.FirstAid.Entities
{
   public class VMTraumaIndexInfo
    {
        /// <summary>主键</summary>
        public string Id { get; set; }

        /// <summary>外出血</summary>
        public int? ExternalHemorrhage { get; set; }

        /// <summary>总评分</summary>
        public int? TotalScore { get; set; }




        /// <summary>血压(mmHg)</summary>
        public int? BloodPressure { get; set; }
        public string BloodPressureText { get; set; }

        /// <summary>受伤部位(1-四肢、3-背部、5-胸部、6-头、颈、腹)</summary>
        public int? InjuredPosition { get; set; }
        public string InjuredPositionText { get; set; }

        /// <summary>脉搏(次/分)(100~140、＞140、＜50)</summary>
        public int? Pulse { get; set; }
        public string PulseText { get; set; }

        /// <summary>呼吸状态(1-胸痛、3-呼吸困难、5-发绀、6-无呼吸)</summary>
        public int? BreathState { get; set; }
        public string BreathStateText { get; set; }

        /// <summary>意识状态(1-嗜睡、3-恍惚、5-浅昏迷、6-深昏迷)</summary>
        public int? ConsciousState { get; set; }
        public string ConsciousStateText { get; set; }

        /// <summary>损伤类型(1-撕裂伤、3-挫伤、5-刺伤、6-钝器伤、子弹伤)</summary>
        public int? InjuryType { get; set; }
        public string InjuryTypeText { get; set; }
    }
}
