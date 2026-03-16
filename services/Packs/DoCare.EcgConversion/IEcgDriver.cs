using ECGConversion;
using FastFrame.Core;

namespace DoCare.EcgConversion
{
    [IgnoreDependency]
    public interface IEcgDriver
    {
        IECGFormat Read(string fullPath);
    }
}
