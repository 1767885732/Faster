//------------------------------------------------------------------------------
//<copyright file="MedLabMasterConfiguration.generated.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 15:05</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.Exam_Inspec_Medication.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者检验申请单信息信息
    /// </summary>
    public partial class MedLabMaster_Configuration : EntityTypeConfigurationBase<MedLabMaster, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedLabMaster> builder)
        {
            builder.ToTable("MED_LAB_MASTER");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("TEST_NO");
            builder.Property(t => t.PriorityIndicator).HasColumnType("VARCHAR2(40)").HasColumnName("PRIORITY_INDICATOR");
            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(40)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.Name).HasColumnType("VARCHAR2(40)").HasColumnName("NAME");
            builder.Property(t => t.TestCause).HasColumnType("VARCHAR2(200)").HasColumnName("TEST_CAUSE");
            builder.Property(t => t.RelevantClinicDiag).HasColumnType("VARCHAR2(200)").HasColumnName("RELEVANT_CLINIC_DIAG");
            builder.Property(t => t.Specimen).HasColumnType("VARCHAR2(200)").HasColumnName("SPECIMEN");
            builder.Property(t => t.SpcmReceivedDateTime).HasColumnType("DATE").HasColumnName("SPCM_RECEIVED_DATE_TIME");
            builder.Property(t => t.ApplyTime).HasColumnType("DATE").HasColumnName("APPLY_TIME");
            builder.Property(t => t.OrderingDept).HasColumnType("VARCHAR2(40)").HasColumnName("ORDERING_DEPT");
            builder.Property(t => t.OrderingProvider).HasColumnType("VARCHAR2(40)").HasColumnName("ORDERING_PROVIDER");
            builder.Property(t => t.PerformedBy).HasColumnType("VARCHAR2(40)").HasColumnName("PERFORMED_BY");
            builder.Property(t => t.ResultStatus).HasColumnType("VARCHAR2(40)").HasColumnName("RESULT_STATUS");
            builder.Property(t => t.ResultRptDateTime).HasColumnType("DATE").HasColumnName("RESULTS_RPT_DATE_TIME");
            builder.Property(t => t.Transcriptionist).HasColumnType("VARCHAR2(40)").HasColumnName("TRANSCRIPTIONIST");
            builder.Property(t => t.VerifiedBy).HasColumnType("VARCHAR2(40)").HasColumnName("VERIFIED_BY");
        }
    }
}