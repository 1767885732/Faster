//------------------------------------------------------------------------------
//<copyright file="MedPatNewbornRecord_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:36</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.Patient.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：新生儿信息表信息
    /// </summary>
    public partial class MedPatNewbornRecord_Configuration : EntityTypeConfigurationBase<MedPatNewbornRecord, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatNewbornRecord> builder)
        {
            builder.ToTable("MED_PAT_NEWBORNRECORD");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.MotherName).HasColumnType("VARCHAR2(60)").HasColumnName("MOTHER_NAME");
            builder.Property(t => t.PhoneNumber).HasColumnType("VARCHAR2(60)").HasColumnName("PHONE_NUMBER");
            builder.Property(t => t.PersonSay).HasColumnType("VARCHAR2(60)").HasColumnName("PERSON_SAY");
            builder.Property(t => t.BirthParity).HasColumnType("VARCHAR2(60)").HasColumnName("BIRTH_PARITY");
            builder.Property(t => t.DeliveryTimes).HasColumnType("VARCHAR2(60)").HasColumnName("DELIVERY_TIMES");
            builder.Property(t => t.GestationalAge).HasColumnType("VARCHAR2(60)").HasColumnName("GESTATIONAL_AGE");
            builder.Property(t => t.BirthWay).HasColumnType("VARCHAR2(60)").HasColumnName("BIRTH_WAY");
            builder.Property(t => t.BirthWeight).HasColumnType("VARCHAR2(60)").HasColumnName("BIRTH_WEIGHT");
            builder.Property(t => t.BirthDefect).HasColumnType("VARCHAR2(60)").HasColumnName("BIRTH_DEFECT");
            builder.Property(t => t.AmnioticFluid).HasColumnType("VARCHAR2(60)").HasColumnName("AMNIOTIC_FLUID");
            builder.Property(t => t.AmnioticFluidAmount).HasColumnType("VARCHAR2(60)").HasColumnName("AMNIOTIC_FLUID_AMOUNT");
            builder.Property(t => t.Placenta).HasColumnType("VARCHAR2(60)").HasColumnName("PLACENTA");
            builder.Property(t => t.UmbilicalCord).HasColumnType("VARCHAR2(60)").HasColumnName("UMBILICAL_CORD");
            builder.Property(t => t.PrenatalSteroidHormones).HasColumnType("VARCHAR2(60)").HasColumnName("PRENATAL_STEROID_HORMONES");
            builder.Property(t => t.ApgarOneMinute).HasColumnType("VARCHAR2(100)").HasColumnName("APGAR_ONE_MINUTE");
            builder.Property(t => t.ApgarFiveMinute).HasColumnType("VARCHAR2(100)").HasColumnName("APGAR_FIVE_MINUTE");
            builder.Property(t => t.ApgarTenMinute).HasColumnType("VARCHAR2(100)").HasColumnName("APGAR_TEN_MINUTE");
            builder.Property(t => t.CriticalPoints).HasColumnType("VARCHAR2(60)").HasColumnName("CRITICAL_POINTS");
            builder.Property(t => t.InHosTime).HasColumnType("DATE").HasColumnName("IN_HOS_TIME");
            builder.Property(t => t.InHosDesc).HasColumnType("VARCHAR2(1000)").HasColumnName("IN_HOS_DESC");
            builder.Property(t => t.MotherBloodType).HasColumnType("VARCHAR2(60)").HasColumnName("MOTHER_BLOOD_TYPE");
            builder.Property(t => t.MotherRhBloodType).HasColumnType("VARCHAR2(60)").HasColumnName("MOTHER_RH_BLOOD_TYPE");
            builder.Property(t => t.MotherWhetherSmoke).HasColumnType("VARCHAR2(60)").HasColumnName("MOTHER_WHETHER_SMOKE");
            builder.Property(t => t.MotherInfectionHistory).HasColumnType("VARCHAR2(2000)").HasColumnName("MOTHER_INFECTION_HISTORY");
            builder.Property(t => t.MotherMedicationHistory).HasColumnType("VARCHAR2(2000)").HasColumnName("MOTHER_MEDICATION_HISTORY");
            builder.Property(t => t.MotherAllergiesHistory).HasColumnType("VARCHAR2(2000)").HasColumnName("MOTHER_ALLERGIES_HISTORY");
            builder.Property(t => t.AsphyxiaResuscitationHistory).HasColumnType("VARCHAR2(2000)").HasColumnName("ASPHYXIA_RESUSCITATION_HISTORY");
            builder.Property(t => t.MotherAge).HasColumnType("VARCHAR2(50)").HasColumnName("MOTHER_AGE");
            builder.Property(t => t.MotherIdCard).HasColumnType("VARCHAR2(60)").HasColumnName("MOTHER_ID_CARD");
            builder.Property(t => t.FamilyAddress).HasColumnType("VARCHAR2(200)").HasColumnName("FAMILY_ADDRESS");
            builder.Property(t => t.BornHospital).HasColumnType("VARCHAR2(100)").HasColumnName("BORN_HOSPITAL");
        }
    }
}
