using DoCare.Core.Common;
using DoCare.Core.Ecg.Entities;
using FastFrame.Core;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace DoCare.Core.Ecg
{
    [Dependency(ServiceLifetime.Scoped)]
    public class OracleCRUD
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public OracleCRUD(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        public bool InsertOrUpdate(MedPatEcgInfo ecgInfo)
        {
            try
            {
                string strQuery = $"SELECT COUNT(1) FROM MED_PAT_ECG_INFO WHERE PATIENT_NO='{ ecgInfo.PatientNo }'";
                if (ecgInfo.ExamTime.HasValue)
                {
                    strQuery += $" AND EXAM_TIME=TO_DATE('{ecgInfo.ExamTime.Value}', 'yyyy-mm-dd hh24:mi:ss')";
                }

                string strSql = "";
                #region 
                if (Convert.ToInt32(OracleHelper.ExecuteScalar(strQuery)) > 0)
                {
                    strSql = @"UPDATE MED_PAT_ECG_INFO SET REPORT_TIME=:REPORT_TIME,DIAG_TIME=:DIAG_TIME,DIAG_DESC=:DIAG_DESC,DIAG_SOURCE=:DIAG_SOURCE,ECG_PATH=:ECG_PATH,ECG_URL=:ECG_URL,NAME=:NAME,SEX=:SEX,AGE=:AGE,ECG_PATIENT_ID=:ECG_PATIENT_ID,ECG_DATA=:ECG_DATA,PDF_DATA=:PDF_DATA
WHERE PATIENT_NO = :PATIENT_NO AND EXAM_TIME=:EXAM_TIME";
                }
                else
                {
                    strSql = @"INSERT INTO MED_PAT_ECG_INFO (ID,PATIENT_NO,EXAM_TIME,REPORT_TIME,DIAG_TIME,DIAG_DESC,DIAG_SOURCE,ECG_PATH,ECG_URL,NAME,SEX,AGE,ECG_PATIENT_ID,ECG_DATA,PDF_DATA)
VALUES (:ID,:PATIENT_NO,:EXAM_TIME,:REPORT_TIME,:DIAG_TIME,:DIAG_DESC,:DIAG_SOURCE,:ECG_PATH,:ECG_URL,:NAME,:SEX,:AGE,:ECG_PATIENT_ID,:ECG_DATA,:PDF_DATA)";
                }
                #endregion 
                OracleParameter[] param = new OracleParameter[]{
                            new OracleParameter("ID",OracleDbType.Varchar2),
                            new OracleParameter("PATIENT_NO",OracleDbType.Varchar2),
                            new OracleParameter("EXAM_TIME",OracleDbType.Date),
                            new OracleParameter("REPORT_TIME",OracleDbType.Date),
                            new OracleParameter("DIAG_TIME",OracleDbType.Date),
                            new OracleParameter("DIAG_DESC",OracleDbType.Varchar2),
                            new OracleParameter("DIAG_SOURCE",OracleDbType.Varchar2),
                            new OracleParameter("ECG_PATH",OracleDbType.Varchar2),
                            new OracleParameter("ECG_URL",OracleDbType.Varchar2),
                            new OracleParameter("NAME",OracleDbType.Varchar2),
                            new OracleParameter("SEX",OracleDbType.Varchar2),
                            new OracleParameter("AGE",OracleDbType.Varchar2),
                            new OracleParameter("ECG_PATIENT_ID",OracleDbType.Varchar2),
                            new OracleParameter("ECG_DATA",OracleDbType.Clob),
                            new OracleParameter("PDF_DATA",OracleDbType.Clob)
                        };
                param[0].Value = ecgInfo.Id;
                param[1].Value = ecgInfo.PatientNo;
                param[2].Value = ecgInfo.ExamTime;
                param[3].Value = ecgInfo.ReportTime;
                param[4].Value = ecgInfo.DiagTime;
                param[5].Value = ecgInfo.DiagDesc;
                param[6].Value = ecgInfo.DiagSource;
                param[7].Value = ecgInfo.EcgPath;
                param[8].Value = ecgInfo.EcgUrl;
                param[9].Value = ecgInfo.Name;
                param[10].Value = ecgInfo.Sex;
                param[11].Value = ecgInfo.Age;
                param[12].Value = ecgInfo.EcgPatientId;
                param[13].Value = ecgInfo.EcgData;
                param[14].Value = ecgInfo.PdfData;
                return OracleHelper.ExecuteNonQuery(OracleHelper.connectionString, CommandType.Text, strSql, param) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
