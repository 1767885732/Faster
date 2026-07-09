using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Entities
{
    [Description("患者关系信息")]
    public class MedPatRelationInfo : EntityBase<string>, ICreatedTime
    {
        /// <summary>
        /// 院前急救患者ID
        /// </summary>
        [MaxLength(60)]
        public string PhepId { get; set; }

        /// <summary>
        /// 院前急救任务ID
        /// </summary>
        [MaxLength(60)]
        public string MissionId { get; set; }

        /// <summary>
        /// 门诊号(可重复)
        /// </summary>
        [MaxLength(60)]
        public string ClinicNo { get; set; }

        /// <summary>
        /// 院内急诊编号
        /// </summary>
        [MaxLength(60)]
        public string EmergencyId { get; set; }

        /// <summary>
        /// 院内HIS挂号编号(唯一号)
        /// </summary>
        [MaxLength(60)]
        public string RegistId { get; set; }

        /// <summary>
        /// 住院号(可重复)
        /// </summary>
        [MaxLength(60)]
        public string InpNo { get; set; }

        /// <summary>
        /// 住院登记号(唯一号)
        /// </summary>
        [MaxLength(60)]
        public string InpRegistId { get; set; }

        /// <summary>
        /// 妇幼保健号
        /// </summary>
        [MaxLength(60)]
        public string MacNo { get; set; }

        /// <summary>
        /// 母亲身份证
        /// </summary>
        [MaxLength(60)]
        public string MotherIdCard { get; set; }

        /// <summary>
        /// 患者首次来源<see cref="PatientSourceType"/>
        /// </summary>
        [MaxLength(10)]
        public string PatientSource { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1000)]
        public string Memo { get; set; }

        /// <summary>
        /// 院区编码(机构号)
        /// </summary>
        [MaxLength(60)]
        public string OrgCode { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string MrNo { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string OtherNo { get; set; }

        /// <summary>
        /// HIS就诊ID
        /// </summary>
        [MaxLength(60)]
        public string VisitId { get; set; }
    }
}
