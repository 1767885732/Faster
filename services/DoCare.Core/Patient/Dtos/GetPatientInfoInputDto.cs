using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Dtos
{
    public class GetPatientInfoInputDto
    {
        [Required(ErrorMessage = "患者ID不能为空")]
        public string PatientId { get; set; }

        [Required(ErrorMessage = "就诊ID不能为空")]
        public string VisitId { get; set; }
    }
}