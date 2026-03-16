using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DoCare.EcgConversion.GeMac800
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class Hl7Tool
    {
        private readonly EcgConversionManager ecgConversionManager;
        public Hl7Tool(EcgConversionManager ecgConversionManager)
        {
            this.ecgConversionManager = ecgConversionManager;
        }

        public Tuple<bool, string> ToHl7String(string filePath)
        {
            var result = new Tuple<bool, string>(false, "转换未开始");

            if (!File.Exists(filePath))
            {
                result = new Tuple<bool, string>(false, "请选择正确的Mac 800 心电图文件!");
                return result;
            }

            EcgInfo ecgEntity = ReadMac800Ecg(filePath);
            string strEcg = ecgConversionManager.ConvertToHl7aEcg(ecgEntity);
            result = new Tuple<bool, string>(true, strEcg);
            return result;
        }

        private EcgInfo ReadMac800Ecg(string strEcgFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strEcgFile);
            XmlNamespaceManager xmlns = new XmlNamespaceManager(doc.NameTable);
            xmlns.AddNamespace("ge", "urn:ge:sapphire:dcar_1");

            EcgInfo ecgEntity = new EcgInfo();
            ecgEntity.EcgId = Guid.NewGuid().ToString();
            ecgEntity.TrialSubjectExtension = Guid.NewGuid().ToString();
            ecgEntity.SoftwareName = "MDSD_EcgToolkit_Conversion";
            ecgEntity.ManufacturerOrganization = "MDSD";
            var curEcgNode = doc.ChildNodes[1].ChildNodes[1];

            XmlNode nameNode = doc.SelectSingleNode("//ge:given", xmlns);
            if (nameNode != null)
            {
                //throw new ApplicationException("xml格式错误!");
                ecgEntity.PatientName = nameNode.Attributes["V"].InnerText;
            }

            XmlNode ecgWave = doc.SelectSingleNode("//ge:ecgWaveformMXG", xmlns);
            if (ecgWave == null)
                throw new ApplicationException("xml格式错误!");

            string scale = ecgWave.Attributes["S"].InnerText;
            ecgEntity.Scale = scale;

            //获取采样率
            var sampleRateNode = ecgWave.SelectSingleNode("ge:sampleRate", xmlns);
            if (ecgWave == null)
                throw new ApplicationException("xml格式错误!");
            int sampleRate = int.Parse(sampleRateNode.Attributes["V"].InnerText);

            #region 获取波形数据
            var waveList = ecgWave.SelectNodes("ge:ecgWaveform", xmlns);
            foreach (XmlNode node in waveList)
            {
                string lead = node.Attributes["lead"].InnerText;
                string leadValue = node.Attributes["V"].InnerText;

                switch (lead)
                {
                    case "I":
                        ecgEntity.MDC_ECG_LEAD_IR = leadValue;
                        break;
                    case "II":
                        ecgEntity.MDC_ECG_LEAD_IIR = leadValue;
                        break;
                    case "III":
                        ecgEntity.MDC_ECG_LEAD_IIIR = leadValue;
                        break;
                    case "AVR":
                        ecgEntity.MDC_ECG_LEAD_aVR = leadValue;
                        break;
                    case "AVL":
                        ecgEntity.MDC_ECG_LEAD_aVL = leadValue;
                        break;
                    case "AVF":
                        ecgEntity.MDC_ECG_LEAD_aVF = leadValue;
                        break;
                    case "V1":
                        ecgEntity.MDC_ECG_LEAD_V1 = leadValue;
                        break;
                    case "V2":
                        ecgEntity.MDC_ECG_LEAD_V2 = leadValue;
                        break;
                    case "V3":
                        ecgEntity.MDC_ECG_LEAD_V3 = leadValue;
                        break;
                    case "V4":
                        ecgEntity.MDC_ECG_LEAD_V4 = leadValue;
                        break;
                    case "V5":
                        ecgEntity.MDC_ECG_LEAD_V5 = leadValue;
                        break;
                    case "V6":
                        ecgEntity.MDC_ECG_LEAD_V6 = leadValue;
                        break;
                }
            }
            #endregion

            //获取心电图采集时间
            XmlNode acquTime = doc.SelectSingleNode("//ge:acquisitionDateTime", xmlns);
            if (acquTime == null)
                new ApplicationException("xml格式错误!");
            string strTempTime = acquTime.Attributes["V"].InnerText;
            DateTime tempTime = DateTime.Parse(strTempTime);
            ecgEntity.BeginEffectiveTime = tempTime.ToString("yyyyMMddhhmmss");
            string[] rateArray = ecgEntity.MDC_ECG_LEAD_IR.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            tempTime.AddSeconds(rateArray.Length / sampleRate);
            ecgEntity.EndEffectiveTime = tempTime.ToString("yyyyMMddhhmmss");
            return ecgEntity;
        }
    }
}
