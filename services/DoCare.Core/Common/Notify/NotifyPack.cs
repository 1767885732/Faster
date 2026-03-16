using DoCare.Core.Common.Notify.SpeciaList;
using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DoCare.Core.Common.Notify
{
    /// <summary>
    /// 推送通知
    /// </summary>
    public class NotifyPack : FastPack
    {
        public override PackLevel Level => PackLevel.Business;

        public override int Order => 2;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddHttpClient<ChestPainApiClient>();
            services.AddHttpClient<StrokeApiClient>();
            services.AddHttpClient<WoundApiClient>();
            services.AddHttpClient<NewbornApiClient>();
            services.AddHttpClient<PuerperaApiClient>();
            services.TryAddScoped<INotify, NotifyServiceBase>();
            return services;
        }
    }
}
