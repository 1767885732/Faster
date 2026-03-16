using FastFrame.EventBuses;

namespace DoCare.Core.Common.Events
{
    /// <summary>
    /// 推送患者事件数据
    /// </summary>
    public class PushPatientEventData : EventDataBase
    {
        public string PatientNo { get; set; }

        public AppType App { get; set; }
    }
}
