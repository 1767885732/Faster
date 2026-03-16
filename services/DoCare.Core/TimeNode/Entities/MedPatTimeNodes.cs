using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.TimeNode.Entities
{
    /// <summary>
    /// 患者腕带时间信息
    /// </summary>
    [Description("患者腕带时间信息")]
    public class MedPatTimeNodes : EntityBase<Guid>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(60)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 腕带ID
        /// </summary>
        [MaxLength(60)]
        public string TagId { get; set; }

        /// <summary>
        /// 时间节点名称
        /// </summary>
        [MaxLength(60)]
        public string ItemName { get; set; }

        /// <summary>
        /// 时间节点值
        /// </summary>
        public DateTime? ItemValue { get; set; }

        /// <summary>
        /// 腕带绑定时间
        /// </summary>
        public DateTime? BindTime { get; set; }
    }
}
