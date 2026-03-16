using DoCare.Core.Common.Notify.SpeciaList;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.Core.Common.Notify
{
    public class NotifyServiceBase : INotify
    {
        private readonly ILogger<NotifyServiceBase> logger;

        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IConfigureOptions<FastFrameOptions> fastFrameOptions;
        private readonly ChestPainApiClient chestPainApiClient;
        private readonly StrokeApiClient strokeApiClient;
        private readonly WoundApiClient woundApiClient;
        private readonly PuerperaApiClient puerperaApiClient;
        private readonly NewbornApiClient newbornApiClient;

        public NotifyServiceBase(ILogger<NotifyServiceBase> logger,
            IConfigureOptions<FastFrameOptions> fastFrameOptions,
            IRepository<MedPatientRegistInfo, string> medPatientRegistInfo,
            ChestPainApiClient chestPainApiClient,
            StrokeApiClient strokeApiClient,
            WoundApiClient woundApiClient,
            PuerperaApiClient puerperaApiClient,
            NewbornApiClient newbornApiClient)
        {
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.fastFrameOptions = fastFrameOptions;
            this.chestPainApiClient = chestPainApiClient;
            this.strokeApiClient = strokeApiClient;
            this.woundApiClient = woundApiClient;
            this.puerperaApiClient = puerperaApiClient;
            this.newbornApiClient = newbornApiClient;
            this.logger = logger;
        }

        public async Task RunAsync(string patientNo, AppType app)
        {
            logger.LogDebug($"开始推送,病历号:{patientNo},专科:{app.ToDescription()}");

            if (app == AppType.No) return;

            switch (app)
            {
                case AppType.ChestPain:
                    await PushToChestPain(patientNo);
                    break;
                case AppType.Stroke:
                    await PushToStroke(patientNo);
                    break;
                case AppType.Wound:
                    await PushToWound(patientNo);
                    break;
                case AppType.Newborn:
                    await PushToNewborn(patientNo);
                    break;
                case AppType.Puerpera:
                    await PushToPuerpera(patientNo);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 推送至胸痛
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        protected virtual async Task PushToChestPain(string patientNo)
        {
            //获取数据（接口）
            var route = "/Notify/PushPatient";
            var parameter = new
            {
                patientNo = patientNo
            };

            var requestUri = $"{chestPainApiClient.ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await chestPainApiClient.ApiClient.PostAsync(requestUri, parameter);
        }

        /// <summary>
        /// 推送至卒中
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        protected virtual async Task PushToStroke(string patientNo)
        {
            var route = "/Notify/PushPatient";
            var parameter = new
            {
                patientNo = patientNo,
            };

            var requestUri = $"{strokeApiClient.ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await strokeApiClient.ApiClient.PostAsync(requestUri, parameter);
        }

        /// <summary>
        /// 推送至创伤
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        protected virtual async Task PushToWound(string patientNo)
        {
            var route = "/Synchronous/SynchronousPatient";
            var parameter = new
            {
                patientNo = patientNo
            };

            var requestUri = $"{woundApiClient.ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await woundApiClient.ApiClient.PostAsync(requestUri, parameter);
        }

        /// <summary>
        /// 推送至新生儿
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        protected virtual async Task PushToNewborn(string patientNo)
        {
            var route = "/Notify/PushPatient";
            var parameter = new
            {
                patientNo = patientNo
            };

            var requestUri = $"{newbornApiClient.ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await newbornApiClient.ApiClient.PostAsync(requestUri, parameter);
        }



        /// <summary>
        /// 推送至孕产妇
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        protected virtual async Task PushToPuerpera(string patientNo)
        {
            //获取数据（接口）
            var route = "/Notify/PushPatient";
            var parameter = new
            {
                patientNo = patientNo
            };

            var requestUri = $"{puerperaApiClient.ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await puerperaApiClient.ApiClient.PostAsync(requestUri, parameter);
        }
    }
}
