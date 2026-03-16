using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewExamReport_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewExamReport_List ExamReportList { get; set; }
    }

    public class ViewExamReport_List
    {
        [XmlElement(ElementName = "ExamReport")]
        public List<ViewExamReport> ExamReport { get; set; }
    }

    public class ViewExamReport
    {
        /// <summary>
        /// 申请单号（报告号）
        /// </summary>
        public string EXAM_NO { get; set; }
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string PATIENT_NO { get; set; }
        /// <summary>
        /// 检查项目类别
        /// </summary>
        [XmlElement(ElementName = "EXAM_SUB_CLASS")]
        public string EXAM_CLASS { get; set; }
        /// <summary>
        /// 检查项目子类
        /// </summary>
        [XmlElement(ElementName = "EXAM_CLASS")]
        public string EXAM_SUB_CLASS { get; set; }
        /// <summary>
        /// 临床诊断
        /// </summary>
        public string CLIN_DIAG { get; set; }
        /// <summary>
        /// 检查方式
        /// </summary>
        public string EXAM_MODE { get; set; }
        /// <summary>
        /// 使用仪器
        /// </summary>
        public string DEVICE { get; set; }
        /// <summary>
        /// 执行科室
        /// </summary>
        public string PERFORMED_BY { get; set; }
        /// <summary>
        /// 申请日期及时间
        /// </summary>
        public string REQ_DATE_TIME { get; set; }
        /// <summary>
        /// 申请科室
        /// </summary>
        public string REQ_DEPT { get; set; }
        /// <summary>
        /// 申请医生
        /// </summary>
        public string REQ_PHYSICIAN { get; set; }
        /// <summary>
        /// 申请备注
        /// </summary>
        public string REQ_MEMO { get; set; }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string NOTICE { get; set; }
        /// <summary>
        /// 检查开始日期时间
        /// </summary>
        public string EXAM_START_DATE { get; set; }
        /// <summary>
        /// 检查结束日期时间
        /// </summary>
        public string EXAM_END_DATE { get; set; }
        /// <summary>
        /// 报告日期及时间
        /// </summary>
        public string REPORT_DATE_TIME { get; set; }
        /// <summary>
        /// 检查参数(部位)
        /// </summary>
        public string EXAM_PARA { get; set; }
        /// <summary>
        /// 检查所见
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 建议
        /// </summary>
        public string CONCLUSION { get; set; }
        /// <summary>
        /// 检查结论
        /// </summary>
        public string RECOMMENDATION { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string TECHNICIAN { get; set; }
        /// <summary>
        /// 报告者
        /// </summary>
        public string REPORTER { get; set; }
        /// <summary>
        /// 检查结果状态
        /// </summary>
        public string RESULT_STATUS { get; set; }
        /// <summary>
        /// 审核者
        /// </summary>
        public string VERIFIED_BY { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string VERIFIED_DATE_TIME { get; set; }
    }
}
