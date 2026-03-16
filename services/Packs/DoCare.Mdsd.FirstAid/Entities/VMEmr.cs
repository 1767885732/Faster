using System.Collections.Generic;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>院前患者病历</summary>
    public class VMEmr
    {
        /// <summary>任务信息</summary>
        public VMMission Mission { get; set; }

        /// <summary>院前患者信息</summary>
        public VMPatient Patient { get; set; }

        /// <summary>新生儿患者信息</summary>
        public VMPatientNewborn PatientNewborn { get; set; }

        /// <summary>病历信息</summary>
        public VMMedicalRecord MedicalRecord { get; set; }

        /// <summary>治疗措施</summary>
        public VMTherapeuticMethod TherapeuticMethod { get; set; }

        /// <summary>格拉斯哥创伤评分</summary>
        public VMWound Wound { get; set; }

        /// <summary>卒中</summary>
        public VMStroke Stroke { get; set; }

        /// <summary>胸痛评估</summary>
        public VMChestEvaluate ChestEvaluate { get; set; }

        /// <summary>TI创伤</summary>
        public VMTraumaIndexInfo TraumaIndex { get; set; }

        /// <summary>交接单</summary>
        public VMShiftRecordInfo ShiftRecord { get; set; }

        /// <summary>用药信息</summary>
        public List<VMEmergencyOrder> EmergencyOrderList { get; set; }

        /// <summary>体格检查</summary>
        public List<VMGeneralInspection> GeneralInspectionList { get; set; }

        /// <summary>Poct</summary>
        public List<VMPoctMaster> PoctList { get; set; }
    }
}
