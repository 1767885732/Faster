using AutoMapper.Configuration;
using DoCare.Core.Common;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using FastFrame.AutoMapper;
using FastFrame.Core;
using FastFrame.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace DoCare.Mdsd.FirstAid.Entities.Configurations
{
    [Dependency(ServiceLifetime.Singleton)]
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void CreateMaps(MapperConfigurationExpression mapper)
        {
            #region 院前等级信息->患者列表模型
            mapper.CreateMap<FirstAidRegist, PatRegistOutDto>()
             .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
             .ForMember(rn => rn.PatJob, opt => { opt.Ignore(); })
             .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
             .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
             .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.BIRTHDAY.ToDateTime()))
             .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => (r.GENDER)))
             .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.IDCARDNO))
             .ForMember(rn => rn.Diagnosis, opt => opt.MapFrom(r => r.SYMPTOM))
             .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CARTEVITALNO))
             .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.ATTACKADDRESS))
             .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE))
             .ForMember(rn => rn.Age, opt => opt.MapFrom(r => r.AGE))
             .ForMember(rn => rn.DcotorName, opt => opt.MapFrom(r => r.DOCTORNAME))
             .ForMember(rn => rn.NurseName, opt => opt.MapFrom(r => r.NURSENAME))
             //.ForMember(rn => rn.RegistTime, opt => opt.MapFrom(r => r.REGIST_TIME))
             .ForMember(rn => rn.RelationId, opt => opt.MapFrom(r => $"{r.ECGPATIENTID}"));
            #endregion

            #region 院前电子病历转院前患者
            mapper.CreateMap<VMEmr, MedPhepPatientInfo>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
           .ForMember(rn => rn.Sense, opt => opt.MapFrom(r => r.ShiftRecord.Mentality))
           .ForMember(rn => rn.Temp, opt => opt.MapFrom(r => r.GeneralInspectionList.Find(m => m.Type == 2).T))
           .ForMember(rn => rn.Br, opt => opt.MapFrom(r => r.GeneralInspectionList.Find(m => m.Type == 2).Rr))
           .ForMember(rn => rn.Plus, opt => opt.MapFrom(r => r.GeneralInspectionList.Find(m => m.Type == 2).Pulse))
           .ForMember(rn => rn.Hr, opt => opt.MapFrom(r => r.GeneralInspectionList.Find(m => m.Type == 2).HR))
           .ForMember(rn => rn.Bp, opt => opt.MapFrom(r => r.GeneralInspectionList.Find(m => m.Type == 2).BP))
           .ForMember(rn => rn.Spo2, opt => opt.MapFrom(r => r.GeneralInspectionList.Find(m => m.Type == 2).Spo2))
           .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.MedicalRecord.Address))
           .ForMember(rn => rn.TouchPatTime, opt => opt.MapFrom(r => r.Mission.TouchPatientTime))
           .ForMember(rn => rn.ToHospital, opt => opt.MapFrom(r => r.MedicalRecord.ToHospialName))
           .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.Patient.Name))
           .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.Patient.Birthday))
           .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.Patient.Gender))
           .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.Patient.IdNo))
           .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.Patient.HealthInsuranceId))
           .ForMember(rn => rn.PhoneNumer, opt => opt.MapFrom(r => r.MedicalRecord.ContactNumber))
           .ForMember(rn => rn.AttackAddress, opt => opt.MapFrom(r => r.MedicalRecord.Address))
           .ForMember(rn => rn.CallTime, opt => opt.MapFrom(r => r.Mission.CallTime))
           .ForMember(rn => rn.DispatchType, opt => opt.MapFrom(r => GetDispatchType(r.Mission.HospitalId, r.MedicalRecord.ToHospial)))
           .ForMember(rn => rn.DispatchVehicletime, opt => opt.MapFrom(r => r.Mission.DispatchVehicleTime))
           .ForMember(rn => rn.AcceptTime, opt => opt.MapFrom(r => r.Mission.AcceptMissionTime))
           .ForMember(rn => rn.ArrivedTime, opt => opt.MapFrom(r => r.Mission.ArrivedSceneTime))
           .ForMember(rn => rn.AttackTime, opt => opt.MapFrom(r => r.Mission.CallTime))
           .ForMember(rn => rn.LeftTime, opt => opt.MapFrom(r => r.Mission.LeftSceneTime))
           .ForMember(rn => rn.ArrivedHosTime, opt => opt.MapFrom(r => r.Mission.ArrivedHospitalTime))
           .ForMember(rn => rn.FmcDoctor, opt => opt.MapFrom(r => r.MedicalRecord.DoctorName))
           .ForMember(rn => rn.FirstDiagnosis, opt => opt.MapFrom(r => r.MedicalRecord.Diagnosis))
           .ForMember(rn => rn.KillipLevel, opt => opt.MapFrom(r => r.ChestEvaluate.Killip))
           .ForMember(rn => rn.Complaint, opt => opt.MapFrom(r => r.MedicalRecord.ChiefComplaint))
           .ForMember(rn => rn.Symptom, opt => opt.MapFrom(r => GetSymptomArray(r.ChestEvaluate)))
           .ForMember(rn => rn.NotifyEmergency, opt => opt.MapFrom(r => r.MedicalRecord.NotifyEmergencyTime))
           .ForMember(rn => rn.DiagnosisTime, opt => opt.MapFrom(r => r.MedicalRecord.SceneDiagnosisTime))
           .ForMember(rn => rn.PresentIllness, opt => opt.MapFrom(r => r.MedicalRecord.PresentIllness))
           .ForMember(rn => rn.Allergichistory, opt => opt.MapFrom(r => r.MedicalRecord.AllergicHistory))
           .ForMember(rn => rn.Pasthistory, opt => opt.MapFrom(r => r.MedicalRecord.PastHistory))
           .ForMember(rn => rn.Conditiongrade, opt => opt.MapFrom(r => r.MedicalRecord.ConditionGrade))
           .ForMember(rn => rn.Res1, opt => opt.MapFrom(r => r.Patient.Age))
           .ForMember(rn => rn.TherapeuticMethod, opt => opt.MapFrom(r => JsonExtensions.ToJson(r.TherapeuticMethod, false, false)));
            #endregion

            #region 院前注册等级信息转换患者基本信息
            //mapper.CreateMap<FirstAidRegist, MedPatientRegistInfo>()
            //  .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.PatJob, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
            //  .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.BIRTHDAY.ToDateTime()))
            //  .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => ConvertGender(r.GENDER)))
            //  .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.IDCARDNO))
            //  .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CARTEVITALNO))
            //  .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.ADDRESS))
            //  .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE))
            //  .ForMember(rn => rn.RelationId, opt => opt.MapFrom(r => $"{r.ECGPATIENTID}|{r.ECGVISITID}"));
            #endregion
        }

        /// <summary>
        /// 获取出车方式
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <param name="toHospial"></param>
        /// <returns></returns>
        private string GetDispatchType(string hospitalId, string toHospial)
        {
            var result = DispatchType.Common120.ToString("D");
            if (hospitalId == toHospial)
            {
                result = DispatchType.SelfAmbulance.ToString("D");
            }
            else
            {
                result = DispatchType.OutAmbulance.ToString("D");
            }
            return result;
        }

        private string GetSymptomArray(VMChestEvaluate chestEva)
        {
            var result = string.Empty;
            if (chestEva == null) return result;

            StringBuilder sb = new StringBuilder();
            sb.Append(chestEva.IsPersistentChestTightness == "1" ? "1" : "0");
            sb.Append(chestEva.IsIntermittentChestTightnes == "1" ? "1" : "0");
            sb.Append(chestEva.IsRemission == "1" ? "1" : "0");
            sb.Append(chestEva.IsAbdominalPain == "1" ? "1" : "0");
            sb.Append(chestEva.IsDyspnea == "1" ? "1" : "0");
            sb.Append(chestEva.IsShock == "1" ? "1" : "0");
            sb.Append(chestEva.IsHeartFailure == "1" ? "1" : "0");
            sb.Append(chestEva.IsMalignantArrhythmia == "1" ? "1" : "0");
            sb.Append("00");
            result = sb.ToString();
            return result;
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
