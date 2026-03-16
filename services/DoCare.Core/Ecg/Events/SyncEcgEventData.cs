using DoCare.Core.Common;
using FastFrame.EventBuses;

namespace DoCare.Core.Patient.Events
{
    /// <summary>
    /// 同步患者心电图
    /// </summary>
    public class SyncEcgEventData : EventDataBase
    {
        public SyncPatientDto Dto { get; set; }
    }
}
