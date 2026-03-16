//------------------------------------------------------------------------------
//<copyright file="MedPhepPatientInfo_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/19 12:31</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.Patient.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：院前患者基本信息信息
    /// </summary>
    public partial class MedPhepPatientInfo_Configuration : EntityTypeConfigurationBase<MedPhepPatientInfo, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPhepPatientInfo> builder)
        {
            builder.ToTable("MED_PHEP_PATIENT_INFO");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.Name).HasColumnType("VARCHAR2(40)").HasColumnName("NAME");
            builder.Property(t => t.DateOfBirth).HasColumnType("DATE").HasColumnName("DATE_OF_BIRTH");
            builder.Property(t => t.Sex).HasColumnType("VARCHAR2(10)").HasColumnName("SEX");
            builder.Property(t => t.IdNo).HasColumnType("VARCHAR2(20)").HasColumnName("ID_NO");
            builder.Property(t => t.CitizenCard).HasColumnType("VARCHAR2(40)").HasColumnName("CITIZEN_CARD");
            builder.Property(t => t.MailAddress).HasColumnType("VARCHAR2(100)").HasColumnName("MAIL_ADDRESS");
            builder.Property(t => t.PhoneNumer).HasColumnType("VARCHAR2(40)").HasColumnName("PHONE_NUMBER");
            builder.Property(t => t.AttackTime).HasColumnType("DATE").HasColumnName("ATTACK_TIME");
            builder.Property(t => t.AttackAddress).HasColumnType("VARCHAR2(100)").HasColumnName("ATTACK_ADDRESS");
            builder.Property(t => t.CallTime).HasColumnType("DATE").HasColumnName("CALL_TIME");
            builder.Property(t => t.AcceptTime).HasColumnType("DATE").HasColumnName("ACCEPT_TIME");
            builder.Property(t => t.DispatchType).HasColumnType("VARCHAR2(40)").HasColumnName("DISPATCH_TYPE");
            builder.Property(t => t.DispatchVehicletime).HasColumnType("DATE").HasColumnName("DISPATCH_VEHICLETIME");
            builder.Property(t => t.ArrivedTime).HasColumnType("DATE").HasColumnName("ARRIVED_TIME");
            builder.Property(t => t.TouchPatTime).HasColumnType("DATE").HasColumnName("TOUCH_PAT_TIME");
            builder.Property(t => t.LeftTime).HasColumnType("DATE").HasColumnName("LEFT_TIME");
            builder.Property(t => t.ArrivedHosTime).HasColumnType("DATE").HasColumnName("ARRIVED_HOS_TIME");
            builder.Property(t => t.FmcDoctor).HasColumnType("VARCHAR2(20)").HasColumnName("FMC_DOCTOR");
            builder.Property(t => t.FirstDiagnosis).HasColumnType("VARCHAR2(200)").HasColumnName("FIRST_DIAGNOSIS");
            builder.Property(t => t.ToHospital).HasColumnType("VARCHAR2(40)").HasColumnName("TO_HOSPITAL");
            builder.Property(t => t.KillipLevel).HasColumnType("VARCHAR2(40)").HasColumnName("KILLIP_LEVEL");
            builder.Property(t => t.Complaint).HasColumnType("VARCHAR2(40)").HasColumnName("COMPLAINT");
            builder.Property(t => t.Sense).HasColumnType("VARCHAR2(40)").HasColumnName("SENSE");
            builder.Property(t => t.Temp).HasColumnType("VARCHAR2(40)").HasColumnName("TEMP");
            builder.Property(t => t.Br).HasColumnType("VARCHAR2(40)").HasColumnName("BR");
            builder.Property(t => t.Plus).HasColumnType("VARCHAR2(40)").HasColumnName("PLUS");
            builder.Property(t => t.Hr).HasColumnType("VARCHAR2(40)").HasColumnName("HR");
            builder.Property(t => t.Bp).HasColumnType("VARCHAR2(40)").HasColumnName("BP");
            builder.Property(t => t.Spo2).HasColumnType("VARCHAR2(40)").HasColumnName("SPO2");
            builder.Property(t => t.BloodSugar).HasColumnType("VARCHAR2(40)").HasColumnName("BLOOD_SUGAR");
            builder.Property(t => t.SpecialCheck).HasColumnType("VARCHAR2(100)").HasColumnName("SPECIAL_CHECK");
            builder.Property(t => t.Res1).HasColumnType("VARCHAR2(40)").HasColumnName("RES1");
            builder.Property(t => t.Res2).HasColumnType("VARCHAR2(40)").HasColumnName("RES2");
            builder.Property(t => t.Symptom).HasColumnType("VARCHAR2(40)").HasColumnName("SYMPTOM");
            builder.Property(t => t.NotifyEmergency).HasColumnType("DATE").HasColumnName("NOTIFY_EMERGENCY");
            builder.Property(t => t.DiagnosisTime).HasColumnType("DATE").HasColumnName("DIAGNOSIS_TIME");
            builder.Property(t => t.PresentIllness).HasColumnType("VARCHAR2(500)").HasColumnName("PRESENT_ILLNESS");
            builder.Property(t => t.Allergichistory).HasColumnType("VARCHAR2(200)").HasColumnName("ALLERGICHISTORY");
            builder.Property(t => t.Pasthistory).HasColumnType("VARCHAR2(100)").HasColumnName("PASTHISTORY");
            builder.Property(t => t.Conditiongrade).HasColumnType("VARCHAR2(40)").HasColumnName("CONDITIONGRADE");
            builder.Property(t => t.TherapeuticMethod).HasColumnType("VARCHAR2(500)").HasColumnName("THERAPEUTIC_METHOD");
            builder.Property(t => t.InfectionHistory).HasColumnType("VARCHAR2(200)").HasColumnName("INFECTION_HISTORY");
            builder.Property(t => t.MedicationHistory).HasColumnType("VARCHAR2(200)").HasColumnName("MEDICATION_HISTORY");
            builder.Property(t => t.RecoveryHistory).HasColumnType("VARCHAR2(200)").HasColumnName("RECOVERY_HISTORY");
            builder.Property(t => t.NurseName).HasColumnType("VARCHAR2(20)").HasColumnName("NURSE_NAME");
        }
    }
}