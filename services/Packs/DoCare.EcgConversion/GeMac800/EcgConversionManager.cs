using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DoCare.EcgConversion.GeMac800
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class EcgConversionManager
    {
        /// <summary>把心电图实体数据转换为hl7 aecg标准格式心电图数据</summary>
        /// <param name="ecgEntity"></param>
        /// <returns></returns>
        public string ConvertToHl7aEcg(EcgInfo ecgEntity)
        {
            Stream xmlStream = GetType().Assembly.GetManifestResourceStream("DoCare.EcgConversion.GeMac800.hl7ecgtemplate.xml");
            if (xmlStream == null)
                throw new ApplicationException("未能找到HL7 aEcg模板!");

            string strEcg = string.Empty;
            using (StreamReader sr = new StreamReader(xmlStream))
            {
                strEcg = sr.ReadToEnd();

            }
            xmlStream.Dispose();
            return MergeEcgDataToTemplate(ecgEntity, strEcg);
        }

        private string MergeEcgDataToTemplate(EcgInfo ecgEntity, string strEcg)
        {
            var ecgEntityAttr = ecgEntity.GetType().GetProperties();
            foreach (var attr in ecgEntityAttr)
            {
                string strName = attr.Name;
                var ecgValue = attr.GetValue(ecgEntity, null);
                string strValue = string.Empty;
                if (ecgValue != null)
                    strValue = ecgValue.ToString();

                strEcg = strEcg.Replace("@@" + strName, strValue);
            }
            return strEcg;
        }

    }
}
