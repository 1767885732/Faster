namespace DoCare.Core.Patient.Dtos
{
    public class GetPatientInfoOutputDto
    {
        public string PATIENT_ID { get; set; }

        public string VISIT_ID { get; set; }

        public string 姓名 { get; set; }

        public string 性别 { get; set; }

        public string 患者年龄 { get; set; }

        public string 体重 { get; set; }

        public string 身高 { get; set; }
    }
}