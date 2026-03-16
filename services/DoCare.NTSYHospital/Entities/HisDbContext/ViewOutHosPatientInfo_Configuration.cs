using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewOutHosPatientInfo_Configuration : EntityTypeConfigurationBase<ViewOutHosPatientInfo, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewOutHosPatientInfo> builder)
        {
        }
    }
}
