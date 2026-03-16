using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    /// <summary>
    /// 检验报告结果
    /// </summary>
    [Table("VIEW_LAB_RESULT")]
    public class ViewLabResult : IEntity<string>
    {
        [NotMapped]
        public string Id { get; set; }

        /// <summary>
        /// 申请序号（报告号）
        /// </summary>
        [Column("TEST_NO")]
        public string TEST_NO { get; set; }

        /// <summary>
        /// 检验报告项目名称
        /// </summary>
        [Column("REPORT_ITEM_NAME")]
        public string REPORT_ITEM_NAME { get; set; }

        /// <summary>
        /// 检验报告项目代码
        /// </summary>
        [Column("REPORT_ITEM_CODE")]
        public string REPORT_ITEM_CODE { get; set; }

        /// <summary>
        /// 结果正常与否标志
        /// </summary>
        [Column("ABNORMAL_INDICATOR")]
        public string ABNORMAL_INDICATOR { get; set; }

        /// <summary>
        /// 结果正常与否标志（自定义）
        /// </summary>
        //[Column("ABNORMAL_INDICATOR1")]
        //public string ABNORMAL_INDICATOR1 { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        [Column("RESULT")]
        public string RESULT { get; set; }

        /// <summary>
        /// 结果单位
        /// </summary>
        [Column("UNITS")]
        public string UNITS { get; set; }

        /// <summary>
        /// 检验日期及时间
        /// </summary>
        [Column("RESULT_DATE_TIME")]
        public DateTime? RESULT_DATE_TIME { get; set; }

        /// <summary>
        /// 检验结果参考值
        /// </summary>
        [Column("REFERENCE_RESULT")]
        public string REFERENCE_RESULT { get; set; }
    }
}
