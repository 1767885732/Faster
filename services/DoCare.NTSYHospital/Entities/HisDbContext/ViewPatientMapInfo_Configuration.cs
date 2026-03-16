using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewPatientMapInfo_Configuration : EntityTypeConfigurationBase<ViewPatientMapInfo, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewPatientMapInfo> builder)
        {
        }
    }
}
