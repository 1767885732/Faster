//------------------------------------------------------------------------------
//<copyright file="MedExamReport_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:39</last-date>
// -----------------------------------------------------------------------

using DoCare.Core.TimeNode.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者检查报告信息信息
    /// </summary>
    public partial class MedPatTimeNodes_Configuration : EntityTypeConfigurationBase<MedPatTimeNodes, Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatTimeNodes> builder)
        {
            builder.ToTable("MED_PAT_TIMENODES");
            builder.HasKey(t => t.Id);
            //builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.HasIndex(t => new { t.PatientNo, t.ItemName, t.TagId }).HasName("PatientNo_ItemName_TagId").IsUnique();
            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.TagId).HasColumnType("VARCHAR2(40)").HasColumnName("TAG_ID");
            builder.Property(t => t.ItemName).HasColumnType("VARCHAR2(60)").HasColumnName("ITEM_NAME");
            builder.Property(t => t.ItemValue).HasColumnType("date").HasColumnName("ITEM_VALUE");
            builder.Property(t => t.BindTime).HasColumnType("date").HasColumnName("BIND_TIME");
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(40)").HasColumnName("ID");
        }
    }
}
