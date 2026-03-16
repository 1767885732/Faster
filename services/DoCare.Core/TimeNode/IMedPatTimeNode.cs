using DoCare.Core.Patient.Entities;
using DoCare.Core.TimeNode.Entities;
using FastFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.TimeNode
{
    /// <summary>
    /// 获取时间节点接口
    /// </summary>
    [IgnoreDependency]
    public interface IMedPatTimeNode
    {
        Task<List<MedPatTimeNodes>> SyncMedPatTimeNode(MedPatRelationInfo relationEntity);
    }
}
