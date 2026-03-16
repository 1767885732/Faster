using DoCare.Core.Common;
using DoCare.Core.Common.Dtos;
using DoCare.Core.Common.Events;
using DoCare.Core.Common.Notify;
using DoCare.Core.Patient;
using DoCare.Core.TimeNode;
using FastFrame.AspNetCore.Mvc.Filters;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.EventBuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    /// <summary>
    /// 推送服务
    /// </summary>
    public class NotifyController : DoCareApiController
    {
        private readonly INotify notify;
        private readonly PatientService patientService;
        private readonly TimeNodeService timeNodeService;
        private readonly IEventBus eventBus;
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public NotifyController(INotify notify, PatientService patientService,
            TimeNodeService timeNodeService, IEventBus eventBus, IUnitOfWorkManager unitOfWorkManager)
        {
            this.notify = notify;
            this.patientService = patientService;
            this.timeNodeService = timeNodeService;
            this.eventBus = eventBus;
            this.unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 患者推送服务
        /// </summary>
        /// <param name="dto">推送服务参数</param>
        /// <returns>推送结果</returns>
        [HttpPost]
        public async Task<DoCareResponse<string>> CreatePatientInfo(CreatePatientInfo dto)
        {
            DoCareResponse<string> result = null;
            try
            {
                Logger.LogError($"同步关系数据开始...");
                var pSource = dto.PatientSource.ToEnum(PatientSourceType.Emergency);
                var app = dto.PatientType.ToEnum(AppType.No);
                var relation = await patientService.Save(dto.ClinicNo, pSource, dto.PatientId);
                unitOfWorkManager.Commit();

                Logger.LogError($"推送专科中心开始...");
                var pushPatientEventData = new PushPatientEventData() { App = app, PatientNo = relation.Id };
                eventBus.PublishAsync(pushPatientEventData);
                result = Success<string>(relation.Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<string>();
            }
            return result;
        }

        /// <summary>
        /// 患者推送服务
        /// </summary>
        /// <param name="dto">推送服务参数</param>
        /// <returns>推送结果</returns>
        [HttpPost]
        public async Task<DoCareResponse<string>> PullNotify(SyncNotifyDto dto)
        {
            DoCareResponse<string> result = null;
            try
            {
                Logger.LogDebug("开始执行PullNotify...");
                var pSource = dto.PatientSource.ToEnum(PatientSourceType.Emergency);
                var app = dto.App.ToEnum(AppType.No);
                var relation = await patientService.Save(dto.PId, pSource, dto.PatientId);
                unitOfWorkManager.Commit();

                Logger.LogDebug($"推送专科中心开始...");
                var pushPatientEventData = new PushPatientEventData() { App = app, PatientNo = relation.Id };
                eventBus.PublishAsync(pushPatientEventData);
                result = Success<string>(relation.Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<string>();
            }
            return result;
        }

        /// <summary>
        /// 更新患者基本数据
        /// </summary>
        /// <param name="dto">根据患者编号更新患者基本信息</param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncPatientBasicInfo(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await patientService.SyncPatientBasicInfo(dto.PatientNo);
                result = Success<bool>(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<bool>();
            }
            return result;
        }


        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncPatientId(UpdatePIdDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await patientService.SyncPatientId(dto.PaientId,dto.ClinicNo);
                result = Success<bool>(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<bool>();
            }
            return result;
        }


        /// <summary>
        /// 更新患者腕带数据
        /// </summary>
        /// <param name="dto">根据患者编号更新患者信息</param>
        /// <returns>返回是否成功</returns>
        [HttpPost]
        //[ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncPatientPatTimeInfo(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await timeNodeService.RunAsync(dto.PatientNo);
                result = Success<bool>(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<bool>();
            }
            return result;
        }

    }
}