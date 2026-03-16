using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewPatientInHospital_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewPatientInHospital PatientInHospital { get; set; }
    }

    public class ViewPatientInHospital
    {
        /// <summary>
        /// 住院号
        /// </summary>
        [XmlElement(ElementName = "PATIENT_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary>
        public string IN_HOS_TIME { get; set; }

        /// <summary>
        /// 入院科室
        /// </summary>
        public string DEPT_CODE { get; set; }

        /// <summary>
        /// 入院病区
        /// </summary>
        public string WARD_CODE { get; set; }

        /// <summary>
        /// 入院诊断
        /// </summary>
        public string IN_HOS_DIAGNOSIS { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public string IN_DEPT_TIME { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string BED_NO { get; set; }

        /// <summary>
        /// 主治医生
        /// </summary>
        public string DOCTOR { get; set; }
    }
}
