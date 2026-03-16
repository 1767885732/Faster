//------------------------------------------------------------------------------
//<copyright file="MedPatientDetailsInfoConfiguration.generated.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 15:00</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Patient.Entities;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者详细信息信息
    /// </summary>
    public partial class MedPatientDetailsInfo_Configuration : EntityTypeConfigurationBase<MedPatientDetailsInfo, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatientDetailsInfo> builder)
        {
            builder.ToTable("MED_PATIENT_DETAILS_INFO");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.RegistTime).HasColumnType("DATE").HasColumnName("REGIST_TIME");
            builder.Property(t => t.DeptCode).HasColumnType("VARCHAR2(60)").HasColumnName("DEPT_CODE");
            builder.Property(t => t.ArrivedDeptTime).HasColumnType("DATE").HasColumnName("ARRIVED_DEPT_TIME");
            builder.Property(t => t.TriageTime).HasColumnType("DATE").HasColumnName("TRIAGE_TIME");
            builder.Property(t => t.Doctor).HasColumnType("VARCHAR2(60)").HasColumnName("DOCTOR");
            builder.Property(t => t.AdmissionTime).HasColumnType("DATE").HasColumnName("ADMISSION_TIME");
            builder.Property(t => t.DiagDoctor).HasColumnType("VARCHAR2(60)").HasColumnName("DIAG_DOCTOR");
            builder.Property(t => t.DiagTime).HasColumnType("DATE").HasColumnName("DIAG_TIME");
            builder.Property(t => t.DiagDesc).HasColumnType("VARCHAR2(100)").HasColumnName("DIAG_DESC");
            builder.Property(t => t.Complaint).HasColumnType("VARCHAR2(500)").HasColumnName("COMPLAINT");
            builder.Property(t => t.IsEmg).HasColumnType("VARCHAR2(10)").HasColumnName("IS_EMG");
            builder.Property(t => t.EmgBedNo).HasColumnType("VARCHAR2(60)").HasColumnName("EMG_BED_NO");
            builder.Property(t => t.TransferTime).HasColumnType("DATE").HasColumnName("TRANSFER_TIME");
            builder.Property(t => t.IsGreenChannel).HasColumnType("NUMBER(2)").HasColumnName("IS_GREEN_CHANNEL");
            builder.Property(t => t.Temp).HasColumnType("VARCHAR2(20)").HasColumnName("TEMP");
            builder.Property(t => t.Br).HasColumnType("VARCHAR2(20)").HasColumnName("BR");
            builder.Property(t => t.Plus).HasColumnType("VARCHAR2(20)").HasColumnName("PLUS");
            builder.Property(t => t.Hr).HasColumnType("VARCHAR2(20)").HasColumnName("HR");
            builder.Property(t => t.Bp).HasColumnType("VARCHAR2(20)").HasColumnName("BP");
            builder.Property(t => t.Spo2).HasColumnType("VARCHAR2(20)").HasColumnName("SPO2");
            builder.Property(t => t.InHosTime).HasColumnType("DATE").HasColumnName("IN_HOS_TIME");
            builder.Property(t => t.InDeptCode).HasColumnType("VARCHAR2(100)").HasColumnName("IN_DEPT_CODE");
            builder.Property(t => t.InWardCode).HasColumnType("VARCHAR2(20)").HasColumnName("IN_WARD_CODE");
            builder.Property(t => t.InHosDiagnosis).HasColumnType("VARCHAR2(1000)").HasColumnName("IN_HOS_DIAGNOSIS");
            builder.Property(t => t.InDeptTime).HasColumnType("DATE").HasColumnName("IN_DEPT_TIME");
            builder.Property(t => t.BedNo).HasColumnType("VARCHAR2(20)").HasColumnName("BED_NO");
            builder.Property(t => t.ToastFlag).HasColumnType("VARCHAR2(20)").HasColumnName("TOAST_FLAG");
            builder.Property(t => t.OutHosDiagnosis).HasColumnType("VARCHAR2(1000)").HasColumnName("OUT_HOS_DIAGNOSIS");
            builder.Property(t => t.OutDeptTime).HasColumnType("DATE").HasColumnName("OUT_DEPT_TIME");
            builder.Property(t => t.OutHosTime).HasColumnType("DATE").HasColumnName("OUT_HOS_TIME");
            builder.Property(t => t.HosDay).HasColumnType("VARCHAR2(20)").HasColumnName("HOS_DAY");
            builder.Property(t => t.TotalCost).HasColumnType("VARCHAR2(40)").HasColumnName("TOTAL_COST");
            builder.Property(t => t.DrugCost).HasColumnType("VARCHAR2(40)").HasColumnName("DRUG_COST");
            builder.Property(t => t.ExamCost).HasColumnType("VARCHAR2(40)").HasColumnName("EXAM_COST");
            builder.Property(t => t.OutWay).HasColumnType("VARCHAR2(200)").HasColumnName("OUT_WAY");
            builder.Property(t => t.IcuSense).HasColumnType("VARCHAR2(40)").HasColumnName("ICU_SENSE");
        }
    }
}