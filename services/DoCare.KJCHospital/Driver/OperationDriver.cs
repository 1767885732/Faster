using DoCare.KJCHospital.Entities;
using FastFrame.Core;
using FastFrame.EFCore;
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
    public class OperationDriver
    {
        private readonly ILogger<OperationDriver> _logger;

        private readonly IRepository<ViewPatOperation, string> _viewPatOperation;

        public OperationDriver(ILogger<OperationDriver> logger
            , IRepository<ViewPatOperation, string> viewPatOperation)
        {
            _logger = logger;
            _viewPatOperation = viewPatOperation;
        }

        /// <summary>
        /// 获取手术信息
        /// </summary>
        /// <param name="inpNo"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ViewPatOperation>> GetOperationList(string inpNo, DateTime startTime, DateTime endTime)
        {
            return _viewPatOperation.Query().Where(o => o.PATIENT_ID == inpNo && o.InDateTime >= startTime && o.InDateTime < endTime).ToList();
        }
    }
}
