using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>
    /// 麦迪斯顿急救登记信息
    /// </summary>
    public class FirstAidRegist
    {
        public string EHRID { get; set; }

        public string CLINICNO { get; set; }

        /// <summary>
        /// 急诊PatientId
        /// </summary>
        public string ECGPATIENTID { get; set; }

        public string ECGVISITID { get; set; }

        public string IDCARDNO { get; set; }

        public string CARTEVITALNO { get; set; }

        public string NAME { get; set; }

        public string BIRTHDAY { get; set; }

        public string AGE { get; set; }

        public string NATION { get; set; }

        public string GENDER { get; set; }

        public string OCCUPATION { get; set; }

        public string MARRIAGE { get; set; }

        public string EDUCATION { get; set; }

        public string PHONE { get; set; }

        public string ADDRESS { get; set; }

        public string REGISTTIME { get; set; }

        public string ATTACKADDRESS { get; set; }

        public string ATTACKTIME { get; set; }

        public string ISCALLHELP { get; set; }

        public string CALLSOURCE { get; set; }

        public string CALLTIME { get; set; }

        public string ORGID { get; set; }

        public string ORGNAME { get; set; }

        public string CREATENAME { get; set; }

        public string CREATEDATE { get; set; }

        public string SYMPTOM { get; set; }

        public string DISEASE_CATEGORY { get; set; }

        public string DOCTORNAME { get; set; }

        public string NURSENAME { get; set; }

        public string ECG_TIME { get; set; }

        public string EcgDiagnoise { get; set; }

        public string ECG_URL { get; set; }
    }
}
