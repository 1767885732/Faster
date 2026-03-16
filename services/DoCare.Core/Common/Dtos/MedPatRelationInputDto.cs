using DoCare.Core.Patient.Entities;
using FastFrame.Mapping;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Common.Dtos
{
    [MapTo(typeof(MedPatRelationInfo))]
    public class MedPatRelationInputDto 
    {
        [Required, DisplayName("急诊编号")]
        public string EmergencyId { get; set; }

        [DisplayName("备注")]
        public string Memo { get; set; }
    }
}
