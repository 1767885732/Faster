using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>
    /// Rfid腕带信息
    /// </summary>
    public class RfidData
    {
        public string EHRID { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 院前任务ID或自行来院患者的挂号ID
        /// </summary>
        public string VisitId { get; set; }
        /// <summary>
        /// 腕带ID
        /// </summary>
        public string TagId { get; set; }
        /// <summary>
        /// 绑定腕带时间
        /// </summary>
        public DateTime? BindTagTime { get; set; }
        /// <summary>
        /// 首次接触患者时间
        /// </summary>
        public DateTime? TouchPatientTime { get; set; }
        /// <summary>
        /// 进入导管室时间 
        /// </summary>
        public DateTime? ConduitTime { get; set; }
        /// <summary>
        /// 离开导管室时间
        /// </summary>
        public DateTime? LeftConduitTime { get; set; }
        /// <summary>
        /// 进入抢救室时间 
        /// </summary>
        public DateTime? RescueTime { get; set; }
        /// <summary>
        /// 离开抢救室时间
        /// </summary>
        public DateTime? LeftRescueTime { get; set; }
        /// <summary>
        /// 进入CCU时间
        /// </summary>
        public DateTime? ArrivedCCUTime { get; set; }
        /// <summary>
        /// 离开CCU时间
        /// </summary>
        public DateTime? LeftCCUTime { get; set; }
        /// <summary>
        /// 进入CT室时间
        /// </summary>
        public DateTime? ArrivedCTTime { get; set; }
        /// <summary>
        /// 离开CT室时间
        /// </summary>
        public DateTime? LeftCTTime { get; set; }
        /// <summary>
        /// 进入手术室时间
        /// </summary>
        public DateTime? ArrivedOperTime { get; set; }
        /// <summary>
        /// 离开手术室时间
        /// </summary>
        public DateTime? LeftOperTime { get; set; }
        /// <summary>
        /// 进入卒中单元时间
        /// </summary>
        public DateTime? ArrivedStrokeTime { get; set; }
        /// <summary>
        /// 离开卒中单元
        /// </summary>
        public DateTime? LeftStrokeTime { get; set; }
        /// <summary>
        /// 进入ICU时间
        /// </summary>
        public DateTime? ArrivedICUTime { get; set; }
        /// <summary>
        /// 离开ICU时间
        /// </summary>
        public DateTime? LeftICUTime { get; set; }

        /// <summary>
        /// 预留字段1
        /// </summary>
        public string CustomField1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        public string CustomField2 { get; set; }
        /// <summary>
        /// 预留字段3
        /// </summary>
        public string CustomField3 { get; set; }
        /// <summary>
        /// 预留字段4
        /// </summary>
        public string CustomField4 { get; set; }
        /// <summary>
        /// 预留字段5
        /// </summary>
        public string CustomField5 { get; set; }
        /// <summary>
        /// 预留字段6
        /// </summary>
        public string CustomField6 { get; set; }
        /// <summary>
        /// 预留字段7
        /// </summary>
        public string CustomField7 { get; set; }
        /// <summary>
        /// 预留字段8
        /// </summary>
        public string CustomField8 { get; set; }
        /// <summary>
        /// 预留字段9
        /// </summary>
        public string CustomField9 { get; set; }
        /// <summary>
        /// 预留字段10
        /// </summary>
        public string CustomField10 { get; set; }
        /// <summary>
        /// 预留字段11
        /// </summary>
        public string CustomField11 { get; set; }
        /// <summary>
        /// 预留字段12
        /// </summary>
        public string CustomField12 { get; set; }
        /// <summary>
        /// 预留字段13
        /// </summary>
        public string CustomField13 { get; set; }
        /// <summary>
        /// 预留字段14
        /// </summary>
        public string CustomField14 { get; set; }
        /// <summary>
        /// 预留字段15
        /// </summary>
        public string CustomField15 { get; set; }
    }
}
