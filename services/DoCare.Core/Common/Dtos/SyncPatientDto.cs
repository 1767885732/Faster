namespace DoCare.Core.Common
{
    public class SyncPatientDto
    {
        public SyncPatientDto()
        {
            PatientType = "2";
        }

        /// <summary>
        /// 专科中心患者病历号
        /// </summary>
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者类型 1-胸痛、2-卒中
        /// </summary>
        public string PatientType { get; set; }
    }
}
