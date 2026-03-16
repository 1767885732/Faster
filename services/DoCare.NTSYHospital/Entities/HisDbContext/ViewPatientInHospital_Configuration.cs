using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewPatientInHospital_Configuration : EntityTypeConfigurationBase<ViewPatientInHospital, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewPatientInHospital> builder)
        {
        }
    }
}
