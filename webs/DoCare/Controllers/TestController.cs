using DoCare.Core.Common.Events;
using DoCare.Core.Patient.Events;
using FastFrame.AspNetCore.MVC;
using FastFrame.AspNetCore.UI;
using FastFrame.Caching;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.EventBuses;
using FastFrame.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    public class TestController : ApiController
    {
        private readonly IEventBus eventBus;
        //private readonly IRepository<MedPatRelationInfo> medPatRelationInfo;
        //private readonly IUnitOfWorkManager unitOfWorkManager;
        //private readonly ICacheService cacheService;
        //public TestController(IRepository<MedPatRelationInfo> medPatRelationInfo, 
        //    IUnitOfWorkManager unitOfWorkManager, ICacheService cacheService)
        //{
        //    this.medPatRelationInfo = medPatRelationInfo;
        //    this.unitOfWorkManager = unitOfWorkManager;
        //    this.cacheService = cacheService;
        //}

        public TestController(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        [HttpPost]
        public async Task<string> GetFirstOrDefault()
        {
            return "aaa";
        }


        ///// <summary>
        ///// 测试FirstOrDefault
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<MedPatRelationOutDto> GetFirstOrDefault()
        //{
        //    MedPatRelationInfo result = null;
        //    var patientNo = "01190117002";
        //    result = medPatRelationInfo.GetFirstOrDefault(a => a.PatientNo == patientNo);
        //    return result.MapTo<MedPatRelationOutDto>();
        //}

        ///// <summary>
        ///// 测试Get主键获取
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<MedPatRelationOutDto> Find()
        //{
        //    MedPatRelationInfo result = null;
        //    var patientNo = "01190117002";
        //    result = medPatRelationInfo.Get(patientNo);
        //    return result.MapTo<MedPatRelationOutDto>();
        //}


        ///// <summary>
        ///// 测试新增Insert方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<int> Insert(List<MedPatRelationInputDto> dtos)
        //{
        //    Check.NotNull(dtos, nameof(dtos));
        //    var result = 0;
        //    var entities = dtos.MapTo<List<MedPatRelationInfo>>();
        //    entities.ForEach(a =>
        //    {
        //        a.PatientNo = Guid.NewGuid().ToString();
        //    });

        //    result = medPatRelationInfo.Insert(entities.ToArray());

        //    result = medPatRelationInfo.Insert(entities.ToArray());

        //    return result;
        //}


        ///// <summary>
        ///// 测试新增InsertDtof方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<AjaxResult> InsertDto()
        //{
        //    var dtos = new List<MedPatRelationInputDto>();
        //    dtos.Add(new MedPatRelationInputDto { EmergencyId = "1", Memo = "memo1" });
        //    dtos.Add(new MedPatRelationInputDto { EmergencyId = "2", Memo = "memo2" });

        //    var operationResult = medPatRelationInfo.Insert<MedPatRelationInputDto>(dtos,
        //      dto => Check.NotNull(dto, nameof(dto)), (dto, relation) =>
        //      {
        //          relation.PatientNo = Guid.NewGuid().ToString();
        //          return relation;
        //      });
        //    return operationResult.ToAjaxResult();
        //}


        ///// <summary>
        ///// 测试新增根据主键Delete方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<int> DeleteByKey(string patientNo)
        //{
        //    var result = medPatRelationInfo.DeleteByKey(patientNo);
        //    return result;
        //}


        ///// <summary>
        ///// 测试新增Delete方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<int> DeleteTrackEntities(string patientNo)
        //{
        //    var entities = medPatRelationInfo.TrackQuery(a => a.PatientNo.Contains(patientNo)).ToList();
        //    var result = medPatRelationInfo.Delete(entities.ToArray());
        //    return result;
        //}

        ///// <summary>
        ///// 测试新增Delete方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<int> DeleteEntities(string patientNo)
        //{
        //    var entities = medPatRelationInfo.Query(a => a.PatientNo.Contains(patientNo)).ToList();
        //    var result = medPatRelationInfo.Delete(entities.ToArray());
        //    return result;
        //}

        ///// <summary>
        ///// 测试新增InsertDtof方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<AjaxResult> DeleteByKeys()
        //{
        //    List<object> ids = new List<object> { "01190117002", "3a206f38-b5cc-4d5b-a5b3-41f28681842c" };
        //    var operationResult = medPatRelationInfo.DeleteByKeys(ids);
        //    return operationResult.ToAjaxResult();
        //}

        ///// <summary>
        ///// 测试新增DeleteBatch方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<int> DeleteBatch(string keywords)
        //{
        //    Check.NotNull(keywords, nameof(keywords));

        //    var result = medPatRelationInfo.DeleteBatch(a=>a.EmergencyId.Contains(keywords));
        //    return result;
        //}


        ///// <summary>
        ///// 测试新增Update方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<int> UpdateEntities()
        //{
        //    var entities = await medPatRelationInfo.GetFirstOrDefaultAsync(a => a.PatientNo == "57629ac5-e23a-44f0-81c6-99426e676664");
        //    var str = new Random().Next(100);
        //    entities.Res1 = str.ToString();

        //    var result = medPatRelationInfo.Update(entities, true, a => a.Res1, a => a.Res2);
        //    return result;
        //}

        ///// <summary>
        ///// 测试SqlExcute方法
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<List<PatRegisterOutDto>> SqlExcute(PatRegisterInDto inDto)
        //{
        //    List<PatRegisterOutDto> regists = new List<PatRegisterOutDto>();
        //    if (inDto == null) return regists;

        //    var sqlExecutor = unitOfWorkManager.GetSqlExecutor<ViewCpcPatientInfo>();
        //    var parms = new DynamicParameters();
        //    var sql = new StringBuilder("SELECT * FROM VIEW_CPC_PATIENT_INFO WHERE 1=1");

        //    #region 参数化方式拼接sql
        //    if (!string.IsNullOrEmpty(inDto.Name))
        //    {
        //        sql.Append(" AND NAME LIKE :Name");
        //        parms.Add("Name", "%" + inDto.Name + "%");
        //    }

        //    if (!string.IsNullOrEmpty(inDto.IdCard))
        //    {
        //        sql.AppendFormat(" AND IDCARDNO LIKE :IdCard");
        //        parms.Add("IdCard", "%" + inDto.IdCard + "%");
        //    }

        //    if (!string.IsNullOrEmpty(inDto.DiagnosisDesc))
        //    {
        //        sql.AppendFormat(" AND PATIENT_SAY LIKE :PATIENT_SAY");
        //        parms.Add("PATIENT_SAY", "%" + inDto.DiagnosisDesc + "%");
        //    }

        //    if (inDto.BeginTime.HasValue && inDto.BeginTime.Value > DateTime.MinValue)
        //    {
        //        sql.AppendFormat(" AND TO_DATE(REGISTTIME, 'yyyy-mm-dd hh24:mi:ss') >= TO_DATE(:BeginTime,'yyyy-mm-dd hh24:mi:ss') ");
        //        parms.Add(":BeginTime", inDto.BeginTime.Value.ToStringYMDHMS());
        //    }

        //    if (inDto.EndTime.HasValue && inDto.EndTime.Value > inDto.BeginTime.Value)
        //    {
        //        sql.AppendFormat(" AND TO_DATE(REGISTTIME, 'yyyy-mm-dd hh24:mi:ss') < TO_DATE(:EndTime,'yyyy-mm-dd hh24:mi:ss') ");
        //        parms.Add("EndTime", inDto.EndTime.Value.ToStringYMDHMS());
        //    }
        //    #endregion

        //    var lstPatient = sqlExecutor.FromSql<ViewCpcPatientInfo>(sql.ToString()
        //        , parms);

        //    if (lstPatient != null && lstPatient.Count() > 0)
        //    {
        //        regists = lstPatient.MapTo<List<PatRegisterOutDto>>();
        //    }
        //    return regists;
        //}

        //[HttpPost]
        //public async Task<List<MedPatRelationInfo>> CacheList()
        //{
        //    var iqueryable = medPatRelationInfo.Query();
        //    var data = cacheService.ToCacheList(iqueryable);
        //    return data;
        //}
    }
}