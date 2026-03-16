using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.KJCHospital.Entities;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Core.EntityConfiguration
{
    public class ViewOutHosPatientInfo_Configuration : EntityTypeConfigurationBase<ViewOutHosPatientInfo, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        public override void Configure(EntityTypeBuilder<ViewOutHosPatientInfo> builder)
        {
        }
    }
}
