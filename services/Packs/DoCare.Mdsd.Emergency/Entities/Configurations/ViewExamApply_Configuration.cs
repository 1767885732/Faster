using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Mdsd.Emergency.Entities.Configurations
{
    public class ViewExamApply_Configuration : EntityTypeConfigurationBase<ViewExamApply, string>
    {
        public override Type DbContextType => typeof(EmergencyDbContext);
        public override void Configure(EntityTypeBuilder<ViewExamApply> builder)
        {
        }
    }
}
