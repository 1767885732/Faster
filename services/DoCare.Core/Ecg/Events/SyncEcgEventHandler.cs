using DoCare.Core.Ecg;
using DoCare.Core.Patient.Entities;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.EventBuses;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DoCare.Core.Patient.Events
{
    public class SyncEcgEventHandler : EventHandlerBase<SyncEcgEventData>
    {
        private readonly IRepository<MedPatRelationInfo, string> medPatRelationInfo;
        private readonly ILogger<SyncEcgEventHandler> logger;
        private readonly EcgService ecgService;

        public SyncEcgEventHandler(ILogger<SyncEcgEventHandler> logger,
            IRepository<MedPatRelationInfo, string> medPatRelationInfo,
            EcgService ecgService)
        {
            this.logger = logger;
            this.medPatRelationInfo = medPatRelationInfo;
            this.ecgService = ecgService;
        }

        public override void Handle(SyncEcgEventData eventData)
        {

        }

        /// <summary>
        /// 同步心电图数据
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public override async Task HandleAsync(SyncEcgEventData eventData, CancellationToken cancelToken = default(CancellationToken))
        {
            Check.NotNullOrEmpty(eventData.Dto.PatientNo, nameof(eventData.Dto.PatientNo));
            logger.LogDebug($"同步心电图:{eventData.Dto.PatientNo}，开始");
            await ecgService.SyncEcgs(eventData.Dto);
            logger.LogDebug($"同步心电图结束");
        }
    }
}
