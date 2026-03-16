using DoCare.Core;
using FastFrame.Core;
using FastFrame.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoCare.NTSYHospital
{
    public class HospitalPack : FastPack
    {
        public override PackLevel Level => PackLevel.Business;

        public override int Order => 2;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddFastFrameDbContext<HisDbContext>();
            services.AddFastFrameDbContext<LisDbContext>();
            //急救
            services.AddScoped<IFirstAid, FirstAidService>();
            //门诊
            services.AddScoped<IOutPatient, OutPatientService>();
            //住院
            services.AddScoped<IInPatient, InPatientService>();
            return services;
        }
    }
}
