using DoCare.Core;
using DoCare.Mdsd.FirstAid.Driver;
using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DoCare.Mdsd.FirstAid
{
    public class MdsdFirstAidPack : FastPack
    {
        public override PackLevel Level => PackLevel.Business;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            ////院前驱动
            services.AddHttpClient<FirstAidApiClient>();
            services.AddHttpClient<FirstAidEcgClient>();

            //急救
            services.AddScoped<IFirstAid, FirstAidService>();
            return services;
        }
    }
}
