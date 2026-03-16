using ECGConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.EcgConversion.GeMac800
{
    public class GeMac800Driver : IEcgDriver
    {
        private readonly Hl7Tool hl7Tool;

        public GeMac800Driver(Hl7Tool hl7Tool)
        {
            this.hl7Tool = hl7Tool;
        }

        /// <summary>
        /// 读取GeMac800型号心电图数据
        /// </summary>
        /// <param name="fullPath">文件完整路径</param>
        /// <returns></returns>
        public IECGFormat Read(string fullPath)
        {
            IECGFormat format = null;
            var hl7String = hl7Tool.ToHl7String(fullPath);
            if (hl7String.Item1 && !string.IsNullOrWhiteSpace(hl7String.Item2))
            {
                UnknownECGReader reader = new UnknownECGReader();
                format = reader.Read(System.Text.Encoding.Default.GetBytes(hl7String.Item2));
            }
            return format;
        }
    }
}
