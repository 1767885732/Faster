using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewPatientOrders_Configuration : EntityTypeConfigurationBase<ViewPatientOrders, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewPatientOrders> builder)
        {
            builder.HasKey(t => new { t.PRESCRIPTION_NO });
        }
    }
}
