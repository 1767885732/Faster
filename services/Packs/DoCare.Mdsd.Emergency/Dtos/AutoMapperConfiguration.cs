using AutoMapper.Configuration;
using DoCare.Core.Common;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.Mdsd.Emergency.Entities;
using FastFrame.AutoMapper;
using FastFrame.Core;
using FastFrame.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DoCare.Mdsd.Emergency.Dtos
{
    [Dependency(ServiceLifetime.Singleton)]
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void CreateMaps(MapperConfigurationExpression mapper)
        {
            #region 急诊患者基本信息
            mapper.CreateMap<ViewCpcPatientInfo, MedPatientRegistInfo>()
           .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
           .ForMember(rn => rn.PatJob, opt => { opt.Ignore(); })
           .ForMember(rn => rn.OrgCode, opt => opt.MapFrom(r => HospitalStatic.OrgCode))
           .ForMember(rn => rn.DiagLev, opt => { opt.Ignore(); })
           .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
           .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
           .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.BIRTHDAY.ToDateTime()))
           .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => ConvertGender(r.GENDER)))
           .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.IDCARDNO))
           .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CITIZEN_CARD))
           .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.ADDRESS))
           .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE));

            mapper.CreateMap<ViewCpcPatientInfo, PatRegistOutDto>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.PatJob, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OrgCode, opt => opt.MapFrom(r => HospitalStatic.OrgCode))
            .ForMember(rn => rn.DiagLev, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Diagnosis, opt => opt.MapFrom(r => r.PATIENTSAY))
            .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
            .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.BIRTHDAY.ToDateTime()))
            .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => ConvertGender(r.GENDER)))
            .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.IDCARDNO))
            .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CITIZEN_CARD))
            .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.ADDRESS))
            .ForMember(rn => rn.RelationId, opt => opt.MapFrom(r => r.PATIENTID))
            .ForMember(rn => rn.DisplayId, opt => opt.MapFrom(r => r.Id))
            .ForMember(rn => rn.RegistTime, opt => opt.MapFrom(r => r.REGISTTIME))
            .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE))
            .ForMember(rn => rn.IsDocare, opt => opt.MapFrom(r => r.ISDOCARE))
            .ForMember(rn => rn.TagId, opt => opt.MapFrom(r => r.WRISTBARCODE))
            .ForMember(rn => rn.PatientSay, opt => opt.MapFrom(r => r.PATIENTSAY));
            #endregion

            #region 急诊患者详情
            mapper.CreateMap<ViewCpcEmergencyInfo, MedPatientDetailsInfo>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DeptCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ArrivedDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.IsGreenChannel, opt => { opt.Ignore(); })
            .ForMember(rn => rn.RegistTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InHosTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InDeptCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InWardCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InHosDiagnosis, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ToastFlag, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutHosTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.HosDay, opt => { opt.Ignore(); })
            .ForMember(rn => rn.TotalCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DrugCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ExamCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutWay, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Doctor, opt => opt.MapFrom(r => r.DOCTOR))
            .ForMember(rn => rn.AdmissionTime, opt => opt.MapFrom(r => r.ADMISSIONTIMEFORSELF))
            .ForMember(rn => rn.DiagDoctor, opt => opt.MapFrom(r => r.DOCTORFORCP))
            .ForMember(rn => rn.TriageTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DiagTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DiagDesc, opt => opt.MapFrom(r => r.DIAGNOSISDESC))
            .ForMember(rn => rn.Complaint, opt => { opt.Ignore(); })
            .ForMember(rn => rn.IsEmg, opt => { opt.Ignore(); })
            .ForMember(rn => rn.EmgBedNo, opt => { opt.Ignore(); })
            .ForMember(rn => rn.TransferTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.IcuSense, opt => opt.MapFrom(r => r.ICUSENSE))
            .ForMember(rn => rn.Temp, opt => opt.MapFrom(r => r.TEMPERATURE))
            .ForMember(rn => rn.Br, opt => opt.MapFrom(r => r.RESPIRATIONRATE))
            .ForMember(rn => rn.Plus, opt => opt.MapFrom(r => r.PULSERATE))
            .ForMember(rn => rn.Hr, opt => opt.MapFrom(r => r.HEARTRATE))
            .ForMember(rn => rn.Bp, opt => opt.MapFrom(r => r.BLOODPRESSURE))
            .ForMember(rn => rn.Spo2, opt => opt.MapFrom(r => r.BLOOD_OXYGEN))
            .ForMember(rn => rn.Complaint, opt => opt.MapFrom(r => r.PATIENTSAY));
            #endregion
        }

        private string ConvertGender(string genderCode)
        {
            var result = "女";
            if (genderCode == "1")
            {
                result = "男";
            }
            return result;
        }
    }
}
