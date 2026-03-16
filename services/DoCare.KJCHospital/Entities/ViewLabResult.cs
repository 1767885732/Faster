using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewLabResult_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewLabResult_List LabResultList { get; set; }
    }

    public class ViewLabResult_List
    {
        [XmlElement(ElementName = "LabResult")]
        public List<ViewLabResult> LabResult { get; set; }
    }

    /// <summary>
    /// 检验报告结果
    /// </summary>
    public class ViewLabResult
    {
        /// <summary>
        /// 申请序号（报告号）
        /// </summary>
        public string TEST_NO { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ITEM_NO { get; set; }

        /// <summary>
        /// 检验报告项目名称
        /// </summary>
        public string REPORT_ITEM_NAME { get; set; }

        /// <summary>
        /// 检验报告项目代码
        /// </summary>
        public string REPORT_ITEM_CODE { get; set; }

        /// <summary>
        /// 结果正常与否标志
        /// </summary>
        public string ABNORMAL_INDICATOR { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string RESULT { get; set; }

        /// <summary>
        /// 结果单位
        /// </summary>
        public string UNITS { get; set; }

        /// <summary>
        /// 检验日期及时间
        /// </summary>
        public string RESULT_DATE_TIME { get; set; }

        /// <summary>
        /// 检验结果参考值
        /// </summary>
        public string REFERENCE_RESULT { get; set; }
    }
}
