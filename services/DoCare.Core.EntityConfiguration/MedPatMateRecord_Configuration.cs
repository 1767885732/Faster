//------------------------------------------------------------------------------
//<copyright file="MedPatMateRecord_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:35</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.Patient.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：孕产妇信息信息
    /// </summary>
    public partial class MedPatMateRecord_Configuration : EntityTypeConfigurationBase<MedPatMateRecord, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatMateRecord> builder)
        {
            builder.ToTable("MED_PAT_MATERECORD");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.InDept).HasColumnType("VARCHAR2(60)").HasColumnName("IN_DEPT");
            builder.Property(t => t.BedNo).HasColumnType("VARCHAR2(60)").HasColumnName("BED_NO");
            builder.Property(t => t.InHosTime).HasColumnType("DATE").HasColumnName("IN_HOS_TIME");
            builder.Property(t => t.InComplaint).HasColumnType("VARCHAR2(200)").HasColumnName("IN_COMPLAINT");
            builder.Property(t => t.PreganetWeek).HasColumnType("VARCHAR2(60)").HasColumnName("PREGANET_WEEK");
            builder.Property(t => t.PreganetTimes).HasColumnType("VARCHAR2(60)").HasColumnName("PREGANET_TIMES");
            builder.Property(t => t.ProductTimes).HasColumnType("VARCHAR2(60)").HasColumnName("PRODUCT_TIMES");
            builder.Property(t => t.InSense).HasColumnType("VARCHAR2(60)").HasColumnName("IN_SENSE");
            builder.Property(t => t.InT).HasColumnType("VARCHAR2(60)").HasColumnName("IN_T");
            builder.Property(t => t.InP).HasColumnType("VARCHAR2(60)").HasColumnName("IN_P");
            builder.Property(t => t.InR).HasColumnType("VARCHAR2(60)").HasColumnName("IN_R");
            builder.Property(t => t.InBP).HasColumnType("VARCHAR2(60)").HasColumnName("IN_B_P");
            builder.Property(t => t.InSpo2).HasColumnType("VARCHAR2(60)").HasColumnName("IN_SPO2");
            builder.Property(t => t.InGlu).HasColumnType("VARCHAR2(60)").HasColumnName("IN_GLU");
            builder.Property(t => t.FetelHeart).HasColumnType("VARCHAR2(60)").HasColumnName("FETEL_HEART");
            builder.Property(t => t.UterHeight).HasColumnType("VARCHAR2(60)").HasColumnName("UTER_HEIGHT");
            builder.Property(t => t.AbdIndex).HasColumnType("VARCHAR2(60)").HasColumnName("ABD_INDEX");
            builder.Property(t => t.PastHistory).HasColumnType("VARCHAR2(1000)").HasColumnName("PAST_HISTORY");
            builder.Property(t => t.FamilyHistory).HasColumnType("VARCHAR2(1000)").HasColumnName("FAMILY_HISTORY");
            builder.Property(t => t.MenarcheAge).HasColumnType("VARCHAR2(60)").HasColumnName("MENARCHE_AGE");
            builder.Property(t => t.MenstrualDay).HasColumnType("VARCHAR2(60)").HasColumnName("MENSTRUAL_DAY");
            builder.Property(t => t.MenstrualCycle).HasColumnType("VARCHAR2(60)").HasColumnName("MENSTRUAL_CYCLE");
            builder.Property(t => t.MenstrualVolume).HasColumnType("VARCHAR2(60)").HasColumnName("MENSTRUAL_VOLUME");
            builder.Property(t => t.Dysmenorrhea).HasColumnType("VARCHAR2(60)").HasColumnName("DYSMENORRHEA");
            builder.Property(t => t.InitialDiagnosis).HasColumnType("VARCHAR2(1000)").HasColumnName("INITIAL_DIAGNOSIS");
            builder.Property(t => t.InitialDoctor).HasColumnType("VARCHAR2(60)").HasColumnName("INITIAL_DOCTOR");
            builder.Property(t => t.InhosDiagnosis).HasColumnType("VARCHAR2(1000)").HasColumnName("INHOS_DIAGNOSIS");
            builder.Property(t => t.InhosDoctor).HasColumnType("VARCHAR2(60)").HasColumnName("INHOS_DOCTOR");
            builder.Property(t => t.HighRisk).HasColumnType("VARCHAR2(2000)").HasColumnName("HIGH_RISK");
            builder.Property(t => t.SpecialTreatment).HasColumnType("VARCHAR2(200)").HasColumnName("SPECIAL_TREATMENT");
            builder.Property(t => t.ProductedBleed).HasColumnType("VARCHAR2(60)").HasColumnName("PRODUCTED_BLEED");
            builder.Property(t => t.Indication).HasColumnType("VARCHAR2(500)").HasColumnName("INDICATION");
        }
    }
}