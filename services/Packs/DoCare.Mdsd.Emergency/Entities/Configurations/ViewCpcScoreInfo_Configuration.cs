using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Mdsd.Emergency.Entities.Configurations
{
    public class ViewCpcScoreInfo_Configuration : EntityTypeConfigurationBase<ViewCpcScoreInfo, string>
    {
        public override Type DbContextType => typeof(EmergencyDbContext);
        public override void Configure(EntityTypeBuilder<ViewCpcScoreInfo> builder)
        {
        }
    }
}
