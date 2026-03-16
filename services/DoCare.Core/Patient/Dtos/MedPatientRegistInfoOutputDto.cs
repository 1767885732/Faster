using DoCare.Core.Patient.Entities;
using FastFrame.Mapping;
using System;

namespace DoCare.Core.Patient.Dtos
{
    [Serializable]
    [MapFrom(typeof(MedPatientRegistInfo))]
    public class MedPatientRegistInfoOutputDto : MedPatientRegistInfo
    {
        /// <summary>
        /// 患者来源Id
        /// </summary>
        public string RelationId { get; set; }
    }
}
