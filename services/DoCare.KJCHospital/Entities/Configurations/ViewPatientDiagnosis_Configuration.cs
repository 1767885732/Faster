using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.KJCHospital.Entities.Configurations
{
    public class ViewPatientDiagnosis_Configuration : EntityTypeConfigurationBase<ViewPatientDiagnosis, string>
    {
        public override Type DbContextType => typeof(DefaultDbContext);
        public override void Configure(EntityTypeBuilder<ViewPatientDiagnosis> builder)
        {
        }
    }
}
