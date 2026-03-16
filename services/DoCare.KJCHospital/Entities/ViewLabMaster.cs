using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewLabMaster_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewLabMaster_List LabMasterList { get; set; }
    }

    public class ViewLabMaster_List
    {
        [XmlElement(ElementName = "LabMaster")]
        public List<ViewLabMaster> LabMaster { get; set; }
    }

    /// <summary>
    /// 检验申请单
    /// </summary>
    public class ViewLabMaster
    {
        /// <summary>
        /// 申请序号（报告号）
        /// </summary>
        [XmlElement(ElementName = "TEST_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 优先标志
        /// </summary>
        public string PRIORITY_INDICATOR { get; set; }
        public string PATIENT_NO { get; set; }

        /// <summary>
        /// 病人检验姓名
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 检验目的
        /// </summary>
        public string TEST_CAUSE { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string RELEVANT_CLINIC_DIAG { get; set; }

        /// <summary>
        /// 标本，如血、尿、痰等化验样本
        /// </summary>
        public string SPECIMEN { get; set; }

        /// <summary>
        /// 上述样本的采样时间
        /// </summary>
        public string SPCM_RECEIVED_DATE_TIME { get; set; }

        /// <summary>
        /// 检验申请时间
        /// </summary>
        public string APPLY_TIME { get; set; }

        /// <summary>
        /// 开出该申请单的科室：科室代码
        /// </summary>
        public string ORDERING_DEPT { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ORDERING_PROVIDER { get; set; }

        /// <summary>
        /// 执行该申请单的科室：科室代码
        /// </summary>
        public string PERFORMED_BY { get; set; }

        /// <summary>
        /// 反映申请的执行情况改
        /// </summary>
        public string RESULT_STATUS { get; set; }

        /// <summary>
        /// 检验报告完成时间
        /// </summary>
        public string RESULTS_RPT_DATE_TIME { get; set; }

        /// <summary>
        /// 报告者
        /// </summary>
        public string TRANSCRIPTIONIST { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        public string VERIFIED_BY { get; set; }
    }
}
