//------------------------------------------------------------------------------
//<copyright file="MedPatientRegistInfoConfiguration.generated.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 14:58</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Patient.Entities;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者基本信息信息
    /// </summary>
    public partial class MedPatientRegistInfo_Configuration : EntityTypeConfigurationBase<MedPatientRegistInfo, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatientRegistInfo> builder)
        {
            builder.ToTable("MED_PATIENT_REGIST_INFO");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.Name).HasColumnType("VARCHAR2(60)").HasColumnName("NAME");
            builder.Property(t => t.Age).HasColumnType("VARCHAR2(60)").HasColumnName("AGE");
            builder.Property(t => t.DateOfBirth).HasColumnType("DATE").HasColumnName("DATE_OF_BIRTH");
            builder.Property(t => t.Sex).HasColumnType("VARCHAR2(60)").HasColumnName("SEX");
            builder.Property(t => t.IdNo).HasColumnType("VARCHAR2(60)").HasColumnName("ID_NO");
            builder.Property(t => t.CitizenCard).HasColumnType("VARCHAR2(60)").HasColumnName("CITIZEN_CARD");
            builder.Property(t => t.PatJob).HasColumnType("VARCHAR2(60)").HasColumnName("PAT_JOB");
            builder.Property(t => t.MailAddress).HasColumnType("VARCHAR2(60)").HasColumnName("MAIL_ADDRESS");
            builder.Property(t => t.PhoneNumber).HasColumnType("VARCHAR2(40)").HasColumnName("PHONE_NUMBER");
            builder.Property(t => t.ComeType).HasColumnType("VARCHAR2(20)").HasColumnName("COME_TYPE");
            builder.Property(t => t.OrgCode).HasColumnType("VARCHAR2(40)").HasColumnName("ORG_CODE");
            builder.Property(t => t.DiagLev).HasColumnType("VARCHAR2(20)").HasColumnName("DIAG_LEV");
            builder.Property(t => t.ArriveTypeExtension).HasColumnType("VARCHAR2(10)").HasColumnName("ARRIVE_TYPE_EXTENSION");
            builder.Property(t => t.ContactName).HasColumnType("VARCHAR2(20)").HasColumnName("CONTACT_NAME");
            builder.Property(t => t.PayType).HasColumnType("VARCHAR2(40)").HasColumnName("PAY_TYPE");
            builder.Property(t => t.ContactPhoneNumber).HasColumnType("VARCHAR2(40)").HasColumnName("CONTACT_PHONE_NUMBER");
        }
    }
}