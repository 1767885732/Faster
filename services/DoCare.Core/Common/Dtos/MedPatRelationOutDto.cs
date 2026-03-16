using DoCare.Core.Patient.Entities;
using FastFrame.Entity;
using FastFrame.Mapping;
using System;
using System.ComponentModel;

namespace DoCare.Core.Common.Dtos
{
    [MapFrom(typeof(MedPatRelationInfo))]
    public class MedPatRelationOutDto: IOutputDto
    {
        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        [Description("患者唯一编号")]
        public int PatientNo { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置 更新时间
        /// </summary>
        [Description("更新时间")]
        public DateTime? UpdateTime { get; set; }
    }
}
