using FastFrame.Core;
using System.Threading.Tasks;

namespace DoCare.Core.Common.Notify
{
    /// <summary>
    /// 推送到具体专科需实现得接口
    /// </summary>
    [MultipleDependency]
    public interface ISpecial
    {
        Task PushToSpecial(string patientNo);
    }
}
