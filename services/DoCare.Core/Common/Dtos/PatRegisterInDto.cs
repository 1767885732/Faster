using System;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Common.Dtos
{
    /// <summary>
    /// 批量查询患者参数DTO
    /// </summary>
    public class PatRegisterInDto
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
        /// 开始时间
        /// </summary>
        [Required, DataType(DataType.DateTime)]
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required, DataType(DataType.DateTime)]
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
    }
}
