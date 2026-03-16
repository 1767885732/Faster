using DoCare.Core.Common;
using DoCare.Core.Ecg;
using DoCare.Core.Ecg.Entities.XmlSerialization;
using DoCare.EcgConversion;
using FastFrame.AspNetCore.Mvc.Filters;
using FastFrame.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    public class EcgController : DoCareApiController
    {
        private readonly EcgService ecgService;
        private readonly EcgConversionService ecgConversionService;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        public EcgController(EcgService ecgService,
            EcgConversionService ecgConversionService,
            IUnitOfWorkManager unitOfWorkManager)
        {
            this.ecgService = ecgService;
            this.ecgConversionService = ecgConversionService;
            this.unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 同步心电图数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncGetEcgs(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                Logger.LogInformation("开始执行SyncGetEcgs...");
                Logger.LogInformation($"参数PatientNo：{dto.PatientNo}|PatientType：{dto.PatientType}");
                var isSucc = await ecgService.SyncEcgs(dto);
                unitOfWorkManager.Commit();

                //var updateIsSucc = ecgConversionService.UpdateEcgDataFromPdfData(dto);//注释该行代码
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
        /// 推送心电图信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/xml")]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<string>> PushEcgs(Request request)
        {
            DoCareResponse<string> result = null;
            string msgId = string.Empty;
            string resultCode = "AA";
            string resultContent = "成功";
            StringBuilder msg_builder = new StringBuilder();
            msg_builder.Append("<Response>");
            msg_builder.Append("<Header>");
            msg_builder.Append("<SourceSystem>");
            msg_builder.Append("ECG");
            msg_builder.Append("</SourceSystem>");
            msg_builder.Append("<MessageID>");
            msg_builder.Append("{0}");
            msg_builder.Append("</MessageID>");
            msg_builder.Append("</Header>");
            msg_builder.Append("<Body>");
            msg_builder.Append("<ResultCode>");
            msg_builder.Append("{1}");
            msg_builder.Append("</ResultCode>");
            msg_builder.Append("<ResultContent>");
            msg_builder.Append("{2}");
            msg_builder.Append("</ResultContent>");
            msg_builder.Append("</Body>");
            msg_builder.Append("</Response>");

            try
            {
                msgId = await ecgService.PushEcgs(request);
                result = Success(string.Format(msg_builder.ToString(), msgId, resultCode, resultContent));
            }
            catch (Exception ex)
            {
                resultCode = "AE";
                resultContent = "失败";
                Logger.LogError(ex, ex.Message);
                result = Error<string>(string.Format(msg_builder.ToString(), msgId, resultCode, resultContent));
            }
            return result;
        }
    }
}