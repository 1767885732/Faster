using DoCare.KJCHospital.DbContext;
using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DoCare.KJCHospital.Entities.Configurations
{
    public class ViewEcgInfo_Configuration : EntityTypeConfigurationBase<ViewEcgInfo, string>
    {
        public override Type DbContextType => typeof(EcgDbContext);
        public override void Configure(EntityTypeBuilder<ViewEcgInfo> builder)
        {
            builder.HasKey(t => new { t.VISIT_ID, t.EXAM_TIME });
        }
    }
}
