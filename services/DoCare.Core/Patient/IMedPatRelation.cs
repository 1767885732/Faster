using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using System.Threading.Tasks;

namespace DoCare.Core.Patient
{
    [IgnoreDependency]
    public interface IMedPatRelation
    {
        /// <summary>
        /// 获取患者关联号
        /// </summary>
        Task<MedPatRelationInfo> SyncPatientRelationNo(MedPatRelationInfo relationEntity);
    }
}
