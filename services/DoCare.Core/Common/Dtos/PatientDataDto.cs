using DoCare.Core.Patient.Entities;

namespace DoCare.Core.Common.Dtos
{
    public class PatientCompleteDto
    {
        /// <summary>
        /// 关系数据
        /// </summary>
        public MedPhepPatientInfo Relation { get; set; }

        /// <summary>
        /// 院前
        /// </summary>
        public MedPhepPatientInfo PhepInfo { get; set; }
        /// <summary>
        /// 院内
        /// </summary>
        public MedPatientRegistInfo Regist { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public MedPatientDetailsInfo Detail { get; set; }
    }
}
