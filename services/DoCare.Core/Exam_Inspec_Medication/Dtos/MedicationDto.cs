using DoCare.Core.Exam_Inspec_Medication.Entities;
using System.Collections.Generic;

namespace DoCare.Core.Exam_Inspec_Medication.Dtos
{
    public class MedicationDto
    {
        public List<MedPatOrders> MedPatOrderses { get; set; }
    }
}
