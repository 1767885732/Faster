using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class MedicalInfo
    {
        /// <summary>
        /// 病历列表
        /// </summary>
        public List<MeidcalListItem> MeidcalList { get; set; }
    }

    public class MeidcalListItem
    {
        /// <summary>
        /// 病历信息
        /// </summary>
        public List<MedicalInfoItem> MedicalInfo { get; set; }
    }

    public class MedicalInfoItem
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string BRXM { get; set; }

        public string LSH { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string BRXB { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string AGE { get; set; }

        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CLBH { get; set; }

        /// <summary>
        /// 送往地点
        /// </summary>
        public string SWDD { get; set; }

        /// <summary>
        /// 现场地点
        /// </summary>
        public string XCDD { get; set; }

        /// <summary>
        /// 初步印象
        /// </summary>
        public string CBYX { get; set; }

        /// <summary>
        /// 首次保存时间
        /// </summary>
        public string SAVETIME { get; set; }

        /// <summary>
        /// 首次提交时间
        /// </summary>
        public string SUBMITTIME { get; set; }

        /// <summary>
        /// 登记医生
        /// </summary>
        public string DOCTORNAME { get; set; }

        /// <summary>
        /// 病历类型
        /// </summary>
        public string MEDICALTYPE { get; set; }

        /// <summary>
        /// 登记病历站点
        /// </summary>
        public string SSDWMC { get; set; }

        /// <summary>
        /// ICD10编码
        /// </summary>
        public string ICD10 { get; set; }

        /// <summary>
        /// 患者措施处理列表
        /// </summary>
        public List<MeasuresInfoItem> MeasuresInfo { get; set; }

        /// <summary>
        /// 患者药材处理
        /// </summary>
        public List<MedicineInfoItem> MedicineInfo { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public List<ExtendedFieldItem> ExtendedField { get; set; }

        public class MeasuresInfoItem
        {
            /// <summary>
            /// 措施编码
            /// </summary>
            public int MEDICALBASICDATAID { get; set; }

            /// <summary>
            /// 病历编码
            /// </summary>
            public int MEDICALID { get; set; }

            /// <summary>
            /// 措施名称
            /// </summary>
            public string INSPECTIONITEM { get; set; }
            /// <summary>
            /// 扩展字段2
            /// </summary>
            public string EXTENDEDFIELD2 { get; set; }

            /// <summary>
            /// 扩展字段1
            /// </summary>
            public string EXTENDEDFIELD1 { get; set; }

            /// <summary>
            /// 诊治记录编码
            /// </summary>
            public int TREATEMENTRECORDID { get; set; }
        }

        public class MedicineInfoItem
        {
            /// <summary>
            /// 药材编码
            /// </summary>
            public int DRUGBASICINFOID { get; set; }

            /// <summary>
            /// 葡萄糖酸钙注射液
            /// </summary>
            public string DRUGNAME { get; set; }

            /// <summary>
            /// 使用数量
            /// </summary>
            public int NUM { get; set; }

            /// <summary>
            /// 病历编码
            /// </summary>
            public int MEDICALRECORDID { get; set; }

            /// <summary>
            /// 诊治记录编码
            /// </summary>
            public int TREATMENTRECORDID { get; set; }

            /// <summary>
            /// 规格
            /// </summary>
            public string SPECIFICATIONS { get; set; }

            /// <summary>
            /// 使用方法
            /// </summary>
            public string METHOD { get; set; }
        }

        public class ExtendedFieldItem
        {
            /// <summary>
            /// 字段值
            /// </summary>
            public string VALUE { get; set; }

            /// <summary>
            /// 字段编码
            /// </summary>
            public string CODE { get; set; }

            /// <summary>
            /// 字段名称
            /// </summary>
            public string NAME { get; set; }
        }
    }
}
