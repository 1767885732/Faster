using System;

namespace DoCare.Core.Patient.Dtos
{
    /// <summary>
    /// 患者列表查询
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 诊断描述
        /// </summary>
        public string DiagnosisDesc { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 患者来源(0:急救;1:急诊 2:门诊 3:住院)
        /// </summary>
        public int DataSource { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string PatientId { get; set; }
    }
}
