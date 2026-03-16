//------------------------------------------------------------------------------
//<copyright file="MedPatOrders_Configuration.cs" company="麦迪科技">
//        Copyright (c) 2014-2019 FastFrame. All rights reserved.
//       </copyright>
//  <last-editor>陈明明</last-editor>
//  <last-date>2019/03/13 16:34</last-date>
// -----------------------------------------------------------------------

using FastFrame.EFCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DoCare.Core.Exam_Inspec_Medication.Entities;

namespace DoCare.Core.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者医嘱用药信息信息
    /// </summary>
    public partial class MedPatOrders_Configuration : EntityTypeConfigurationBase<MedPatOrders, Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatOrders> builder)
        {
            builder.ToTable("MED_PAT_ORDERS");
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => new { t.PatientNo, t.OrderNo, t.OrderSubNo }).HasName("PatientNo_OrderNo_SubNo").IsUnique();

            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.OrderNo).HasColumnType("VARCHAR2(60)").HasColumnName("ORDER_NO");
            builder.Property(t => t.OrderSubNo).HasColumnType("VARCHAR2(60)").HasColumnName("ORDER_SUB_NO");
            builder.Property(t => t.ClinicNo).HasColumnType("VARCHAR2(60)").HasColumnName("CLINIC_NO");
            builder.Property(t => t.InpNo).HasColumnType("VARCHAR2(60)").HasColumnName("INP_NO");
            builder.Property(t => t.RepeatIndicator).HasColumnType("VARCHAR2(60)").HasColumnName("REPEAT_INDICATOR");
            builder.Property(t => t.OrderClass).HasColumnType("VARCHAR2(60)").HasColumnName("ORDER_CLASS");
            builder.Property(t => t.OrderText).HasColumnType("VARCHAR2(200)").HasColumnName("ORDER_TEXT");
            builder.Property(t => t.OrderCode).HasColumnType("VARCHAR2(60)").HasColumnName("ORDER_CODE");
            builder.Property(t => t.Dosage).HasColumnType("VARCHAR2(60)").HasColumnName("DOSAGE");
            builder.Property(t => t.DosageUnits).HasColumnType("VARCHAR2(60)").HasColumnName("DOSAGE_UNITS");
            builder.Property(t => t.Administration).HasColumnType("VARCHAR2(60)").HasColumnName("ADMINISTRATION");
            builder.Property(t => t.StartDateTime).HasColumnType("DATE").HasColumnName("START_DATE_TIME");
            builder.Property(t => t.StopDateTime).HasColumnType("DATE").HasColumnName("STOP_DATE_TIME");
            builder.Property(t => t.Duration).HasColumnType("DATE").HasColumnName("DURATION");
            builder.Property(t => t.DurationUnits).HasColumnType("VARCHAR2(60)").HasColumnName("DURATION_UNITS");
            builder.Property(t => t.Frequency).HasColumnType("VARCHAR2(60)").HasColumnName("FREQUENCY");
            builder.Property(t => t.OrderingDept).HasColumnType("VARCHAR2(60)").HasColumnName("ORDERING_DEPT");
            builder.Property(t => t.Doctor).HasColumnType("VARCHAR2(60)").HasColumnName("DOCTOR");
            builder.Property(t => t.StopDoctor).HasColumnType("VARCHAR2(60)").HasColumnName("STOP_DOCTOR");
            builder.Property(t => t.Nurse).HasColumnType("VARCHAR2(60)").HasColumnName("NURSE");
            builder.Property(t => t.StopNurse).HasColumnType("VARCHAR2(60)").HasColumnName("STOP_NURSE");
            builder.Property(t => t.EnterDateTime).HasColumnType("DATE").HasColumnName("ENTER_DATE_TIME");
            builder.Property(t => t.StopOrderDateTime).HasColumnType("DATE").HasColumnName("STOP_ORDER_DATE_TIME");
            builder.Property(t => t.OrderStatus).HasColumnType("VARCHAR2(60)").HasColumnName("ORDER_STATUS");
            builder.Property(t => t.CurrentPrescNo).HasColumnType("VARCHAR2(60)").HasColumnName("CURRENT_PRESC_NO");
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(40)").HasColumnName("ID");
        }
    }
}