using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DoCare.EcgConversion
{
    public class EcgConversionPack : FastPack
    {
        public override PackLevel Level => PackLevel.Application;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //心电图转换驱动
            services.AddScoped<IEcgDriver, aEcg.AEcgDriver>();
            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override async void UsePack(IServiceProvider provider)
        {
            EcgMonitorOption ecgMonitorOption = provider.GetFastFrameOptions()?.EcgMonitor;
            if (ecgMonitorOption != null && ecgMonitorOption.Enabled)
            {
                ILogger<EcgConversionPack> logger = provider.GetLogger<EcgConversionPack>();

                if (string.IsNullOrWhiteSpace(ecgMonitorOption.DirectoryPath)
                || string.IsNullOrWhiteSpace(ecgMonitorOption.FileFilter)
                || ecgMonitorOption.DelayInterval <= 0)
                {
                    logger.LogError($"心电图监视配置错误");
                    return;
                }

                using (var scope = provider.CreateScope())
                {
                    var p = scope.ServiceProvider;

                    var monitorService = p.GetService<EcgMonitorService>();
                    if (monitorService.CheckDirectory(ecgMonitorOption.DirectoryPath))
                    {
                        bool result = await monitorService.TurnOnMonitor(ecgMonitorOption.DirectoryPath, ecgMonitorOption.FileFilter, ecgMonitorOption.DelayInterval);
                        logger.LogInformation($"心电图监视模块启用【{(result ? "成功" : "失败")}】");
                    }
                    else
                    {
                        logger.LogError($"心电图监视目录不存在");
                    }
                }
            }
            base.UsePack(provider);
        }
    }
}
