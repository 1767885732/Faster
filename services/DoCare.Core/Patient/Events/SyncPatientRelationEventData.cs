using FastFrame.EventBuses;

namespace DoCare.Core.Patient.Events
{
    /// <summary>
    /// 同步患者基本信息的事件数据
    /// </summary>
    public class SyncPatientRelationEventData : EventDataBase
    {
        public string PatientNo { get; set; }
    }
}
