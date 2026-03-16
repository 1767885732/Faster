using DoCare.Core.Patient.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Core.EntityConfiguration
{
    public partial class MedPatBloodGasDetail_Configuration : EntityTypeConfigurationBase<MedPatBloodGasDetail, Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<MedPatBloodGasDetail> builder)
        {
            builder.ToTable("MED_PAT_BLOOD_GAS_DETAIL");
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => new { t.PatientNo, t.ReportTime, t.TestId }).HasName("PATIENTNO_REPORTTIME_TESTID").IsUnique();
            builder.Property(t => t.Id).HasColumnType("VARCHAR2(40)").HasColumnName("ID");
            builder.Property(t => t.PatientNo).HasColumnType("VARCHAR2(60)").HasColumnName("PATIENT_NO");
            builder.Property(t => t.BloodTime).HasColumnType("DATE").HasColumnName("BLOOD_TIME");
            builder.Property(t => t.ReportTime).HasColumnType("DATE").HasColumnName("REPORT_TIME");
            builder.Property(t => t.TestId).HasColumnType("VARCHAR2(100)").HasColumnName("TEST_ID");
            builder.Property(t => t.TestName).HasColumnType("VARCHAR2(100)").HasColumnName("TEST_NAME");
            builder.Property(t => t.TestValue).HasColumnType("VARCHAR2(100)").HasColumnName("TEST_VALUE");
            builder.Property(t => t.Unit).HasColumnType("VARCHAR2(100)").HasColumnName("UNIT");
            builder.Property(t => t.Conclusion).HasColumnType("VARCHAR2(100)").HasColumnName("CONCLUSION");
        }
    }
}
