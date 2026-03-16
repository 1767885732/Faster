using FastFrame.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoCare.Core.Patient.Entities
{
    /// <summary>
    /// 急诊急救时间信息
    /// </summary>
    [Description("急诊急救时间信息")]
    public class PhepAutoTimes : EntityBase<string>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 院前任务ID或自行来院患者的挂号ID
        /// </summary>
        public string MISSIONID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TimeFlag { get; set; }
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
        /// 影像学开单时间
        /// </summary>
        public DateTime? CtOpenTime { get; set; }
        /// <summary>
        /// 影像学报告时间
        /// </summary>
        public DateTime? CtResultTime { get; set; }
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
        public DateTime? CustomField1 { get; set; }
        public DateTime? CustomField2 { get; set; }
        public DateTime? CustomField3 { get; set; }
        public DateTime? CustomField4 { get; set; }
        public DateTime? CustomField5 { get; set; }
        public DateTime? CustomField6 { get; set; }
        public DateTime? CustomField7 { get; set; }
        public DateTime? CustomField8 { get; set; }
        public DateTime? CustomField9 { get; set; }
        public DateTime? CustomField10 { get; set; }
        public string CustomField11 { get; set; }
        public string CustomField12 { get; set; }
        public string CustomField13 { get; set; }
        public string CustomField14 { get; set; }
        public string CustomField15 { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateBy { get; set; }
    }
}
