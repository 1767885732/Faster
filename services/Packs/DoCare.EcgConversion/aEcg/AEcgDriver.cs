using ECGConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.EcgConversion.aEcg
{
    public class AEcgDriver : IEcgDriver
    {
        /// <summary>
        /// 读取aEcg格式心电图数据
        /// </summary>
        /// <param name="fullPath">文件完整路径</param>
        /// <returns></returns>
        public IECGFormat Read(string fullPath)
        {
            IECGFormat format = null;
            using (var stream = System.IO.File.OpenRead(fullPath))
            {
                if (stream != null)
                {
                    UnknownECGReader reader = new UnknownECGReader();
                    format = reader.Read(stream);
                }
            }
            return format;
        }
    }
}
