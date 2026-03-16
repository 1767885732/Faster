using DoCare.Core.Ecg.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DoCare.EcgConversion.Common
{
    [Dependency(ServiceLifetime.Scoped)]
    public class OracleCRUD
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public OracleCRUD(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        private static OracleConnection CrateOracleConnection(string connectionString)
        {
            var connection = new OracleConnection(connectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public bool InsertEcgInfo(MedPatEcgInfo ecgInfo, string connectionString)
        {
            using (OracleConnection conn = CrateOracleConnection(connectionString))
            {
                try
                {
                    string sqlText = @"INSERT INTO MED_PAT_ECG_INFO (ID,PATIENT_NO,EXAM_TIME,REPORT_TIME,DIAG_TIME,DIAG_DESC,DIAG_SOURCE,ECG_PATH,ECG_URL,NAME,SEX,AGE,ECG_PATIENT_ID,ECG_DATA)
VALUES (:ID,:PATIENT_NO,:EXAM_TIME,:REPORT_TIME,:DIAG_TIME,:DIAG_DESC,:DIAG_SOURCE,:ECG_PATH,:ECG_URL,:NAME,:SEX,:AGE,:ECG_PATIENT_ID,:ECG_DATA)";
                    using (OracleCommand cmd = new OracleCommand(sqlText, conn))
                    {
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
                            new OracleParameter("ECG_DATA",OracleDbType.Clob)
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
                        cmd.Parameters.AddRange(param);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<MedPatEcgInfo> GetMedPatEcgInfoList(string patientNo, string connectionString)
        {
            List<MedPatEcgInfo> list = new List<MedPatEcgInfo>();
            DataSet ds = new DataSet();

            using (OracleConnection connection = CrateOracleConnection(connectionString))
            {
                string sqlText = @"SELECT ID,PATIENT_NO,EXAM_TIME,REPORT_TIME,DIAG_TIME,DIAG_DESC,DIAG_SOURCE,ECG_PATH,ECG_URL,NAME,SEX,AGE,ECG_PATIENT_ID,ECG_DATA,PDF_DATA 
FROM MED_PAT_ECG_INFO WHERE PATIENT_NO=:PATIENT_NO";
                OracleParameter[] param = new OracleParameter[]{
                            new OracleParameter("PATIENT_NO",OracleDbType.Varchar2)
                        };
                param[0].Value = patientNo;

                OracleCommand cmd = new OracleCommand(sqlText, connection);
                cmd.Parameters.AddRange(param);

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (OracleException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    MedPatEcgInfo ecgInfo = new MedPatEcgInfo();
                    ecgInfo.Id = Guid.Parse(row["ID"].ToString());
                    ecgInfo.PatientNo = row["PATIENT_NO"].ToString();
                    ecgInfo.ExamTime = Convert.IsDBNull(row["EXAM_TIME"]) ? null : row["EXAM_TIME"].ToString().ToDateTime();
                    ecgInfo.ReportTime = Convert.IsDBNull(row["REPORT_TIME"]) ? null : row["REPORT_TIME"].ToString().ToDateTime();
                    ecgInfo.DiagTime = Convert.IsDBNull(row["DIAG_TIME"]) ? null : row["DIAG_TIME"].ToString().ToDateTime();
                    ecgInfo.DiagDesc = Convert.IsDBNull(row["DIAG_DESC"]) ? null : row["DIAG_DESC"].ToString();
                    ecgInfo.DiagSource = Convert.IsDBNull(row["DIAG_SOURCE"]) ? null : row["DIAG_SOURCE"].ToString();
                    ecgInfo.EcgPath = Convert.IsDBNull(row["ECG_PATH"]) ? null : row["ECG_PATH"].ToString();
                    ecgInfo.EcgUrl = Convert.IsDBNull(row["ECG_URL"]) ? null : row["ECG_URL"].ToString();
                    ecgInfo.Name = Convert.IsDBNull(row["NAME"]) ? null : row["NAME"].ToString();
                    ecgInfo.Sex = Convert.IsDBNull(row["SEX"]) ? null : row["SEX"].ToString();
                    ecgInfo.Age = Convert.IsDBNull(row["AGE"]) ? null : row["AGE"].ToString();
                    ecgInfo.EcgPatientId = Convert.IsDBNull(row["ECG_PATIENT_ID"]) ? null : row["ECG_PATIENT_ID"].ToString();
                    ecgInfo.EcgData = Convert.IsDBNull(row["ECG_DATA"]) ? null : row["ECG_DATA"].ToString();
                    ecgInfo.PdfData = Convert.IsDBNull(row["PDF_DATA"]) ? null : row["PDF_DATA"].ToString();
                    list.Add(ecgInfo);
                }
            }
            return list;
        }

        public void UpdateEcgData(List<MedPatEcgInfo> list, string connectionString)
        {
            using (OracleConnection conn = CrateOracleConnection(connectionString))
            {
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    string cmdText = @"UPDATE MED_PAT_ECG_INFO SET ECG_DATA=:ECG_DATA WHERE ID=:ID";
                    OracleCommand cmd = new OracleCommand(cmdText, conn);
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;//cmdType;
                    try
                    {
                        foreach (var ecgInfo in list)
                        {
                            OracleParameter[] param = new OracleParameter[]{
                                new OracleParameter("ECG_DATA",OracleDbType.Clob),
                                new OracleParameter("ID",OracleDbType.Varchar2)
                            };
                            param[0].Value = ecgInfo.EcgData;
                            param[1].Value = ecgInfo.Id;

                            cmd.Parameters.AddRange(param);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
