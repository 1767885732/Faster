using FastFrame.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoCare.Core.Common.Notify.SpeciaList
{
    /// <summary>
    /// 专科中心-卒中
    /// </summary>
    public class StrokeApiClient : HttpClientBase, ISpecial
    {
        public StrokeApiClient(HttpClient httpClient) : base(httpClient)
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
            var route = "";
            var parameter = new
            {
                patientNo = patientNo
            };

            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await ApiClient.PostAsync(requestUri, parameter);
        }
    }
}
