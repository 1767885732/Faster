//------------------------------------------------------------------------------
//<copyright file="MedPatOperation_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:37</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Operation.Entities;


namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：手术表信息
    /// </summary>
    public partial class MedPatOperation_Configuration : EntityTypeConfigurationBase<MedPatOperation, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatOperation> builder)
        {
            builder.ToTable("MED_PAT_OPERATION");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(60)").HasColumnName("APPLY_NO");
            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.ScheduledDateTime).HasColumnType("DATE").HasColumnName("SCHEDULED_DATE_TIME");
            builder.Property(t => t.AnesthesiaMethod).HasColumnType("VARCHAR2(60)").HasColumnName("ANESTHESIA_METHOD");
            builder.Property(t => t.Surgeon).HasColumnType("VARCHAR2(60)").HasColumnName("SURGEON");
            builder.Property(t => t.AnesthesiaDoctor).HasColumnType("VARCHAR2(60)").HasColumnName("ANESTHESIA_DOCTOR");
            builder.Property(t => t.DiagBeforeOperation).HasColumnType("VARCHAR2(500)").HasColumnName("DIAG_BEFORE_OPERATION");
            builder.Property(t => t.DiagAfterOperation).HasColumnType("VARCHAR2(500)").HasColumnName("DIAG_AFTER_OPERATION");
            builder.Property(t => t.OperationScale).HasColumnType("VARCHAR2(60)").HasColumnName("OPERATION_SCALE");
            builder.Property(t => t.InDateTime).HasColumnType("DATE").HasColumnName("IN_DATE_TIME");
            builder.Property(t => t.OutDateTime).HasColumnType("DATE").HasColumnName("OUT_DATE_TIME");
            builder.Property(t => t.AnesStartTime).HasColumnType("DATE").HasColumnName("ANES_START_TIME");
            builder.Property(t => t.AnesEndTime).HasColumnType("DATE").HasColumnName("ANES_END_TIME");
            builder.Property(t => t.StartDateTime).HasColumnType("DATE").HasColumnName("START_DATE_TIME");
            builder.Property(t => t.EndDateTime).HasColumnType("DATE").HasColumnName("END_DATE_TIME");
            builder.Property(t => t.InPacuDateTime).HasColumnType("DATE").HasColumnName("IN_PACU_DATE_TIME");
            builder.Property(t => t.OutPacuDateTime).HasColumnType("DATE").HasColumnName("OUT_PACU_DATE_TIME");
            builder.Property(t => t.BloodLossed).HasColumnType("NUMBER(6)").HasColumnName("BLOOD_LOSSED");
            builder.Property(t => t.BloodTransfered).HasColumnType("NUMBER(6)").HasColumnName("BLOOD_TRANSFERED");
            builder.Property(t => t.OperationName).HasColumnType("VARCHAR2(200)").HasColumnName("OPERATION_NAME");
            builder.Property(t => t.OperationPart).HasColumnType("VARCHAR2(60)").HasColumnName("OPERATION_PART");
            builder.Property(t => t.OperNurse).HasColumnType("VARCHAR2(60)").HasColumnName("OPER_NURSE");
            builder.Property(t => t.SupplyNurse).HasColumnType("VARCHAR2(60)").HasColumnName("SUPPLY_NURSE");
        }
    }
}