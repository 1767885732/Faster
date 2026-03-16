using FastFrame.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DoCare.Mdsd.Emergency.Entities
{
    [Table("PHEP_AUTO_TIMES")]
    public class AutoTime : IEntity<string>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Column("TIME_ID")]
        public string Id { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        [Column("PATIENT_ID")]
        public string PatientId { get; set; }
        /// <summary>
        /// 院前任务ID或自行来院患者的挂号ID
        /// </summary>
        [Column("MISSION_ID")]
        public string MISSIONID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("TIME_FLAG")]
        public decimal? TimeFlag { get; set; }
        /// <summary>
        /// 腕带ID
        /// </summary>
        [Column("TAG_ID")]
        public string TagId { get; set; }
        /// <summary>
        /// 绑定腕带时间
        /// </summary>
        [Column("BIND_TAG_TIME")]
        public DateTime? BindTagTime { get; set; }
        /// <summary>
        /// 首次接触患者时间
        /// </summary>
        [Column("TOUCH_PATIENT_TIME")]
        public DateTime? TouchPatientTime { get; set; }
        /// <summary>
        /// 进入导管室时间 
        /// </summary>
        [Column("CONDUIT_TIME")]
        public DateTime? ConduitTime { get; set; }
        /// <summary>
        /// 离开导管室时间
        /// </summary>
        [Column("LEFT_CONDUIT_TIME")]
        public DateTime? LeftConduitTime { get; set; }
        /// <summary>
        /// 进入抢救室时间 
        /// </summary>
        [Column("RESCUE_TIME")]
        public DateTime? RescueTime { get; set; }
        /// <summary>
        /// 离开抢救室时间
        /// </summary>
        [Column("LEFT_RESCUE_TIME")]
        public DateTime? LeftRescueTime { get; set; }
        /// <summary>
        /// 进入CCU时间
        /// </summary>
        [Column("ARRIVED_CCU_TIME")]
        public DateTime? ArrivedCCUTime { get; set; }
        /// <summary>
        /// 离开CCU时间
        /// </summary>
        [Column("LEFT_CCU_TIME")]
        public DateTime? LeftCCUTime { get; set; }
        /// <summary>
        /// 进入CT室时间
        /// </summary>
        [Column("ARRIVED_CT_TIME")]
        public DateTime? ArrivedCTTime { get; set; }
        /// <summary>
        /// 离开CT室时间
        /// </summary>
        [Column("LEFT_CT_TIME")]
        public DateTime? LeftCTTime { get; set; }
        /// <summary>
        /// 进入手术室时间
        /// </summary>
        [Column("ARRIVED_OPER_TIME")]
        public DateTime? ArrivedOperTime { get; set; }
        /// <summary>
        /// 离开手术室时间
        /// </summary>
        [Column("LEFT_OPER_TIME")]
        public DateTime? LeftOperTime { get; set; }
        /// <summary>
        /// 进入卒中单元时间
        /// </summary>
        [Column("ARRIVED_STROKE_TIME")]
        public DateTime? ArrivedStrokeTime { get; set; }
        /// <summary>
        /// 离开卒中单元
        /// </summary>
        [Column("LEFT_STROKE_TIME")]
        public DateTime? LeftStrokeTime { get; set; }
        /// <summary>
        /// 进入ICU时间
        /// </summary>
        [Column("ARRIVED_ICU_TIME")]
        public DateTime? ArrivedICUTime { get; set; }
        /// <summary>
        /// 离开ICU时间
        /// </summary>
        [Column("LEFT_ICU_TIME")]
        public DateTime? LeftICUTime { get; set; }
        [Column("CUSTOM_FIELD1")]
        public DateTime? CustomField1 { get; set; }
        [Column("CUSTOM_FIELD2")]
        public DateTime? CustomField2 { get; set; }
        [Column("CUSTOM_FIELD3")]
        public DateTime? CustomField3 { get; set; }
        [Column("CUSTOM_FIELD4")]
        public DateTime? CustomField4 { get; set; }
        [Column("CUSTOM_FIELD5")]
        public DateTime? CustomField5 { get; set; }
        [Column("CUSTOM_FIELD6")]
        public DateTime? CustomField6 { get; set; }
        [Column("CUSTOM_FIELD7")]
        public DateTime? CustomField7 { get; set; }
        [Column("CUSTOM_FIELD8")]
        public DateTime? CustomField8 { get; set; }
        [Column("CUSTOM_FIELD9")]
        public DateTime? CustomField9 { get; set; }
        [Column("CUSTOM_FIELD10")]
        public DateTime? CustomField10 { get; set; }
        [Column("CUSTOM_FIELD11")]
        public string CustomField11 { get; set; }
        [Column("CUSTOM_FIELD12")]
        public string CustomField12 { get; set; }
        [Column("CUSTOM_FIELD13")]
        public string CustomField13 { get; set; }
        [Column("CUSTOM_FIELD14")]
        public string CustomField14 { get; set; }
        [Column("CUSTOM_FIELD15")]
        public string CustomField15 { get; set; }
        [Column("CREATE_TIME")]
        public DateTime? CreateTime { get; set; }
        [Column("CREATE_BY")]
        public string CreateBy { get; set; }
        [Column("UPDATE_TIME")]
        public DateTime? UpdateTime { get; set; }
        [Column("UPDATE_BY")]
        public string UpdateBy { get; set; }


    }
}
