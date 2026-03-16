using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Operation.Entities
{
    /// <summary>
    /// 手术表
    /// </summary>
    [Description("手术表")]
    public class MedPatOperation : EntityBase<string>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(60)]
        public string PatientNo { get; set; }
        /// <summary>

        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime? ScheduledDateTime { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        [MaxLength(60)]
        public string AnesthesiaMethod { get; set; }
        /// <summary>
        /// 主刀医生
        /// </summary>
        [MaxLength(60)]
        public string Surgeon { get; set; }
        /// <summary>
        /// 麻醉医生
        /// </summary>
        [MaxLength(60)]
        public string AnesthesiaDoctor { get; set; }
        /// <summary>
        /// 术前诊断
        /// </summary>
        [MaxLength(500)]
        public string DiagBeforeOperation { get; set; }
        /// <summary>
        /// 术后诊断
        /// </summary>
        [MaxLength(500)]
        public string DiagAfterOperation { get; set; }
        /// <summary>
        /// 手术等级
        /// </summary>
        [MaxLength(60)]
        public string OperationScale { get; set; }
        /// <summary>
        /// 进入手术室时间
        /// </summary>
        [MaxLength(60)]
        public DateTime? InDateTime { get; set; }
        /// <summary>
        /// 出手术室时间
        /// </summary>
        public DateTime? OutDateTime { get; set; }
        /// <summary>
        /// 麻醉开始时间
        /// </summary>
        public DateTime? AnesStartTime { get; set; }
        /// <summary>
        /// 麻醉结束时间
        /// </summary>
        public DateTime? AnesEndTime { get; set; }
        /// <summary>
        /// 手术开始时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 手术结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 复苏开始时间
        /// </summary>
        public DateTime? InPacuDateTime { get; set; }
        /// <summary>
        /// 复苏结束时间
        /// </summary>
        public DateTime? OutPacuDateTime { get; set; }
        /// <summary>
        /// 失血量
        /// </summary>
        public decimal? BloodLossed { get; set; }
        /// <summary>
        /// 输血量
        /// </summary>
        public decimal? BloodTransfered { get; set; }
        /// <summary>
        /// 手术名称
        /// </summary>
        [MaxLength(200)]
        public string OperationName { get; set; }
        /// <summary>
        /// 手术部位
        /// </summary>
        [MaxLength(60)]
        public string OperationPart { get; set; }
        /// <summary>
        /// 器械护士
        /// </summary>
        [MaxLength(60)]
        public string OperNurse { get; set; }
        /// <summary>
        /// 巡回护士
        /// </summary>
        [MaxLength(60)]
        public string SupplyNurse { get; set; }
    }
}
