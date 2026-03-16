using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Core.Patient.Entities
{
    [Description("患者基本信息")]
    public class MedPatientRegistInfo : EntityBase<string>
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        [MaxLength(60)]
        public string Name { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(60)]
        public string Sex { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [MaxLength(60)]
        public string IdNo { get; set; }

        /// <summary>
        /// 市民卡号
        /// </summary>
        [MaxLength(60)]
        public string CitizenCard { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [MaxLength(60)]
        public string PatJob { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        [MaxLength(60)]
        public string MailAddress { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(40)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        [MaxLength(20)]
        public string ComeType { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        [MaxLength(40)]
        public string OrgCode { get; set; }

        /// <summary>
        /// 分诊等级
        /// </summary>
        [MaxLength(20)]
        public string DiagLev { get; set; }

        /// <summary>
        /// 来院方式,扩展字段
        /// </summary>
        public string ArriveTypeExtension { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 医疗付费方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(40)]
        public string ContactPhoneNumber { get; set; }
    }
}
