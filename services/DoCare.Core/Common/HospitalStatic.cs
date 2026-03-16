using FastFrame.Core;
using FastFrame.Data;
using Microsoft.Extensions.Configuration;

namespace DoCare.Core.Common
{
    public static class HospitalStatic
    {
        /// <summary>
        /// 机构编号
        /// </summary>
        public static string OrgCode => Singleton<FastFrameOptions>.Instance.CustomField["OrgCode"];

        /// <summary>
        /// 医院编号
        /// </summary>
        public static string HospitalNo => Singleton<FastFrameOptions>.Instance.CustomField["HospitalNo"];
    }
}
