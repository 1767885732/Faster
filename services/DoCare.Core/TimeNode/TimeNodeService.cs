using Dapper;
using DoCare.Core.Patient.Entities;
using DoCare.Core.TimeNode.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.Core.TimeNode
{

    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class TimeNodeService
    {
        private readonly ILogger<TimeNodeService> logger;
        private readonly IFirstAid firstAid;
        private readonly IEmergency emergency;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedPatTimeNodes, Guid> medPatTimeNodes;

        public TimeNodeService(ILogger<TimeNodeService> logger,
            IFirstAid firstAid, IEmergency emergency,
            IRepository<MedPatRelationInfo, string> medPatRelation, IRepository<MedPatTimeNodes, Guid> medPatTimeNodes)
        {
            this.logger = logger;
            this.firstAid = firstAid;
            this.emergency = emergency;
            this.medPatRelation = medPatRelation;
            this.medPatTimeNodes = medPatTimeNodes;
        }

        public async Task<bool> RunAsync(string patientNo)
        {
            logger.LogDebug($"同步时间节点,患者编号:{patientNo}.Start...");
            bool result = false;
            if (string.IsNullOrEmpty(patientNo)) return false;

            var relation = await medPatRelation.GetAsync(patientNo);
            logger.LogDebug($"患者:{relation.ToJson()}");

            var lstMedPatTime = new List<MedPatTimeNodes>();
            if (firstAid is IMedPatTimeNode firstAidMedPatTimeNode)
            {
                //logger.LogDebug($"进入院前同步腕带");
                //try
                //{
                //    var firstAidTimeNodes = await firstAidMedPatTimeNode.SyncMedPatTimeNode(relation);
                //    if ((firstAidTimeNodes?.Count ?? 0) > 0)
                //        lstMedPatTime.AddRange(firstAidTimeNodes);
                //}
                //catch (Exception ex)
                //{
                //    logger.LogError($"腕带-急救同步错误:{ex.Message}");
                //}
            }

            if (lstMedPatTime.Count == 0 && emergency is IMedPatTimeNode emergencyMedPatTimeNode)
            {
                logger.LogDebug($"进入急诊同步腕带");
                try
                {
                    var emergencyTimeNodes = await emergencyMedPatTimeNode.SyncMedPatTimeNode(relation);
                    logger.LogDebug($"同步到的数据条数:"+emergencyTimeNodes.Count);
                    if ((emergencyTimeNodes?.Count ?? 0) > 0)
                        lstMedPatTime.AddRange(emergencyTimeNodes);
                }
                catch (Exception ex)
                {
                    logger.LogError($"腕带-急诊同步错误:{ex.Message}");
                }
            }

            if (lstMedPatTime.Count < 1) return true;

            try
            {
                logger.LogDebug($"同步到得时间节点数据：{lstMedPatTime.ToJson()}");
                var tagItem = lstMedPatTime.FirstOrDefault();

                foreach (var item in lstMedPatTime) 
                {
                    item.PatientNo = patientNo;
                    if (string.IsNullOrEmpty(item.TagId)) item.TagId = tagItem.TagId;
                    if (!item.BindTime.HasValue) item.BindTime = tagItem.BindTime;

                    logger.LogError($"A1:{item.ItemName}");
                    var db = await medPatTimeNodes.GetFirstAsync(a => a.PatientNo == item.PatientNo && a.TagId == item.TagId && a.ItemName == item.ItemName);
                    if (db == null)
                    {
                        //不知道为啥用封装的insert返回1一直无法插入数据库
                        //所以没办法临时重新写了一个新增的方法。。。
                        var res = InsertTimeNodes(item);
                        logger.LogError($"A2:{res}");
                    }
                    else
                    {
                        item.Id = db.Id;
                        db = item;
                        await medPatTimeNodes.UpdateAsync(db);
                    }
                }


                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError($"腕带-数据入库错误:{ex.Message}");
            }
            return result;
        }

        public static OracleConnection CrateOracleConnection(string connectionString)
        {
            var connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }

        public bool InsertTimeNodes(MedPatTimeNodes nodes)
        {
            IConfiguration configuration = Singleton<IConfiguration>.Instance;
            string MedCommConnStr = configuration["FastFrame:DbContexts:DoCare:ConnectionString"];
            using (OracleConnection conn = CrateOracleConnection(MedCommConnStr))
            {
                var guid = Guid.NewGuid().ToString();
                string executeSql = $@"insert into med_pat_timenodes(PATIENT_NO,TAG_ID,ITEM_NAME,ITEM_VALUE,BIND_TIME,ID)
                                        values('{nodes.PatientNo}','{nodes.TagId}','{nodes.ItemName}',
                to_date('{nodes.ItemValue}','yyyy-mm-dd hh24:mi:ss'),to_date('{nodes.BindTime}','yyyy-mm-dd hh24:mi:ss'),'{guid}')";
                logger.LogError($"A3:{executeSql}");
                return conn.Execute(executeSql) > 0 ? true : false;
            }
        }
    }
}
