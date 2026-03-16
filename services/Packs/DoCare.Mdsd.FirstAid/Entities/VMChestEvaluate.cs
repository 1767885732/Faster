using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>胸痛评估</summary>
    public class VMChestEvaluate
    {
        /// <summary>主键</summary>
        public string EvaluateId { get; set; }

        /// <summary>院前告知时间</summary>
        public DateTime? NoticeTime { get; set; }

        /// <summary>呼吸困难</summary>
        public string IsDyspnea { get; set; }

        /// <summary>微信通知时间</summary>
        public DateTime? WechatTime { get; set; }

        /// <summary>腹痛</summary>
        public string IsAbdominalPain { get; set; }

        /// <summary>心衰</summary>
        public string IsHeartFailure { get; set; }

        /// <summary>恶性心律失常</summary>
        public string IsMalignantArrhythmia { get; set; }

        /// <summary>间断性胸闷</summary>
        public string IsIntermittentChestTightnes { get; set; }

        /// <summary>持续性胸闷</summary>
        public string IsPersistentChestTightness { get; set; }

        /// <summary>离开导管室时间</summary>
        public DateTime? LeftConduitTime { get; set; }

        /// <summary>分诊心内科时间</summary>
        public DateTime? TriageCardiologyTime { get; set; }

        /// <summary>(作废)离开CCU时间</summary>
        public DateTime? LeftCcuTime { get; set; }

        /// <summary>胸痛症状缓解</summary>
        public string IsRemission { get; set; }

        /// <summary>分诊CCU时间</summary>
        public DateTime? TriageCcuTime { get; set; }

        /// <summary>接诊准备时间</summary>
        public DateTime? ReceptTime { get; set; }

        /// <summary>导管室启动时间</summary>
        public DateTime? ConduitStartTime { get; set; }

        /// <summary>申请导管室时间</summary>
        public DateTime? ApplyConduitTime { get; set; }

        /// <summary>分诊导管室时间</summary>
        public DateTime? TriageConduitTime { get; set; }

        /// <summary>休克</summary>
        public string IsShock { get; set; }

        /// <summary>到达导管室时间</summary>
        public DateTime? ArrivedConduitTime { get; set; }

        /// <summary>(作废)到达CCU时间</summary>
        public DateTime? ArrivedCcuTime { get; set; }

        /// <summary>分诊急诊科时间</summary>
        public DateTime? TriageEmergencyTime { get; set; }
        /// <summary>病情评估</summary>
        public string IllEvaluate { get; set; }


        /// <summary>KILLIP分级</summary>
        public string Killip { get; set; }
        public string KillipText { get; set; }

        /// <summary>初步诊断</summary>
        public string PreliminaryDiagnosis { get; set; }
        public string PreliminaryDiagnosisText { get; set; }
    }
}
