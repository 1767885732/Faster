//------------------------------------------------------------------------------
//<copyright file="MedPatScore_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/22 17:05</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Score.Entities;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者评分信息数据信息
    /// </summary>
    public partial class MedPatScore_Configuration : EntityTypeConfigurationBase<MedPatScore, System.Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatScore> builder)
        {
            builder.ToTable("MED_PAT_SCORE");
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => new { t.PatientNo, t.ScoreType, t.ScoreSource, t.ScoreItem }).HasName("PNo_ST_SS_SI").IsUnique();
            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.ScoreType).HasColumnType("VARCHAR2(60)").HasColumnName("SCORE_TYPE");
            builder.Property(t => t.ScoreSource).HasColumnType("VARCHAR2(60)").HasColumnName("SCORE_SOURCE");
            builder.Property(t => t.ScoreItem).HasColumnType("VARCHAR2(60)").HasColumnName("SCORE_ITEM");
            builder.Property(t => t.ScoreResult).HasColumnType("VARCHAR2(60)").HasColumnName("SCORE_RESULT");
            builder.Property(t => t.ScoreDate).HasColumnType("DATE").HasColumnName("SCORE_DATE");
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(40)").HasColumnName("ID");
        }
    }
}