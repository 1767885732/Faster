//------------------------------------------------------------------------------
//<copyright file="MedExamReport_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:39</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.Exam_Inspec_Medication.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者检查报告信息信息
    /// </summary>
    public partial class MedExamReport_Configuration : EntityTypeConfigurationBase<MedExamReport, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedExamReport> builder)
        {
            builder.ToTable("MED_EXAM_REPORT");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("EXAM_NO");
            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.ExamClass).HasColumnType("VARCHAR2(100)").HasColumnName("EXAM_CLASS");
            builder.Property(t => t.ExamSubClass).HasColumnType("VARCHAR2(100)").HasColumnName("EXAM_SUB_CLASS");
            builder.Property(t => t.ClinDiag).HasColumnType("VARCHAR2(200)").HasColumnName("CLIN_DIAG");
            builder.Property(t => t.ExamMode).HasColumnType("VARCHAR2(100)").HasColumnName("EXAM_MODE");
            builder.Property(t => t.Device).HasColumnType("VARCHAR2(60)").HasColumnName("DEVICE");
            builder.Property(t => t.PerformedBy).HasColumnType("VARCHAR2(60)").HasColumnName("PERFORMED_BY");
            builder.Property(t => t.ReqDateTime).HasColumnType("DATE").HasColumnName("REQ_DATE_TIME");
            builder.Property(t => t.ReqDept).HasColumnType("VARCHAR2(60)").HasColumnName("REQ_DEPT");
            builder.Property(t => t.ReqPhysician).HasColumnType("VARCHAR2(60)").HasColumnName("REQ_PHYSICIAN");
            builder.Property(t => t.ReqMemo).HasColumnType("VARCHAR2(100)").HasColumnName("REQ_MEMO");
            builder.Property(t => t.Notice).HasColumnType("VARCHAR2(60)").HasColumnName("NOTICE");
            builder.Property(t => t.ExamStartDate).HasColumnType("DATE").HasColumnName("EXAM_START_DATE");
            builder.Property(t => t.ExamEndDate).HasColumnType("DATE").HasColumnName("EXAM_END_DATE");
            builder.Property(t => t.ReportDateTime).HasColumnType("DATE").HasColumnName("REPORT_DATE_TIME");
            builder.Property(t => t.ExamPara).HasColumnType("VARCHAR2(60)").HasColumnName("EXAM_PARA");
            builder.Property(t => t.Description).HasColumnType("VARCHAR2(500)").HasColumnName("DESCRIPTION");
            builder.Property(t => t.Recommendation).HasColumnType("VARCHAR2(1000)").HasColumnName("RECOMMENDATION");
            builder.Property(t => t.Technician).HasColumnType("VARCHAR2(60)").HasColumnName("TECHNICIAN");
            builder.Property(t => t.Reporter).HasColumnType("VARCHAR2(60)").HasColumnName("REPORTER");
            builder.Property(t => t.ResultStatus).HasColumnType("VARCHAR2(60)").HasColumnName("RESULT_STATUS");
            builder.Property(t => t.VerifiedBy).HasColumnType("VARCHAR2(60)").HasColumnName("VERIFIED_BY");
            builder.Property(t => t.VerifiedDateTime).HasColumnType("DATE").HasColumnName("VERIFIED_DATE_TIME");
        }
    }
}
