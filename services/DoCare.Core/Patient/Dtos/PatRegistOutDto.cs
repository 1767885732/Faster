using DoCare.Core.Patient.Entities;
using FastFrame.Mapping;
using System;

namespace DoCare.Core.Patient.Dtos
{
    [Serializable]
    [MapFrom(typeof(MedPatientRegistInfo))]
    public class PatRegistOutDto : MedPatientRegistInfo
    {
        /// <summary>
        /// 患者来源Id
        /// </summary>
        public string RelationId { get; set; }

        /// <summary>
        /// 用来显示的号(门诊号/住院号)
        /// </summary>
        public string DisplayId { get; set; }

        /// <summary>
        /// 排序规则显示日期
        /// </summary>
        public string RegistTime { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 就诊科室
        /// </summary>
        public string DeptInCode { get; set; }

        /// <summary>
        ///  病区代码
        /// </summary>
        public string DeptFirstCode { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string PatientSay { get; set; }

        /// <summary>
        /// 是否为抢救室患者
        /// </summary>
        public string IsDocare { get; set; }

        /// <summary>
        /// 腕带编号
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string MrNo { get; set; }

        public string DcotorName { get; set; }

        public string NurseName { get; set; }
    }
}
