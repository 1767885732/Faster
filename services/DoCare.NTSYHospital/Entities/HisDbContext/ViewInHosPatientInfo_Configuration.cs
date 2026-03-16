using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewInHosPatientInfo_Configuration : EntityTypeConfigurationBase<ViewInHosPatientInfo, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewInHosPatientInfo> builder)
        {
        }
    }
}
