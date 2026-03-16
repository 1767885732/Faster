using DoCare.KJCHospital.Entities;
using FastFrame.Core;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.KJCHospital.Driver
{
    [Dependency(ServiceLifetime.Scoped)]
    public class EcgDriver
    {
        private readonly ILogger<EcgDriver> _logger;
        private readonly IRepository<ViewEcgInfo, string> viewEcgInfo;

        public EcgDriver(ILogger<EcgDriver> logger
            , IRepository<ViewEcgInfo, string> viewEcgInfo
            )
        {
            _logger = logger;
            this.viewEcgInfo = viewEcgInfo;
        }

        public async Task<List<ViewEcgInfo>> GetViewEcgInfo(string serialNo)
        {
            return viewEcgInfo.Query().Where(o => o.VISIT_ID == serialNo).ToList();
        }
    }
}
