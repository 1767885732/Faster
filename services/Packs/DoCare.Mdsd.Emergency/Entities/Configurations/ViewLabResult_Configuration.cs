using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.Mdsd.Emergency.Entities.Configurations
{
    public class ViewLabResult_Configuration : EntityTypeConfigurationBase<ViewLabResult, string>
    {
        public override Type DbContextType => typeof(EmergencyDbContext);
        public override void Configure(EntityTypeBuilder<ViewLabResult> builder)
        {
            builder.HasKey(t => new { t.TEST_NO, t.REPORT_ITEM_CODE });
        }
    }
}
