using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.Patient
{
    /// <summary>
    /// 患者信息接口
    /// </summary>
    [IgnoreDependency]
    public interface IPatient
    {

        /// <summary>
        /// 同步患者信息
        /// </summary>
        Task<string> SyncPatientData(MedPatRelationInfo relation);

        /// <summary>
        /// 查询对应条件的患者基本信息
        /// </summary>
        /// <param name="queryParms"></param>
        /// <returns></returns>
        Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms);
    }
}
