namespace DoCare.Core.Patient.Dtos
{
    /// <summary>
    /// 患者推送参数
    /// </summary>
    public class PushNotifyDto
    {
        public PushNotifyDto()
        {
            PatientSource = string.Empty;
            PId = string.Empty;
            App = string.Empty;
        }

        /// <summary>
        /// 推送来源:0-急救;1-急诊;2-门诊;3-住院;
        /// </summary>
        public string PatientSource { get; set; }

        /// <summary>
        /// 推送来源关联编号
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// -1.不推送;1.胸痛;2.卒中;3.创伤;4.高危孕产妇;5.高危新生儿;
        /// </summary>
        public string App { get; set; }
    }
}
