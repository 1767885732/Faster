using DoCare.Mdsd.FirstAid.Dtos;
using DoCare.Mdsd.FirstAid.Entities;
using FastFrame.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoCare.Mdsd.FirstAid.Driver
{
    public class FirstAidEcgClient : HttpClientBase
    {
        private readonly ILogger<FirstAidEcgClient> logger;

        public FirstAidEcgClient(ILogger<FirstAidEcgClient> logger, HttpClient httpClient) : base(httpClient)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 获取院前心电列表
        /// </summary>
        public async Task<List<EcgInfo>> GetEcgDataList(string phepPatientId, string phepMissionId)
        {
            List<EcgInfo> ecgDatas = null;
            if (string.IsNullOrEmpty(phepPatientId) || string.IsNullOrEmpty(phepMissionId)) return ecgDatas;

            var route = "/ECGService/GetEcgData";
            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}/{route}?patientid={phepPatientId}&visitId={phepMissionId}";

            var response = await ApiClient.GetAsync(requestUri, BasicAuth);
            ecgDatas = response.ContentAsType<PhepResponse<List<EcgInfo>>>()?.data;
            return ecgDatas;
        }

        /// <summary>
        /// 获取院前心电详情
        /// </summary>
        /// <param name="ecgId"></param>
        /// <returns></returns>
        public async Task<EcgInfo> GetEcgData(string ecgId)
        {
            EcgInfo ecgData = null;
            if (string.IsNullOrEmpty(ecgId)) return ecgData;

            var route = "ECGService/GetEcgDataByEcgId";
            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}/{route}?EcgId={ecgId}";
            var response = await ApiClient.GetAsync(requestUri, BasicAuth);
            ecgData = response.ContentAsType<PhepResponse<EcgInfo>>()?.data;
            return ecgData;
        }

        public string GetEcgView(string patientId, string visitId)
        {
            var viewUrl = string.Empty;
            if (string.IsNullOrEmpty(patientId) || string.IsNullOrEmpty(visitId)) return viewUrl;

            var route = "/ECGBrowser/PatientEcgReport";
            viewUrl = $"{ApiOption.Host}{route}?patientId={patientId}&visitId={visitId}";
            return viewUrl;
        }

    }
}
