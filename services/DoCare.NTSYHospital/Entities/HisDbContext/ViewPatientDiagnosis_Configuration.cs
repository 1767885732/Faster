using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.NTSYHospital.Entities
{
    public class ViewPatientDiagnosis_Configuration : EntityTypeConfigurationBase<ViewPatientDiagnosis, string>
    {
        public override Type DbContextType => typeof(HisDbContext);
        public override void Configure(EntityTypeBuilder<ViewPatientDiagnosis> builder)
        {
        }
    }
}
