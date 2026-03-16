using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class CarUserInfo
    {
        public bool isSucceed { get; set; }
        public string msg { get; set; }
        public List<UserList> resultData { get; set; }
    }

    public class UserList 
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        public string HZBH { get; set; }

        /// <summary>
        /// 受理时间
        /// </summary>
        public string SLSJ { get; set; }

        /// <summary>
        /// 主诉症状
        /// </summary>
        public string HJYY_NAME { get; set; }

        /// <summary>
        /// 接车地址
        /// </summary>
        public string JCDD { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string HZXM { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string HZXB { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string HZNL { get; set; }

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CLMC { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CPH { get; set; }

        /// <summary>
        /// 送往医院编码
        /// </summary>
        public string SWDD { get; set; }

        /// <summary>
        /// 送往医院名称
        /// </summary>
        public string SWDD_NAME { get; set; }

        /// <summary>
        /// 呼救时间
        /// </summary>
        public string HJSJ { get; set; }

        /// <summary>
        /// 派车时间
        /// </summary>
        public string PCSJ { get; set; }

        /// <summary>
        /// 出车时间
        /// </summary>
        public string CCSJ { get; set; }

        /// <summary>
        /// 到达现场时间
        /// </summary>
        public string DDXCSJ { get; set; }

        /// <summary>
        /// 上车时间
        /// </summary>
        public string SCSJ { get; set; }

        /// <summary>
        /// 到达医院时间
        /// </summary>
        public string SWSJ { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public string WCSJ { get; set; }

        /// <summary>
        /// 返站时间
        /// </summary>
        public string FZSJ { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DOCTOR_NAME { get; set; }

        /// <summary>
        /// 医生手机
        /// </summary>
        public string DOCTOR_PHONE { get; set; }

        /// <summary>
        /// 护士姓名
        /// </summary>
        public string NURSE_NAME { get; set; }

        /// <summary>
        /// 护士手机
        /// </summary>
        public string NURSE_PHONE { get; set; }

        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DRIVE_NAME { get; set; }

        /// <summary>
        /// 驾驶员手机
        /// </summary>
        public string DRIVE_PHONE { get; set; }

        /// <summary>
        /// 做心电图的时间(需要心电系统的支持)
        /// </summary>
        public string ECG_TIME { get; set; }

        /// <summary>
        /// 心电图url地址(需要心电系统的支持)
        /// </summary>
        public string ECG_URL { get; set; }
    }
}
