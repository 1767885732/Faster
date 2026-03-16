using DoCare.Core.Common;
using System;

namespace DoCare.Core.Patient.Dtos
{
    public class PatientNoData
    {
        public PatientNoData()
        {
            OrderNo = 0;
            CreateDate = DateTime.Now;
        }
        public int OrderNo { get; set; }

        public DateTime CreateDate { get; set; }

        public string OrderNoStr =>
            $"{CreateDate.ToString($"{HospitalStatic.HospitalNo}yyMMdd")}{OrderNo.ToString().PadLeft(3, '0')}";
    }
}
