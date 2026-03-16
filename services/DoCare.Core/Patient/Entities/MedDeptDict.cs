using FastFrame.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Core.Patient.Entities
{
    /// <summary>
    /// [Description("科室信息")]
    /// </summary>
    public class MedDeptDict : EntityBase<string>
    {
        public int Serial_No { get; set; }

        public string Dept_Code { get; set; }

        public string Dept_Name { get; set; }

        public string Input_Code { get; set; }
    }
}
