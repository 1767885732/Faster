using DoCare.Core.Exam_Inspec_Medication.Entities;
using System.Collections.Generic;

namespace DoCare.Core.Exam_Inspec_Medication.Dtos
{
    public class InspectionDto
    {
        public List<MedLabMaster> MedLabMasters { get; set; }

        public List<MedLabResult> MedLabResults { get; set; }
    }
}
