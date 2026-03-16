using DoCare.Core.Patient.Entities;
using FastFrame.Mapping;
using System;

namespace DoCare.Core.Common.Dtos
{
    [Serializable]
    [MapFrom(typeof(MedPatientRegistInfo))]
    public class PatRegisterOutDto : MedPatientRegistInfo
    {
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
    }
}
