using DoCare.Core.Exam_Inspec_Medication.Entities;
using System.Collections.Generic;

namespace DoCare.Core.Exam_Inspec_Medication.Dtos
{
    public class ExaminationDto
    {
        public List<MedExamReport> MedExamReports { get; set; }
    }
}
