using DoCare.KJCHospital.DbContext;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.KJCHospital.Entities.Configurations
{
    public class ViewPatOperation_Configuration : EntityTypeConfigurationBase<ViewPatOperation, string>
    {
        public override Type DbContextType => typeof(OperationDbContext);

        public override void Configure(EntityTypeBuilder<ViewPatOperation> builder)
        {
        }
    }
}
