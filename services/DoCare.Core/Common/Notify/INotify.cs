using System.Threading.Tasks;

namespace DoCare.Core.Common.Notify
{
    /// <summary>
    /// 推送接口
    /// </summary>
    public interface INotify
    {
        Task RunAsync(string patientNo, AppType app);
    }
}
