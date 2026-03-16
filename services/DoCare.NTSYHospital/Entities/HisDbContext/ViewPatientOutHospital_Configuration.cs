using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewPatientOutHospital_Configuration : EntityTypeConfigurationBase<ViewPatientOutHospital, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewPatientOutHospital> builder)
        {
        }
    }
}
