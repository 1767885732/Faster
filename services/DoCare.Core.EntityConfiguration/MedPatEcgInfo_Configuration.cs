//------------------------------------------------------------------------------
//<copyright file="MedPatEcgInfo_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:30</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Ecg.Entities;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：心电数据表信息
    /// </summary>
    public partial class MedPatEcgInfo_Configuration : EntityTypeConfigurationBase<MedPatEcgInfo, Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatEcgInfo> builder)
        {
            builder.ToTable("MED_PAT_ECG_INFO");
            builder.HasKey(t => t.Id);
            //builder.HasIndex(t => new { t.PatientNo, t.ExamTime }).HasName("PatientExamEcg").IsUnique();

            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(40)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.ExamTime).HasColumnType("DATE").HasColumnName("EXAM_TIME");
            builder.Property(t => t.ReportTime).HasColumnType("DATE").HasColumnName("REPORT_TIME");
            builder.Property(t => t.DiagTime).HasColumnType("DATE").HasColumnName("DIAG_TIME");
            builder.Property(t => t.DiagDesc).HasColumnType("VARCHAR2(200)").HasColumnName("DIAG_DESC");
            builder.Property(t => t.DiagSource).HasColumnType("VARCHAR2(40)").HasColumnName("DIAG_SOURCE");
            builder.Property(t => t.EcgPath).HasColumnType("VARCHAR2(200)").HasColumnName("ECG_PATH");
            builder.Property(t => t.EcgUrl).HasColumnType("VARCHAR2(200)").HasColumnName("ECG_URL");
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnType("VARCHAR2(60)").HasColumnName("NAME");
            builder.Property(t => t.Sex).HasColumnType("VARCHAR2(60)").HasColumnName("SEX");
            builder.Property(t => t.Age).HasColumnType("VARCHAR2(60)").HasColumnName("AGE");
            builder.Property(t => t.EcgPatientId).HasColumnType("VARCHAR2(60)").HasColumnName("ECG_PATIENT_ID");
            builder.Property(t => t.EcgData).HasColumnType("CLOB").HasColumnName("ECG_DATA");
            builder.Property(t => t.PdfData).HasColumnType("CLOB").HasColumnName("PDF_DATA");
        }
    }
}