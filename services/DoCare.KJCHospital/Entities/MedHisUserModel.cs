using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoCare.KJCHospital.Entities
{
    public class MedHisUserModel
    {
        public string patient_id { get; set; }
        public string visit_id { get; set; }
        public string visit_type { get; set; }
        public string inpat_no { get; set; }
        public string clinic_no { get; set; }
        public string name { get; set; }
        public string date_of_birth { get; set; }
        public string sex { get; set; }
        public string id_no { get; set; }
        public string pat_job { get; set; }
        public string mr_no { get; set; }
        public string mail_address { get; set; }
        public string next_of_kin { get; set; }
        public string relationship { get; set; }
        public string next_of_kin_phone { get; set; }
        public string regist_time { get; set; }
        public string phone_numer { get; set; }
        public string dept_code { get; set; }
        public string diag_desc { get; set; }
        public string citizen_card { get; set; }
    }
}
