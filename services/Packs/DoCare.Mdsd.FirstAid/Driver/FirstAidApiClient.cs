using DoCare.Core.Common;
using DoCare.Core.Patient.Dtos;
using DoCare.Mdsd.FirstAid.Dtos;
using DoCare.Mdsd.FirstAid.Entities;
using FastFrame.Data;
using FastFrame.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoCare.Mdsd.FirstAid.Driver
{
    /// <summary>
    /// 院前API驱动类
    /// </summary>
    public class FirstAidApiClient : HttpClientBase
    {
        private readonly ILogger<FirstAidApiClient> logger;
        public FirstAidApiClient(ILogger<FirstAidApiClient> logger, HttpClient httpClient) : base(httpClient)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 查询院前患者列表
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<FirstAidRegist>> GetRegistList(PatRegistInputDto parameters)
        {
            #region 注释
            ////获取数据（接口）
            //var route = "/BasePatient/GetBasePatientInfo";

            //var parameter = new
            //{
            //    Name = parameters.Name,
            //    DiagnosisDesc = parameters.DiagnosisDesc,
            //    StartDate = parameters.BeginTime,
            //    EndDate = parameters.EndTime,
            //    organcode = ""
            //};

            //var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}{route}";
            //var response = await ApiClient.PostAsync(requestUri, parameter, BasicAuth);
            //var registList = response.ContentAsType<PhepResponse<List<FirstAidRegist>>>()?.data;
            //return registList;
            #endregion

            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}";
            requestUri = requestUri.Replace("api", "");
            requestUri += "ZIT.WS.DataOut.asmx/AMBUL_OUT_INFO_BYSWDD";
            requestUri += "?token=BD1CE6651B88165F0661910BF9BD5922";
            requestUri += "&userid=3212820001";
            requestUri += "&begintime=" + parameters.BeginTime?.ToString("yyyy-MM-dd HH:mm:ss");
            requestUri += "&endtime=" + parameters.EndTime?.ToString("yyyy-MM-dd HH:mm:ss");

            logger.LogInformation("开始执行FirstAidApiClient.GetRegistList...");
            logger.LogInformation($"请求地址：{requestUri}");
            var returnRes = new List<FirstAidRegist>();
            var res = WebServiceHelper.GetRequest(requestUri);
            logger.LogInformation($"执行FirstAidApiClient.GetRegistList结果：{res}");
            //去除xml报文
            int IndexofA = res.IndexOf('{');
            int IndexofB = res.LastIndexOf('}');
            res = res.Substring(IndexofA, IndexofB - IndexofA + 1);

            var resModel = JsonConvert.DeserializeObject<CarUserInfo>(res);
            if (resModel.isSucceed)
            {
                resModel.resultData.ForEach(x =>
                {
                    //查病历
                    var missonInfo = GetMissionInfo(x.HZBH);
                    string name = string.Empty, age = string.Empty, gender = string.Empty, diag = string.Empty;
                    string ecg_diag = string.Empty;
                    if (missonInfo != null && missonInfo.Patient != null)
                    {
                        name = missonInfo.Patient.Name;
                        age = missonInfo.Patient.Age;
                        gender = missonInfo.Patient.Gender;
                    }
                    if (missonInfo != null && missonInfo.MedicalRecord != null)
                    {
                        diag = missonInfo.MedicalRecord.Diagnosis;
                        ecg_diag = (missonInfo.MedicalRecord.EcgDiagnoise != null) ? ((DateTime)missonInfo.MedicalRecord.EcgDiagnoise).ToString("yyyy-MM-dd HH24:mi:ss") : string.Empty;
                    }
                    
                    returnRes.Add(new FirstAidRegist
                    {
                        ECGPATIENTID = x.HZBH,
                        NAME = name,
                        AGE = age,
                        GENDER = gender,
                        ATTACKADDRESS = x.JCDD,
                        REGISTTIME = x.HJSJ,
                        SYMPTOM = diag,
                        DOCTORNAME = x.DOCTOR_NAME,
                        NURSENAME = x.NURSE_NAME,
                        ECG_TIME=x.ECG_TIME,
                        EcgDiagnoise=ecg_diag,
                        ECG_URL=x.ECG_URL
                    });
                });
            }
            return returnRes;
        }

        public CarUserInfo GetCarUserInfo(PatRegistInputDto parameters)
        {
            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}";
            requestUri = requestUri.Replace("api", "");
            requestUri += "ZIT.WS.DataOut.asmx/AMBUL_OUT_INFO_BYSWDD";
            requestUri += "?token=BD1CE6651B88165F0661910BF9BD5922";
            requestUri += "&userid=3212820001";
            requestUri += "&begintime=" + parameters.BeginTime?.ToString("yyyy-MM-dd HH:mm:ss");
            requestUri += "&endtime=" + parameters.EndTime?.ToString("yyyy-MM-dd HH:mm:ss");

            var res = WebServiceHelper.GetRequest(requestUri);
            //去除xml报文
            int IndexofA = res.IndexOf('{');
            int IndexofB = res.LastIndexOf('}');
            res = res.Substring(IndexofA, IndexofB - IndexofA + 1);

            var resModel = JsonConvert.DeserializeObject<CarUserInfo>(res);
            return resModel;
        }

        /// <summary>
        /// 获取院前电子病历数据
        /// </summary>
        public VMEmr GetMissionInfo(string patientId)
        {
            #region 注释

            //VMEmr data = null;
            //if (string.IsNullOrEmpty(missionId) || string.IsNullOrEmpty(patientId)) return data;

            //var route = "/MissionT/GetMissionInfo";
            //var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}{route}";
            //string urlParms = $"?missionID={missionId}&patientID={patientId}";

            //var response = await ApiClient.GetAsync($"{requestUri}{urlParms}", BasicAuth);
            //var vmemr = response.ContentAsType<PhepResponse<VMEmr>>()?.data;
            //return vmemr;

            #endregion

            int dc;
            IConfiguration configuration = Singleton<IConfiguration>.Instance;
            var requestUri = configuration["ConnectionStrings:emrApi"];
            requestUri += "/api/MedicalService/GetMedicalInfoByTaskNo";
            requestUri += "?LSH=" + patientId.Split('_')[0];
            requestUri += "&CCXH=" + patientId.Split('_')[1];
            requestUri += "&CLBH=" + patientId.Split('_')[2];

            var returnRes = new VMEmr();
            var res = WebServiceHelper.GetRequest(requestUri);

            logger.LogInformation($"同步院前病历信息结果：{res}");
            var resModel = JsonConvert.DeserializeObject<MedicalInfo>(res);
            if (resModel.MeidcalList.Count > 0)
            {
                var emrInfo = resModel.MeidcalList[0].MedicalInfo[0];

                //院前患者信息
                var pat = new VMPatient();
                pat.Name = emrInfo.BRXM;
                pat.Gender = emrInfo.BRXB;
                pat.Age = emrInfo.AGE;
                returnRes.Patient = pat;
                //任务信息
                var miss = new VMMission();
                miss.VehicleId = emrInfo.CLBH;
                returnRes.Mission = miss;
                //病历信息
                var blxx = new VMMedicalRecord();
                blxx.Diagnosis = emrInfo.CBYX;
                returnRes.MedicalRecord = blxx;
                //用药信息
                if (emrInfo.MedicineInfo != null)
                {
                    foreach (var medicine in emrInfo.MedicineInfo)
                    {
                        var model = new VMEmergencyOrder();
                        model.DrugCode = medicine.DRUGBASICINFOID.ToString();
                        model.DrugName = medicine.DRUGNAME;
                        model.DoseDesc = medicine.NUM.ToString();
                        model.Specifications = medicine.SPECIFICATIONS;
                        model.UsageText = medicine.METHOD;
                        returnRes.EmergencyOrderList.Add(model);
                    }
                }
                //扩展字段
                returnRes.GeneralInspectionList = new List<VMGeneralInspection>();
                var inspectionModel = new VMGeneralInspection();
                inspectionModel.Type = 2;
                foreach (var extended in emrInfo.ExtendedField)
                {
                    if (extended.NAME == "T"){inspectionModel.T = extended.VALUE;}
                    if (extended.NAME == "P") { if (int.TryParse(extended.VALUE, out dc)) inspectionModel.Pulse = dc; }
                    if (extended.NAME == "R") { if (int.TryParse(extended.VALUE, out dc)) { inspectionModel.Rr = dc; } }
                    if (extended.NAME == "HR") { if (int.TryParse(extended.VALUE, out dc)) { inspectionModel.HR = dc; } }
                    if (extended.NAME == "BP") { inspectionModel.BP = extended.VALUE; }
                    if (extended.NAME == "血氧饱和度") { if (int.TryParse(extended.VALUE, out dc)) { inspectionModel.Spo2 = dc; } }
                    if (extended.NAME == "既往史") { returnRes.MedicalRecord.PastHistory = extended.VALUE; }
                    if (extended.NAME == "药物过敏史") { returnRes.MedicalRecord.AllergicHistory = extended.VALUE; }
                }
                //呼救时间信息
                if (!string.IsNullOrEmpty(emrInfo.SAVETIME))
                {
                    var saveTime = Convert.ToDateTime(emrInfo.SAVETIME);
                    var carInfoList = GetCarUserInfo(new PatRegistInputDto { BeginTime = saveTime.AddDays(-1), EndTime = saveTime.AddDays(1) });
                    if (carInfoList.isSucceed)
                    {
                        var thisPat = carInfoList.resultData?.Where(x => x.HZBH.StartsWith(emrInfo.LSH));
                        if (thisPat.Count() == 1)
                        {
                            if (!string.IsNullOrEmpty(thisPat.First().SWSJ)) { returnRes.Mission.ArrivedHospitalTime = Convert.ToDateTime(thisPat.First().SWSJ); }
                            if (!string.IsNullOrEmpty(thisPat.First().HJSJ)) { returnRes.Mission.CallTime = Convert.ToDateTime(thisPat.First().HJSJ); }
                            if (!string.IsNullOrEmpty(thisPat.First().DDXCSJ)) { returnRes.Mission.ArrivedSceneTime = Convert.ToDateTime(thisPat.First().DDXCSJ); }
                            if (!string.IsNullOrEmpty(thisPat.First().PCSJ)) { returnRes.Mission.PlanVehicleTime = Convert.ToDateTime(thisPat.First().PCSJ); }
                            if (!string.IsNullOrEmpty(thisPat.First().CCSJ)) { returnRes.Mission.DispatchVehicleTime = Convert.ToDateTime(thisPat.First().CCSJ); }
                            if (!string.IsNullOrEmpty(thisPat.First().JCDD)) { returnRes.MedicalRecord.Address = thisPat.First().JCDD; }
                            if (!string.IsNullOrEmpty(thisPat.First().ECG_TIME)) { returnRes.MedicalRecord.FirstEcgTime = Convert.ToDateTime(thisPat.First().ECG_TIME); }
                        }
                    }
                }
                returnRes.GeneralInspectionList.Add(inspectionModel);
            }

            return returnRes;
        }

        /// <summary>
        /// 获取腕带数据
        /// </summary>
        public async Task<RfidData> GetRfidInfo(string phepPatientId, string phepMissionId, string clinicPatientId, string clinicVisitId)
        {
            RfidData rfidData = null;

            var requestRfidModel = new
            {
                PhepPatientId = phepPatientId,
                PhepVisitId = phepMissionId,
                PatientId = clinicPatientId,
                VisitId = clinicVisitId
            };

            var route = "/PatientTime/GetAllPatientRfidTimes";
            var requestUri = $"{ApiClient.BaseAddress.AbsoluteUri}{route}";
            var response = await ApiClient.PostAsync(requestUri, requestRfidModel, BasicAuth);

            rfidData = response.ContentAsType<PhepResponse<RfidData>>()?.data;
            return rfidData;
        }
    }
}
