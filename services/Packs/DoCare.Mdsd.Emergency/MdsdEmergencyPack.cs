using DoCare.Core;
using DoCare.Mdsd.Emergency;
using FastFrame.Core;
using FastFrame.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoCare.Mdsd.Emergency
{
    public class MdsdEmergencyPack : FastPack
    {
        public override PackLevel Level => PackLevel.Business;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IEmergency, EmergencyService>();
            services.AddFastFrameDbContext<EmergencyDbContext>();
            services.AddFastFrameDbContext<EmergencyTagDbContext>();
            return services;
        }
    }
}
