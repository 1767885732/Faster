using System;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMEmergencyPatientRelation
    {
        /// <summary>院前患者id</summary>
        public string PatientId { get; set; }
        /// <summary>急诊患者id</summary>
        public string EmergencyPatientId { get; set; }

        /// <summary>急诊挂号ID</summary>
        public string VisitId { get; set; }

        /// <summary>上传者</summary>
        public string Uploader { get; set; }


        /// <summary>院前任务ID</summary>
        /// <remarks>不需要上传，接口会根据院前患者id查找任务id并赋值</remarks>
        public string MissionId { get; set; }
    }
}
