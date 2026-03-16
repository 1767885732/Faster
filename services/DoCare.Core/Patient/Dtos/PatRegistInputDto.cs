using System;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Dtos
{
    /// <summary>
    /// 批量查询患者参数DTO
    /// </summary>
    public class PatRegistInputDto
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 腕带号
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 是否绑定腕带(0:不限;1:已绑)
        [Required, Range(0, 1, ErrorMessage = "绑定参数不在有效范围")]
        /// </summary>
        public int BindFlag { get; set; }

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
        /// 患者来源(0:急救;1:急诊 2:门诊 3:住院)
        /// </summary>
        [Required, Range(0, 3, ErrorMessage = "患者来源不在有效范围")]
        public int DataSource { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 是否筛选
        /// </summary>
        public int IsFliter { get; set; } = 1;
    }
}
