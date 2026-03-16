using DoCare.NTSYHospital.Entities;
using DoCare.NTSYHospital.service;
using FastFrame.Core;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.NTSYHospital
{
    [Dependency(ServiceLifetime.Scoped)]
    public class LisDriver
    {
        private readonly ILogger<LisDriver> logger;
        private readonly IRepository<ViewLabMaster, string> viewLabMaster;
        private readonly IRepository<ViewLabResult, string> viewLabResult;
        private readonly IRepository<ViewExamApply, string> viewExamApply;


        public LisDriver(ILogger<LisDriver> logger
          , IRepository<ViewLabMaster, string> viewLabMaster
          , IRepository<ViewLabResult, string> viewLabResult
          , IRepository<ViewExamApply, string> viewExamApply
          )
        {
            this.logger = logger;
            this.viewLabMaster = viewLabMaster;
            this.viewLabResult = viewLabResult;
            this.viewExamApply = viewExamApply;
        }

        public async Task<List<ViewLabMaster>> GetViewLabMasterByRegistId(string registId)
        {

            List<ViewLabMaster> list = new List<ViewLabMaster>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT REP.REQUESTNO AS Id,--TEST_NO
                                   '' AS PRIORITY_INDICATOR, -- 优先标志
                                   REP.PATIENTID AS INP_NO,--PATIENT_NO
                                   REP.PATIENTID AS PATIENT_ID,
                                   REP.PATIENTNAME AS NAME,
                                   '' AS TEST_CAUSE, --检验目的
                                   REP.CLINICALDIAGINFO AS RELEVANT_CLINIC_DIAG,
                                   REP.SPECIMENTYPENAME AS SPECIMEN,
                                   SP.SPECIMENCOLLECTIONDATETIME AS SPCM_RECEIVED_DATE_TIME,
                                   REP.REQUESTDATETIME AS APPLY_TIME,
                                   REP.DEPTCODE AS ORDERING_DEPT,
                                   REP.DOCTORCODE AS ORDERING_PROVIDER,
                                   REP.EXECDEPTCODE AS PERFORMED_BY,
                                   REP.STATUSNAME AS RESULT_STATUS,
                                   REP.REPORTDATETIME AS RESULTS_RPT_DATE_TIME,
                                   REP.REPORTERNAME AS TRANSCRIPTIONIST,
                                   REP.REVIEWERNAME AS VERIFIED_BY
                              FROM CDR_LAB.DBO.LAB_REPORT REP
                              LEFT JOIN CDR_LAB.DBO.LAB_SPECIMEN SP
                                ON REP.REQUESTNO = SP.REQUESTNO
                               AND SP.VISITNUMBER = REP.VISITNUMBER
                               AND SP.PATIENTID = REP.PATIENTID";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(registId))
                {
                    whereStr += string.Format(" AND REP.PATIENTID='{0}'", registId);
                }


                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("获取检验信息GetViewLabMasterByRegistId:Sql\r\n" + lastSql);
                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewLabMaster>(lastSql);
                logger.LogInformation("获取到检验数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();



            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //return viewLabMaster.Query().Where(o => o.CLINIC_REGIST_NO == registId).ToList();
            #endregion
        }

        public async Task<List<ViewLabMaster>> GetViewLabMasterByInp(string inpRegistId)
        {
            List<ViewLabMaster> list = new List<ViewLabMaster>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT REP.REQUESTNO AS ID,--TEST_NO
                                   '' AS PRIORITY_INDICATOR, -- 优先标志
                                   REP.PATIENTID AS INP_NO,--PATIENT_NO,
                                   REP.PATIENTID AS PATIENT_ID,
                                   REP.PATIENTNAME AS NAME,
                                   '' AS TEST_CAUSE, --检验目的
                                   REP.CLINICALDIAGINFO AS RELEVANT_CLINIC_DIAG,
                                   REP.SPECIMENTYPENAME AS SPECIMEN,
                                   SP.SPECIMENCOLLECTIONDATETIME AS SPCM_RECEIVED_DATE_TIME,
                                   REP.REQUESTDATETIME AS APPLY_TIME,
                                   REP.DEPTCODE AS ORDERING_DEPT,
                                   REP.DOCTORCODE AS ORDERING_PROVIDER,
                                   REP.EXECDEPTCODE AS PERFORMED_BY,
                                   REP.STATUSNAME AS RESULT_STATUS,
                                   REP.REPORTDATETIME AS RESULTS_RPT_DATE_TIME,
                                   REP.REPORTERNAME AS TRANSCRIPTIONIST,
                                   REP.REVIEWERNAME AS VERIFIED_BY
                              FROM CDR_LAB.DBO.LAB_REPORT REP
                              LEFT JOIN CDR_LAB.DBO.LAB_SPECIMEN SP
                                ON REP.REQUESTNO = SP.REQUESTNO
                               AND SP.VISITNUMBER = REP.VISITNUMBER
                               AND SP.PATIENTID = REP.PATIENTID";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(inpRegistId))
                {
                    whereStr += string.Format(" AND REP.PATIENTID='{0}'", inpRegistId);
                }


                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("获取检验信息GetViewLabMasterByRegistId:Sql\r\n" + lastSql);
                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewLabMaster>(lastSql);
                logger.LogInformation("获取到检验数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();
            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //return viewLabMaster.Query().Where(o => o.INP_NO == inpRegistId).ToList();
            #endregion
        }

        public async Task<List<ViewLabResult>> GetViewLabResult(string testNo)
        {
            List<ViewLabResult> list = new List<ViewLabResult>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT R.REQUESTNO                AS TEST_NO,
                                           R.OBSERVATIONCODE          AS REPORT_ITEM_CODE,
                                           R.OBSERVATIONNAME_CN       AS REPORT_ITEM_NAME,
                                           R.ISABNORMAL               AS ABNORMAL_INDICATOR,
                                           R.OBSERVATIONORIGINALVALUE AS RESULT,
                                           R.OBSERVATIONVALUEUNIT     AS UNITS,
                                           R.OBSERVATIONDATETIME      AS RESULT_DATE_TIME,
                                           R.REFERENCERANGETYPE       AS REFERENCE_RESULT
                                      FROM CDR_LAB.DBO.LAB_RESULT R WITH(NOLOCK)";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(testNo))
                {
                    whereStr += string.Format(" AND R.REQUESTNO='{0}'", testNo);
                }
                else
                    return null;


                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("获取检验信息GetViewLabMasterByRegistId:Sql\r\n" + lastSql);
                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewLabResult>(lastSql);
                logger.LogInformation("获取到检验数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();


            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            // return viewLabResult.Query().Where(o => o.TEST_NO == testNo).ToList();
            #endregion

        }
        public async Task<List<ViewExamApply>> GetViewExamApply(string registId)
        {
            List<ViewExamApply> list = new List<ViewExamApply>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT ITEM.REQUESTNO AS EXAM_NO,
                                               REP.PATIENTID AS PATIENT_NO,
                                               REP.STUDYCLASSNAME AS EXAM_CLASS,
                                               REP.STUDYSUBCLASSNAME AS EXAM_SUB_CLASS,
                                               REP.CLINICALDIAGINFO AS CLIN_DIAG,
                                               REP.STUDYMETHODNAME AS EXAM_MODE,
                                               REP.INSTRUMENT AS DEVICE,
                                               REP.EXECDEPTNAME AS PERFORMED_BY,
                                               REP.REQUESTDATETIME AS REQ_DATE_TIME,
                                               REP.DOCTORDEPTNAME AS REQ_DEPT,
                                               REP.DOCTORNAME AS REQ_PHYSICIAN,
                                               '' AS REQ_MEMO, --  申请备注
                                               '' AS NOTICE, --注意事项
                                               REP.STUDYOPERATEDATETIME AS EXAM_START_DATE,
                                               REP.STUDYOPERATEDATETIME AS EXAM_END_DATE,
                                               REP.STUDYREPORTDATETIME AS REPORT_DATE_TIME,
                                               ITEM.STUDYSITENAME AS EXAM_PARA,
                                               REP.IMAGINGFINDINGS AS DESCRIPTION,
                                               REP.STUDYCONCLUSION AS RECOMMENDATION,
                                               REP.STUDYOPERATERNAME AS TECHNICIAN,
                                               REP.STUDYREPORTERNAME AS REPORTER,
                                               REP.STATUSNAME AS RESULT_STATUS,
                                               REP.STUDYREVIEWERNAME AS VERIFIED_BY,
                                               REP.STUDYREVIEWDATETIME AS VERIFIED_DATE_TIME
                                          FROM CDR_RIS.DBO.RIS_REPORT REP
                                          LEFT JOIN CDR_RIS.DBO.RIS_REPORTITEM ITEM
                                            ON ITEM.REPORTNO = REP.REPORTNO
                                           AND ITEM.HOSPITALCODE = REP.HOSPITALCODE";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(registId))
                {
                    whereStr += string.Format(" AND REP.PATIENTID='{0}'", registId);
                }


                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("获取检查信息GetViewExamApplySql\r\n" + lastSql);
                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewExamApply>(lastSql);
                logger.LogInformation("获取到检查数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取

            //return viewExamApply.Query().Where(o => o.Id == registId).ToList();
            #endregion
        }

    }
}
