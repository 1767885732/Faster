using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Mdsd.Emergency.Entities.Configurations
{
    public class ViewCpcPatientInfo_Configuration : EntityTypeConfigurationBase<ViewCpcPatientInfo, string>
    {
        public override Type DbContextType => typeof(EmergencyDbContext);
        public override void Configure(EntityTypeBuilder<ViewCpcPatientInfo> builder)
        {
        }
    }
}
