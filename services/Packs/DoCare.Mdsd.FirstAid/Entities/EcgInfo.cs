using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>
    /// 心电图信息
    /// </summary>
    public class EcgInfo
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 就诊ID或者急救任务ID
        /// </summary>
        public string VisitId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 心电图描述
        /// </summary>
        public string EcgDesc { get; set; }
        /// <summary>
        /// 心电图图片类型（bmp,png,jpg等）
        /// </summary>
        public string EcgFileType { get; set; }
        /// <summary>
        /// 心电图来源 1 院前救护车 2 院内急诊抢救室 3.急诊  4.门诊  5.住院
        /// </summary>
        public string EcgFlag { get; set; }
        /// <summary>
        /// 主键GUID
        /// </summary>
        public string EcgId { get; set; }
        /// <summary>
        /// 心电图图片
        /// </summary>
        public string EcgPic { get; set; }
        /// <summary>
        /// 心电图采集状态 1新建 2未采集 3已采集未上传 4已采集已上传
        /// </summary>
        public string EcgStatus { get; set; }
        /// <summary>
        /// 心电图时间
        /// </summary>
        public DateTime? EcgTime { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string HospitalId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string HospitalName { get; set; }
        /// <summary>
        /// 患者年龄
        /// </summary>
        public string PatientAge { get; set; }
        /// <summary>
        /// 患者性别
        /// </summary>
        public string PatientGender { get; set; }


    }
}
