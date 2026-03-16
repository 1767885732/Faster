using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Common.Dtos
{
    public class SyncNotifyDto
    {
        public SyncNotifyDto()
        {
            PatientSource = string.Empty;
            PId = string.Empty;
            App = string.Empty;
        }

        /// 推送来源:0-急救;1-急诊;2-门诊;3-住院;
        /// </summary>
        [Required, Range(0, 3, ErrorMessage = "不支持的推送来源")]
        public string PatientSource { get; set; }

        /// <summary>
        /// 推送来源关联编号
        /// </summary>
        [Required]
        public string PId { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        [Required]
        public string PatientId { get; set; }

        /// <summary>
        /// -1.不推送;1.胸痛;2.卒中;3.创伤;4.高危孕产妇;5.高危新生儿;
        /// </summary>
        [Required, Range(-1, 5, ErrorMessage = "不支持的专科推送去向")]
        public string App { get; set; }
    }
}
