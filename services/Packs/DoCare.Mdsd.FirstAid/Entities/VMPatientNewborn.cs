namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMPatientNewborn
    {
        /// <summary>患儿胎龄(周)</summary>
        public int? GestationalAge { get; set; }

        /// <summary>生产方式(1顺产 2剖宫产)</summary>
        public int? ParturitionWay { get; set; }

        /// <summary>生产方式描述</summary>
        public string ParturitionWayDesc { get; set; }

        /// <summary>剖宫产原因</summary>
        public string CaesareanSectionReason { get; set; }

        /// <summary>胎次</summary>
        public int? Embryo { get; set; }

        /// <summary>产次</summary>
        public int? Parity { get; set; }

        /// <summary>脐带情况(1绕颈 2扭转 3打结 4脱垂 5其他异常)</summary>
        public int? FunicleType { get; set; }

        /// <summary>脐带情况描述</summary>
        public string FunicleTypeDescr { get; set; }

        /// <summary>脐带其他异常描述</summary>
        public string FunicleAbnormal { get; set; }

        /// <summary>羊水早破</summary>
        public int? AmnioticFluidRupture { get; set; }

        /// <summary>羊水早破时间（小时）</summary>
        public string RuptureTime { get; set; }

        /// <summary>胎盘情况(1前置 2早剥 3老化 4钙化 5其他异常)</summary>
        public int? PlacentaStatus { get; set; }

        /// <summary>胎盘情况描述</summary>
        public string PlacentaStatusDescr { get; set; }

        /// <summary>胎盘异常描述</summary>
        public string PlacentaAbnormal { get; set; }

        /// <summary>Apgar评分</summary>
        public int? ApgarGrade { get; set; }

        /// <summary>Apgar评分项（1：1分钟 5：5分钟 10：10分钟）</summary>
        public int? ApgarType { get; set; }

        /// <summary>Apgar评分项描述</summary>
        public string ApgarTypeDescr { get; set; }

        /// <summary>母亲孕期疾病(1高血压 2糖尿病 3其他)</summary>
        public int? MaternalMalady { get; set; }

        /// <summary>母亲孕期疾病描述</summary>
        public string MaternalMaladyDescr { get; set; }

        /// <summary>母亲孕期其他疾病</summary>
        public string MaternalMaladyOther { get; set; }

        /// <summary>母亲血型(1A 2B 3AB 4O)</summary>
        public int? MotherBloodType { get; set; }

        /// <summary>母亲血型描述</summary>
        public string MotherBloodTypeDescr { get; set; }

        /// <summary>母亲RH血型(1阴性 2阳性)</summary>
        public int? MotherRhBloodType { get; set; }

        /// <summary>母亲RH血型描述</summary>
        public string MotherRhBloodTypeDescr { get; set; }

        /// <summary>母亲用药史</summary>
        public string MotherMedicationHistor { get; set; }

        /// <summary>羊水(1正常 2过多 3过少 4血性 5粪染)</summary>
        public int? AmnioticFluid { get; set; }

        /// <summary>羊水描述</summary>
        public string AmnioticFluidDescr { get; set; }

        /// <summary>Apgar描述</summary>
        public string ApgarText { get; set; }

        /// <summary>脐带描述</summary>
        public string FunicleText { get; set; }

        /// <summary>羊水早破描述</summary>
        public string RuptureText { get; set; }

        /// <summary>母亲孕期疾病描述</summary>
        public string MaternalMaladyText { get; set; }

        /// <summary>生产方式描述</summary>
        public string ParturitionWayText { get; set; }

        /// <summary>胎盘描述</summary>
        public string PlacentaStatusText { get; set; }

        /// <summary>生后多久</summary>
        public string BornDays { get; set; }
    }
}
