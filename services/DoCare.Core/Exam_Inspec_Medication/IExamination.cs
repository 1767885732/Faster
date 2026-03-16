using DoCare.Core.Exam_Inspec_Medication.Dtos;
using DoCare.Core.Patient.Entities;
using System.Threading.Tasks;

namespace DoCare.Core.Exam_Inspec_Medication
{
    public interface IExamination
    {
        Task<ExaminationDto> GetExaminations(MedPatRelationInfo relation);
    }
}
