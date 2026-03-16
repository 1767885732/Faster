//------------------------------------------------------------------------------
//<copyright file="MedLabResult_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/14 09:05</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.Exam_Inspec_Medication.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者检验报告信息信息
    /// </summary>
    public partial class MedLabResult_Configuration : EntityTypeConfigurationBase<MedLabResult, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedLabResult> builder)
        {
            builder.ToTable("MED_LAB_RESULT");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(40)").HasColumnName("ITEM_NO");
            builder.HasIndex(t => new { t.TestNo, t.Id }).HasName("TESTNO_ITEMNO").IsUnique();
            builder.Property(t => t.TestNo).HasColumnType("VARCHAR2(40)").HasColumnName("TEST_NO");
            //builder.Property(t => t.ItemNo).HasColumnType("VARCHAR2(40)").HasColumnName("ITEM_NO");
            builder.Property(t => t.ReportItemName).HasColumnType("VARCHAR2(100)").HasColumnName("REPORT_ITEM_NAME");
            builder.Property(t => t.ReportItemCode).HasColumnType("VARCHAR2(60)").HasColumnName("REPORT_ITEM_CODE");
            builder.Property(t => t.AbnormalIndicator).HasColumnType("VARCHAR2(60)").HasColumnName("ABNORMAL_INDICATOR");
            builder.Property(t => t.Result).HasColumnType("VARCHAR2(200)").HasColumnName("RESULT");
            builder.Property(t => t.Units).HasColumnType("VARCHAR2(20)").HasColumnName("UNITS");
            builder.Property(t => t.ResultDateTime).HasColumnType("DATE").HasColumnName("RESULT_DATE_TIME");
            builder.Property(t => t.ReferenceResult).HasColumnType("VARCHAR2(500)").HasColumnName("REFERENCE_RESULT");
        }
    }
}
