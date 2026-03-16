using DoCare.Core.Patient.Entities;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.EventBuses;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DoCare.Core.Patient.Events
{
    public class SyncPatientRelationEventHandler : EventHandlerBase<SyncPatientRelationEventData>
    {
        private readonly IRepository<MedPatRelationInfo, string> medPatRelationInfo;
        private readonly ILogger<SyncPatientRelationEventHandler> logger;

        public SyncPatientRelationEventHandler(ILogger<SyncPatientRelationEventHandler> logger,
            IRepository<MedPatRelationInfo, string> medPatRelationInfo
            )
        {
            this.logger = logger;
            this.medPatRelationInfo = medPatRelationInfo;
        }

        public override void Handle(SyncPatientRelationEventData eventData)
        {

        }

        /// <summary>
        /// 推送患者事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public override async Task HandleAsync(SyncPatientRelationEventData eventData, CancellationToken cancelToken = default(CancellationToken))
        {
            Check.NotNullOrEmpty(eventData.PatientNo, nameof(eventData.PatientNo));
            
            logger.LogDebug($"患者推送:{eventData.PatientNo}，开始");


            logger.LogDebug($"患者推送:{eventData.PatientNo},成功");
        }
    }
}
