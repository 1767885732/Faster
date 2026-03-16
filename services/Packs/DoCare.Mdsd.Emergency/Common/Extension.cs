using DoCare.Mdsd.Emergency.Entities;
using DoCare.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Mdsd.Emergency.Common
{
    public static class Extension
    {
        public static string ToComingType(this ViewCpcEmergencyInfo cpcEmerInfo, ViewCpcPatientInfo cpcPatientInfo)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(cpcEmerInfo?.COMINGTYPE))
            {
                if (cpcEmerInfo.COMINGTYPE == ComingType.Inhospital.ToString("d"))
                {
                    result = ComingType.Self.ToString("d");
                }
                else
                {
                    result = ComingType.FirstAid120.ToString("d");
                }
            }
            else
            {
                result = string.IsNullOrEmpty(cpcPatientInfo?.PHEPPATIENTID) ? ComingType.Self.ToString("d") : ComingType.FirstAid120.ToString("d");
            }
            return result;
        }
    }
}
