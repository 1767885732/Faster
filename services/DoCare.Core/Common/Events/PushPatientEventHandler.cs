using DoCare.Core.Common.Notify;
using DoCare.Core.Patient.Entities;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.EventBuses;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DoCare.Core.Common.Events
{
    public class PushPatientEventHandler : EventHandlerBase<PushPatientEventData>
    {
        private readonly IRepository<MedPatRelationInfo, string> medPatRelationInfo;
        private readonly ILogger<PushPatientEventHandler> logger;
        private readonly INotify notify;

        public PushPatientEventHandler(ILogger<PushPatientEventHandler> logger,
            IRepository<MedPatRelationInfo, string> medPatRelationInfo,
            INotify notify)
        {
            this.logger = logger;
            this.medPatRelationInfo = medPatRelationInfo;
            this.notify = notify;
        }

        public override void Handle(PushPatientEventData eventData)
        {
           
        }

        /// <summary>
        /// 推送患者事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public override async Task HandleAsync(PushPatientEventData eventData, CancellationToken cancelToken = default(CancellationToken))
        {
            Check.NotNullOrEmpty(eventData.PatientNo, nameof(eventData.PatientNo));
            if (eventData.App == AppType.No) return;
            logger.LogDebug($"患者推送:{eventData.PatientNo}，开始");

            await notify.RunAsync(eventData.PatientNo, eventData.App);

            logger.LogDebug($"患者推送:{eventData.PatientNo},成功");
        }
    }
}
