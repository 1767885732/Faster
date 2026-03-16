using DoCare.Core.Exam_Inspec_Medication.Dtos;
using DoCare.Core.Patient.Entities;
using System.Threading.Tasks;

namespace DoCare.Core.Exam_Inspec_Medication
{
    public interface IMedication
    {
        Task<MedicationDto> GetMedications(MedPatRelationInfo relation);
    }
}
