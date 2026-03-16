using DoCare.Core.Patient.Entities;
using System.Threading.Tasks;

namespace DoCare.Core.Patient
{
    public interface IOutcomeInfo
    {
        /// <summary>
        /// 获取转归信息
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        Task<MedPatientDetailsInfo> GetOutcomeInfo(MedPatRelationInfo relation);
    }
}
