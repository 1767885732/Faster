using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewLabMaster_Configuration : EntityTypeConfigurationBase<ViewLabMaster, string>
    {
        public override Type DbContextType => typeof(LisDbContext);
        public override void Configure(EntityTypeBuilder<ViewLabMaster> builder)
        {
        }
    }
}
