using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewPatientOutHospital_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewPatientOutHospital_List PatientInOutHospital { get; set; }
    }

    public class ViewPatientOutHospital_List
    {
        [XmlElement(ElementName = "PatientInOutHospital")]
        public List<ViewPatientOutHospital> PatientInOutHospitalList { get; set; }
    }

    public class ViewPatientOutHospital
    {
        /// <summary>
        /// 住院号
        /// </summary>
        [XmlElement(ElementName = "PATIENT_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public string OUT_DEPT_TIME { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary>
        public string OUT_HOS_TIME { get; set; }

        /// <summary>
        /// 出院诊断
        /// </summary>
        public string OUT_DIAGNOSIS { get; set; }

        /// <summary>
        /// 住院天数
        /// </summary>
        public string HOS_DAY { get; set; }

        /// <summary>
        /// 总住院费用
        /// </summary>
        public string TOTAL_COST { get; set; }

        /// <summary>
        /// 药品费用
        /// </summary>
        public string DRUG_COST { get; set; }

        /// <summary>
        /// 检查费用
        /// </summary>
        public string EXAM_COST { get; set; }

        /// <summary>
        /// 患者去向(好转，死亡等)
        /// </summary>
        public string OUT_WAY { get; set; }
    }
}
