using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Score.Entities
{
    /// <summary>
    /// 患者评分信息数据
    /// </summary>
    [Description("患者评分信息数据")]
    public class MedPatScore : EntityBase<System.Guid>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(60)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 评分类别
        /// </summary>
        [MaxLength(60)]
        public string ScoreType { get; set; }

        /// <summary>
        /// 评分来源
        /// </summary>
        [MaxLength(60)]
        public string ScoreSource { get; set; }

        /// <summary>
        /// 评分项目
        /// </summary>
        [MaxLength(60)]
        public string ScoreItem { get; set; }

        /// <summary>
        /// 评分结果
        /// </summary>
        [MaxLength(60)]
        public string ScoreResult { get; set; }

        /// <summary>
        /// 评分时间
        /// </summary>
        public DateTime? ScoreDate { get; set; }
    }
}
