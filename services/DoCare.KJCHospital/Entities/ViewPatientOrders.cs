using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewPatientOrders_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewPatientOrders_List PatientOrdersList { get; set; }
    }

    public class ViewPatientOrders_List
    {
        [XmlElement(ElementName = "PatientOrders")]
        public List<ViewPatientOrders> PatientOrders { get; set; }
    }

    public class ViewPatientOrders
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string PATIENT_NO { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [XmlElement(ElementName = "CURRENT_PRESC_NO")]
        public string PRESCRIPTION_NO { get; set; }

        /// <summary>
        /// 父医嘱号、组号
        /// </summary>
        public string ORDER_NO { get; set; }

        /// <summary>
        /// 子医嘱号
        /// </summary>
        public string ORDER_SUB_NO { get; set; }

        /// <summary>
        /// 医嘱标志：1为长期医嘱，0为临时医嘱
        /// </summary>
        public string REPEAT_INDICATOR { get; set; }

        /// <summary>
        /// 医嘱类别
        /// </summary>
        public string ORDER_CLASS { get; set; }

        /// <summary>
        /// 医嘱正文
        /// </summary>
        public string ORDER_TEXT { get; set; }

        /// <summary>
        /// 医嘱代码
        /// </summary>
        public string ORDER_CODE { get; set; }

        /// <summary>
        /// 药品一次使用剂量
        /// </summary>
        public string DOSAGE { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DOSAGE_UNITS { get; set; }

        /// <summary>
        /// 给药途径
        /// </summary>
        public string ADMINISTRATION { get; set; }

        /// <summary>
        /// 医嘱起始日期及时间
        /// </summary>
        public string START_DATE_TIME { get; set; }

        /// <summary>
        /// 医嘱停止日期及时间
        /// </summary>
        public string STOP_DATE_TIME { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public string DURATION { get; set; }

        /// <summary>
        /// 持续时间单位
        /// </summary>
        public string DURATION_UNITS { get; set; }

        /// <summary>
        /// 执行频率描述
        /// </summary>
        public string FREQUENCY { get; set; }

        /// <summary>
        /// 药品总量
        /// </summary>
        public string TOTAL_AMOUNT { get; set; }

        /// <summary>
        /// 开医嘱科室
        /// </summary>
        public string ORDERING_DEPT { get; set; }

        /// <summary>
        /// 开医嘱医生
        /// </summary>
        public string DOCTOR { get; set; }

        /// <summary>
        /// 停医嘱医生
        /// </summary>
        public string STOP_DOCTOR { get; set; }

        /// <summary>
        /// 开医嘱校对护士
        /// </summary>
        public string NURSE { get; set; }

        /// <summary>
        /// 停医嘱校对护士
        /// </summary>
        public string STOP_NURSE { get; set; }

        /// <summary>
        /// 医嘱录入的当前日期及时间
        /// </summary>
        public string ENTER_DATE_TIME { get; set; }

        /// <summary>
        /// 停医嘱录入的当前日期及时间
        /// </summary>
        public string STOP_ORDER_DATE_TIME { get; set; }

        /// <summary>
        /// 医嘱状态
        /// </summary>
        public string ORDER_STATUS { get; set; }

    }
}
