using DoCare.Core.Common;
using DoCare.Core.Patient;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.Core.TimeNode;
using DoCare.Core.TimeNode.Entities;
using FastFrame.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    public class PatientController : DoCareApiController
    {
        private readonly PatientService patientService;

        public PatientController(PatientService patientService)
        {
            this.patientService = patientService;
        }

        /// <summary>
        /// 查询患者列表
        /// </summary>
        /// <param name="dto">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DoCareResponse<List<PatRegistOutDto>>> QueryRegistInfo(PatRegistInputDto dto)
        {
            DoCareResponse<List<PatRegistOutDto>> result = null;
            try
            {
                List<PatRegistOutDto> regists = await patientService.Query(dto);
                Logger.LogError(JsonConvert.SerializeObject(regists[0]));
                result = Success(regists);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<List<PatRegistOutDto>>();
            }
            return result;
        }

        /// <summary>
        /// 查询患者列表
        /// </summary>
        /// <param name="dto">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DoCareResponse<List<PatRegistOutDto>>> GetPhepPatInfo(PatRegistInputDto dto)
        {
            DoCareResponse<List<PatRegistOutDto>> result = null;
            try
            {
                List<PatRegistOutDto> regists = await patientService.Query(dto);
                Logger.LogError(JsonConvert.SerializeObject(regists[0]));

                result = Success(regists);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<List<PatRegistOutDto>>();
            }
            return result;
        }

        /// <summary>
        /// 更新患者关系数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncPatRelation(SyncRelationInputDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await patientService.Save(dto);
                result = Success(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<bool>();
            }
            return result;
        }

        /// <summary>
        /// 同步转归数据
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncPatOut(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await patientService.SyncPatOutProcess(dto);
                result = Success(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<bool>();
            }
            return result;
        }

        /// <summary>
        /// 根据挂号编号获取急诊号
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<object>> GetEmergencyId(SyncPatientDto dto)
        {
            DoCareResponse<object> result = null;
            try
            {
                var isSucc = await patientService.GetEmergencyNo(dto.PatientNo);
                result = Success(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<object>();
            }
            return result;
        }

        /// <summary>
        /// 同步患者急诊急救时间信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<PhepAutoTimes>> SyncPhepAutoTime(SyncPatientDto dto)
        {
            DoCareResponse<PhepAutoTimes> result = null;
            try
            {
                var isSucc = await patientService.SyncPhepAutoTime(dto);
                result = Success(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<PhepAutoTimes>();
            }
            return result;
        }

        /// <summary>
        /// 同步科室字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<List<MedDeptDict>>> SyncDeptDict()
        {
            DoCareResponse<List<MedDeptDict>> result = null;
            try
            {
                var isSucc = await patientService.SyncDeptDict();
                result = Success(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<List<MedDeptDict>>();
            }
            return result;
        }
    }
}