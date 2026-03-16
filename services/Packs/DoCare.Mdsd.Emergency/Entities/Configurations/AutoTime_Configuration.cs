using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Mdsd.Emergency.Entities.Configurations
{
    public class AutoTime_Configuration : EntityTypeConfigurationBase<AutoTime,string>
    {
        public override Type DbContextType => typeof(EmergencyDbContext);
        public override void Configure(EntityTypeBuilder<AutoTime> builder)
        {
            builder.HasKey(a => new { a.Id });
        }
    }
}
