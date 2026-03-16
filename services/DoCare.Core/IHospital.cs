using DoCare.Core.Patient;
using DoCare.Core.Patient.Entities;
using DoCare.Core.TimeNode.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core
{
    /// <summary>
    /// 院前
    /// </summary>
    public interface IFirstAid : IPatient { }

    /// <summary>
    /// 急诊
    /// </summary>
    public interface IEmergency : IPatient
    {
        /// <summary>
        /// 根据挂号编号获取急诊号
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        Task<MedPatRelationInfo> GetEmergencyNo(MedPatRelationInfo relationEntity);

        /// <summary>
        /// 同步患者急诊急救时间信息
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        Task<PhepAutoTimes> SyncPhepAutoTime(MedPatRelationInfo relationEntity);

        /// <summary>
        /// 根据患者ID、分诊挂号日期获取患者关系
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        Task<MedPatRelationInfo> GetPatientRelationInfo(string patientId, string regTime);
    }

    /// <summary>
    /// 门诊
    /// </summary>
    public interface IOutPatient : IPatient { }

    /// <summary>
    /// 住院
    /// </summary>
    public interface IInPatient : IPatient {
        Task<List<MedDeptDict>> SyncDeptDict();
    }
}
