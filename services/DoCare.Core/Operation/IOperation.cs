using DoCare.Core.Operation.Dtos;
using DoCare.Core.Patient.Entities;
using System.Threading.Tasks;

namespace DoCare.Core.Operation
{
    public interface IOperation
    {
        /// <summary>
        /// 手术
        /// </summary>
        Task<OperationDto> GetOperations(MedPatRelationInfo relation);
    }
}
