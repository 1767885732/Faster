using System;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>任务</summary>
    public class VMMission
    {
        /// <summary>主键</summary>
        public string MissionId { get; set; }

        /// <summary>呼救原因</summary>
        public string ReasonType { get; set; }

        /// <summary>车载护士</summary>
        public string NurseName { get; set; }

        /// <summary>120主键</summary>
        public string Id120 { get; set; }   

        /// <summary>出车分站</summary>
        public string DispatchVehicleSite { get; set; }

        /// <summary>任务类型</summary>
        public string MissionType { get; set; }

        /// <summary>车载司机</summary>
        public string DriverName { get; set; }

        /// <summary>车载医生</summary>
        public string DoctorName { get; set; }

        /// <summary>出车时间</summary>
        public DateTime? DispatchVehicleTime { get; set; }

        /// <summary>备忘</summary>
        public string Memo { get; set; }     

        /// <summary>救治目的地</summary>
        public string DestinationAddress { get; set; }

        /// <summary>联系人</summary>
        public string Contact { get; set; }

        /// <summary>联系电话</summary>
        public string ContactPhone { get; set; }

        /// <summary>派车时间</summary>
        public DateTime? PlanVehicleTime { get; set; }

        /// <summary>呼叫时间</summary>
        public DateTime? CallTime { get; set; }

        /// <summary>接收任务时间</summary
        public DateTime? AcceptMissionTime { get; set; }

        /// <summary>到达医院时间</summary>
        public DateTime? ArrivedHospitalTime { get; set; }

        /// <summary>到达现场时间</summary>
        public DateTime? ArrivedSceneTime { get; set; }

        /// <summary>接触患者时间</summary>
        public DateTime? TouchPatientTime { get; set; }

        /// <summary>离开现场时间</summary>
        public DateTime? LeftSceneTime { get; set; }




        /// <summary>来源类型 0 120安克建立，1自己创建任务 </summary>
        public decimal? FromType { get; set; }
        public string FromTypeText { get; set; }

        /// <summary>所属医院ID</summary>
        public string HospitalId { get; set; }
        public string HospitalName { get; set; }

        /// <summary>是否需要担架</summary>
        public decimal? NeedStretcher { get; set; }
        public string NeedStretcherText { get; set; }

        /// <summary>事件</summary>
        public string IncidentId { get; set; }
        public string IncidentName { get; set; }

        /// <summary>状态（准备出车0,赶往现场1,到达现场2,回院途中3,到达医院4，完成急救5,任务取消8,删除9）</summary>
        public string Status { get; set; }
        /// <summary>状态描述</summary>
        public string StatusText { get; set; }

        /// <summary>车辆ID</summary>
        public string VehicleId { get; set; }
        /// <summary>车牌号</summary>
        public string VehicleNumber { get; set; }
    }
}
