using DoCare.Core.Common;
using DoCare.Core.Ecg.Entities;
using DoCare.EcgConversion.Common;
using ECGConversion;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace DoCare.EcgConversion
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class EcgConversionService
    {
        private readonly ILogger<EcgConversionService> logger;
        private readonly IEcgDriver ecgDriver;
        private readonly EcgImgConversion ecgImgConversion;
        private readonly OracleCRUD oracleCRUD;
        private readonly string connectionString;

        public EcgConversionService(ILogger<EcgConversionService> logger,
            IEcgDriver ecgDriver,
            EcgImgConversion ecgImgConversion,
            OracleCRUD oracleCRUD)
        {
            this.logger = logger;
            this.ecgDriver = ecgDriver;
            this.oracleCRUD = oracleCRUD;
            this.ecgImgConversion = ecgImgConversion;
            connectionString = Singleton<FastFrameOptions>.Instance.GetDbOption(typeof(DefaultDbContext)).ConnectionString;
        }

        public bool UpdateEcgDataFromPdfData(SyncPatientDto dto)
        {
            var res = false;
            if (string.IsNullOrEmpty(dto.PatientNo)) return res;
            logger.LogDebug($"心电图转换-PDF转图片 开始");

            List<MedPatEcgInfo> ecgList = new List<MedPatEcgInfo>();
            try
            {
                ecgList = oracleCRUD.GetMedPatEcgInfoList(dto.PatientNo, connectionString);
                //logger.LogDebug($"心电图转换-PDF转图片 数据源:{ecgList.ToJson()}");

                ecgList.ForEach(o =>
                {
                    string base64 = PdfHelper.PdfToImage(o.PdfData);
                    //logger.LogDebug($"心电图转换-PDF转图片 转换结果:{base64}");
                    if (!string.IsNullOrEmpty(base64))
                    {
                        o.EcgData = base64;
                    }
                });
                //logger.LogDebug($"心电图转换-PDF转图片 转换后:{ecgList.ToJson()}");

            }
            catch (Exception ex)
            {
                logger.LogError($"心电图转换-PDF转图片 数据转换错误:{ex.Message}");
            }

            try
            {
                if (ecgList.Count > 0)
                {
                    oracleCRUD.UpdateEcgData(ecgList, connectionString);
                    logger.LogDebug($"心电图转换-PDF转图片 更新结束,共更新{ecgList.Count}行");
                }
                res = true;
            }
            catch (Exception ex)
            {
                logger.LogError($"心电图转换-PDF转图片 数据同步错误:{ex.Message}");
            }
            return res;
        }

        /// <summary>
        /// 同步心电图文件
        /// </summary>
        /// <param name="fullPath">文件完整路径</param>
        /// <returns></returns>
        public bool SyncEcgFile(string fullPath)
        {
            bool result = false;
            if (ecgDriver is IEcgDriver)
            {
                MedPatEcgInfo ecgInfo = new MedPatEcgInfo();
                //读取心电图对象
                IECGFormat format = ecgDriver.Read(fullPath);
                if (format != null)
                {
                    //转换实体
                    ecgInfo = MapToMedPatEcgInfo(format);
                    //解析心电图图片
                    Bitmap ecgImg = ecgImgConversion.ConvertToBitmap(format);
                    ecgInfo.EcgData = ImgToBase64(ecgImg);
                    if (ecgImg != null)
                    {
                        ecgImg.Dispose();
                    }
                    ecgInfo.EcgPath = Path.GetFileName(fullPath);
                    //入库
                    result = oracleCRUD.InsertEcgInfo(ecgInfo, connectionString);
                }
                else
                {
                    throw new Exception("文件解析失败,format=null");
                }
            }
            return result;
        }

        /// <summary>
        /// Bitmap图片转换成Base64字符串
        /// </summary>
        /// <param name="bitmap">Bitmap图片</param>
        /// <returns></returns>
        private string ImgToBase64(Bitmap bitmap)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                ImageFormat format;
                if (bitmap.RawFormat.Equals(ImageFormat.MemoryBmp))
                {
                    format = ImageFormat.Png;
                }
                else
                {
                    format = bitmap.RawFormat;
                }
                bitmap.Save(ms, format);
                //bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                logger.LogError($"心电图图片转Base64错误,错误信息:{ex.Message}");
                return null;
            }
        }

        private MedPatEcgInfo MapToMedPatEcgInfo(IECGFormat format)
        {
            MedPatEcgInfo ecgInfo = new MedPatEcgInfo();
            ecgInfo.PatientNo = null;
            ecgInfo.ExamTime = format.Demographics.TimeAcquisition;
            ecgInfo.ReportTime = null;
            ecgInfo.DiagTime = null;
            ecgInfo.DiagDesc = null;
            ecgInfo.DiagSource = null;
            ecgInfo.EcgPath = null;
            ecgInfo.EcgUrl = null;
            ecgInfo.Name = ConvertName(format);
            ecgInfo.Sex = ConvertSex(format);
            ecgInfo.Age = ConvertAge(format);
            ecgInfo.EcgPatientId = format.Demographics.PatientID;
            ecgInfo.EcgData = null;
            return ecgInfo;
        }

        private string ConvertName(IECGFormat format)
        {
            StringBuilder sb = new StringBuilder();

            if ((format.Demographics.FirstName != null)
                    && (format.Demographics.FirstName.Length != 0))
            {
                sb.Append(format.Demographics.FirstName);
                sb.Append(" ");
            }

            sb.Append(format.Demographics.LastName);

            if ((format.Demographics.SecondLastName != null)
            && (format.Demographics.SecondLastName.Length != 0))
            {
                sb.Append('-');
                sb.Append(format.Demographics.SecondLastName);
            }

            return sb.ToString();
        }

        private string ConvertSex(IECGFormat format)
        {
            string result = "";
            if (format.Demographics.Gender == ECGConversion.ECGDemographics.Sex.Male)
            {
                result = "男";
            }
            else if (format.Demographics.Gender == ECGConversion.ECGDemographics.Sex.Female)
            {
                result = "女";
            }
            return result;
        }

        private string ConvertAge(IECGFormat format)
        {
            StringBuilder sb = new StringBuilder();

            ushort ageVal;
            ECGConversion.ECGDemographics.AgeDefinition ad;
            if (format.Demographics.getPatientAge(out ageVal, out ad) == 0)
            {
                sb.Append(ageVal);

                if (ad != ECGConversion.ECGDemographics.AgeDefinition.Years)
                {
                    sb.Append(" ");
                    sb.Append(ad.ToString());
                }
            }
            else
            {
                ECGConversion.ECGDemographics.Date birthDate = format.Demographics.PatientBirthDate;
                if (birthDate != null)
                {
                    DateTime now = DateTime.Now;
                    int age = now.Year - birthDate.Year;
                    if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                    {
                        age--;
                    }
                    sb.Append(age);
                }
            }
            return sb.ToString();
        }
    }
}
