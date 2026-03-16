//------------------------------------------------------------------------------
//<copyright file="MedPatRelationInfoConfiguration.generated.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 14:56</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Patient.Entities;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者关系信息信息
    /// </summary>
    public partial class MedPatRelationInfo_Configuration : EntityTypeConfigurationBase<MedPatRelationInfo, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatRelationInfo> builder)
        {
            builder.ToTable("MED_PAT_RELATION_INFO");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.PhepId).HasColumnType("VARCHAR2(60)").HasColumnName("PHEP_ID");
            builder.Property(t => t.MissionId).HasColumnType("VARCHAR2(60)").HasColumnName("MISSION_ID");
            builder.Property(t => t.ClinicNo).HasColumnType("VARCHAR2(60)").HasColumnName("CLINIC_NO");
            builder.Property(t => t.EmergencyId).HasColumnType("VARCHAR2(60)").HasColumnName("EMERGENCY_ID");
            builder.Property(t => t.RegistId).HasColumnType("VARCHAR2(60)").HasColumnName("REGIST_ID");
            builder.Property(t => t.InpNo).HasColumnType("VARCHAR2(60)").HasColumnName("INP_NO");
            builder.Property(t => t.MacNo).HasColumnType("VARCHAR2(60)").HasColumnName("MAC_NO");
            builder.Property(t => t.MotherIdCard).HasColumnType("VARCHAR2(60)").HasColumnName("MOTHER_ID_CARD");
            builder.Property(t => t.PatientSource).HasColumnType("VARCHAR2(10)").HasColumnName("PATIENT_SOURCE");
            builder.Property(t => t.CreatedTime).HasColumnType("DATE").HasColumnName("CREATED_TIME");
            builder.Property(t => t.Memo).HasColumnType("VARCHAR2(1000)").HasColumnName("MEMO");
            builder.Property(t => t.OrgCode).HasColumnType("VARCHAR2(60)").HasColumnName("ORG_CODE");
            builder.Property(t => t.InpRegistId).HasColumnType("VARCHAR2(60)").HasColumnName("INP_REGIST_ID");
            builder.Property(t => t.MrNo).HasColumnType("VARCHAR2(60)").HasColumnName("MR_NO");
            builder.Property(t => t.OtherNo).HasColumnType("VARCHAR2(60)").HasColumnName("OTHER_NO");
        }
    }
}