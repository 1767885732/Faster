using FastFrame.Data;
using FastFrame.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoCare.Core.Common.Notify.SpeciaList
{
    /// <summary>
    /// 专科中心-创伤
    /// </summary>
    public class WoundApiClient : HttpClientBase, ISpecial
    {
        public WoundApiClient(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// 推送至专科
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public async Task PushToSpecial(string patientNo)
        {
            //获取数据（接口）
            var route = "/Synchronous/SynchronousPatient";
            var parameter = new
            {
                patientNo = patientNo
            };

            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await ApiClient.PostAsync(requestUri, parameter);
        }
    }
}
