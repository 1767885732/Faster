using System;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>急救交接记录</summary>
    public class VMShiftRecordInfo
    {
        /// <summary></summary>
        public string ShiftRecordId { get; set; }

        /// <summary>脉搏(次/分)</summary>
        public int? P { get; set; }

        /// <summary>意识</summary>
        public string Mentality { get; set; }

        /// <summary>收缩压(mmHg)</summary>
        public int? Sbp { get; set; }

        /// <summary>外周静脉 1是 0否</summary>
        public decimal? ConduitWaizhou { get; set; }

        /// <summary>其他</summary>
        public string OtherInfo { get; set; }

        /// <summary>三无人员标识 1是 0否</summary>
        public bool IdentityFlag { get; set; }

        /// <summary>其他</summary>
        public string ConduitOther { get; set; }

        /// <summary>已用药物</summary>
        public string Medical { get; set; }

        /// <summary>交接时间</summary>
        public DateTime? ShiftTime { get; set; }

        /// <summary>胃管 1是 0否</summary>
        public decimal? ConduitWeiguan { get; set; }

        /// <summary>导尿管 1是 0否</summary>
        public decimal? ConduitDaoniao { get; set; }

        /// <summary>体温</summary>
        public string T { get; set; }

        /// <summary>PICC 1是 0否</summary>
        public decimal? ConduitPicc { get; set; }

        /// <summary>已做急救措施</summary>
        public string EmTreatment { get; set; }

        /// <summary>是否报警  1是 0否</summary>
        public bool AlertFlat { get; set; }
      
        /// <summary>舒张压(mmHg)</summary>
        public int? Dbp { get; set; }

        /// <summary>呼吸频率(次/分)</summary>
        public int? R { get; set; }

        /// <summary>出警派出所或交警队</summary>
        public string PoliceAddress { get; set; }

        /// <summary>中心静脉 1是 0否</summary>
        public decimal? ConduitZhongxin { get; set; }

        /// <summary>急救医生</summary>
        public string Doctor { get; set; }

        /// <summary>SPO2</summary>
        public string Spo2 { get; set; }

        /// <summary>腹引 1是 0否</summary>
        public decimal? ConduitFuyin { get; set; }

        /// <summary>吸氧管 1是 0否</summary>
        public decimal? ConduitXiyangguan { get; set; }

        /// <summary>胸引 1是 0否</summary>
        public decimal? ConduitXiongyin { get; set; }

        /// <summary>初步诊断</summary>
        public string PreliminaryDiagnosis { get; set; }

        /// <summary>气管导管 1是 0否</summary>
        public decimal? ConduitQiguan { get; set; }

        /// <summary>接诊医护人员签名</summary>
        public string EmergencyDoctor { get; set; }


        /// <summary>瞳孔(左)</summary>
        public string CustomField1 { get; set; }

        /// <summary>瞳孔(右)</summary>
        public string CustomField2 { get; set; }

        /// <summary>对光反射</summary>
        public string CustomField3 { get; set; }

        /// <summary>皮肤黏膜</summary>
        public string CustomField4 { get; set; }

        /// <summary>是否血糖检查</summary>
        public string CustomField5 { get; set; }

        /// <summary>血糖检查值</summary>
        public string CustomField6 { get; set; }

        /// <summary>专科检查</summary>
        public string CustomField7 { get; set; }

        /// <summary>是否确认(空为未确认交接单，1为已确认交接单)</summary>
        public string CustomField8 { get; set; }

        /// <summary>对光反射(右)</summary>
        public string CustomField9 { get; set; }

        /// <summary>自定义字段10</summary>
        public string CustomField10 { get; set; }


         /// <summary>导管留置</summary>
        public string Conduit { get; set; }
      
    }

}
