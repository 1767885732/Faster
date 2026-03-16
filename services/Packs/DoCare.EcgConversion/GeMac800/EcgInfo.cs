using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.EcgConversion.GeMac800
{
    /// <summary>
    /// 心电图数据
    /// </summary>
    public class EcgInfo
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string EcgId { get; set; }

        public string BeginEffectiveTime { get; set; }

        public string EndEffectiveTime { get; set; }

        public string PatientName { get; set; }

        public string Scale { get; set; }

        /// <summary>
        /// GUID
        /// </summary>
        public string TrialSubjectExtension { get; set; }

        public string SoftwareName { get; set; }

        public string ManufacturerOrganization { get; set; }

        public string MDC_ECG_LEAD_IR { get; set; }

        public string MDC_ECG_LEAD_IIR { get; set; }
        public string MDC_ECG_LEAD_IIIR { get; set; }
        public string MDC_ECG_LEAD_aVR { get; set; }
        public string MDC_ECG_LEAD_aVL { get; set; }
        public string MDC_ECG_LEAD_aVF { get; set; }
        public string MDC_ECG_LEAD_V1 { get; set; }
        public string MDC_ECG_LEAD_V2 { get; set; }
        public string MDC_ECG_LEAD_V3 { get; set; }
        public string MDC_ECG_LEAD_V4 { get; set; }
        public string MDC_ECG_LEAD_V5 { get; set; }
        public string MDC_ECG_LEAD_V6 { get; set; }

    }
}
