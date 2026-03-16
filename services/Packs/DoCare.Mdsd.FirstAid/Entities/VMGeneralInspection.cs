using System;

namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>体格检查 </summary>
    public class VMGeneralInspection
    {
        /// <summary>主键</summary>
        public string InspectionId { get; set; }

        /// <summary>脉搏(次/分)</summary>
        public int? Pulse { get; set; }

        /// <summary>瞳孔（右）</summary>
        public string PupilRightSize { get; set; }

        /// <summary>其他</summary>
        public string Other { get; set; }

        /// <summary>是否ECG检查</summary>
        public decimal? IsEcg { get; set; }

        /// <summary>类型（现场，返院，目的地）</summary>
        public decimal? Type { get; set; }

        /// <summary>SPO2</summary>
        public int? Spo2 { get; set; }

        /// <summary>血糖</summary>
        public string BloodGlucose { get; set; }

        /// <summary>是否测量了体温</summary>
        public decimal? IsMeasuredT { get; set; }

        /// <summary>体温(℃)</summary>
        public string T { get; set; }

        /// <summary>瞳孔（左）</summary>
        public string PupilLeftSize { get; set; }

        /// <summary>收缩压(mmHg)</summary>
        public int? Sbp { get; set; }

        /// <summary>专科检查</summary>
        public string ExaminationResult { get; set; }

        /// <summary>体检时间</summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>舒张压(mmHg)</summary>
        public int? Dbp { get; set; }

        /// <summary>皮肤黏膜:其他</summary>
        public string SkinOther { get; set; }

        /// <summary>是否血糖仪检查</summary>
        public decimal? IsBloodGlucose { get; set; }

        /// <summary>瞳孔（反应）</summary>
        public string PupilResponse { get; set; }

        /// <summary>呼吸频率(次/分)</summary>
        public int? Rr { get; set; }


        /// <summary>心率(次/分)</summary>
        public int? HR { get; set; }

        /// <summary>心率(次/分)</summary>
        public string BP { get; set; }



        /// <summary>左眼对光反射（1灵敏 2 迟钝 3消失 -1 未设置）</summary>
        public decimal? LightReflection { get; set; }
        public string LightReflectionText { get; set; }
        /// <summary>右眼对光反射（1灵敏 2 迟钝 3消失 -1 未设置）</summary>
        public decimal? LightReflectionRight { get; set; }
        public string LightReflectionRightText { get; set; }

        /// <summary>皮肤黏膜（1正常 2 湿冷 3苍白 4黄染 5发绀 6水肿 -1 未设置）</summary>
        public decimal? Skin { get; set; }
        public string SkinText { get; set; }


        /// <summary>体检状态（0 正常采集 2 未检测 3拒绝采集 -1 未设置）</summary>
        public decimal? InspectionStatus { get; set; }
        public string InspectionStatusText { get; set; }


        /// <summary>神志</summary>
        public decimal? Mind { get; set; }
        public string MindText { get; set; }





        /// <summary></summary>
        public string CustomField1 { get; set; }

        /// <summary></summary>
        public string CustomField2 { get; set; }

        /// <summary></summary>
        public string CustomField3 { get; set; }

        /// <summary></summary>
        public string CustomField4 { get; set; }

        /// <summary></summary>
        public string CustomField5 { get; set; }

        /// <summary>左上肢能否抬起</summary>
        public int? IsLeftHandUplift { get; set; }
        /// <summary>左上肢能否抬起描述</summary>
        public string IsLeftHandUpliftText { get; set; }

        /// <summary>右上肢能否抬起</summary>
        public int? IsRightHandUplift { get; set; }

        /// <summary>右上肢能否抬起描述</summary>
        public string IsRightHandUpliftText { get; set; }

        /// <summary>左下肢能否抬起</summary>
        public int? IsLeftLegUplift { get; set; }

        /// <summary>左下肢能否抬起描述</summary>
        public string IsLeftLegUpliftText { get; set; }

        /// <summary>右下肢能否抬起</summary>
        public int? IsRightLegUplift { get; set; }

        /// <summary>右下肢能否抬起描述</summary>
        public string IsRightLegUpliftText { get; set; }

        /// <summary>肢体活动</summary>
        public string LimbExercise { get; set; }


        /// <summary>左球结膜水肿(0无 1有)</summary>
        public int? IsChemosisLeft { get; set; }

        /// <summary>右球结膜水肿(0无 1有)</summary>
        public int? IsChemosisRight { get; set; }

        /// <summary>面色(1红润 2紫绀 3苍白 4其他)</summary>
        public int? Complexion { get; set; }

        /// <summary>面色描述</summary>
        public string ComplexionDescr { get; set; }

        /// <summary>面色其他</summary>
        public string ComplexionOther { get; set; }

        /// <summary>脱水(1无 2轻 3中 4重)</summary>
        public int? Dehydration { get; set; }

        /// <summary>脱水描述</summary>
        public string DehydrationDescr { get; set; }

        /// <summary>有无鼻扇(0无 1有)</summary>
        public int? FlaringNares { get; set; }

        /// <summary>三凹征(1阴性 2阳性)</summary>
        public int? ThreeConcaveSign { get; set; }

        /// <summary>三凹征描述</summary>
        public string ThreeConcaveSignDescr { get; set; }

        /// <summary>双肺呼吸音(1粗 2啰音 3弱 4正常 5不对称)</summary>
        public int? BreathSounds { get; set; }

        /// <summary>双肺呼吸音描述</summary>
        public string BreathSoundsDescr { get; set; }

        /// <summary>是否正吸氧(0否 1是)</summary>
        public int? IsOxygen { get; set; }

        /// <summary>吸氧容量（升） </summary>
        public decimal? OxygenLitre { get; set; }

        /// <summary>吸氧方式(1鼻导管 2面罩 3头罩 4经气管插管复苏气囊正压给氧)</summary>
        public int? OxygenWay { get; set; }

        /// <summary>吸氧方式描述</summary>
        public string OxygenWayDescr { get; set; }

        /// <summary>吸入气中的氧浓度分数（%）</summary>
        public decimal? Fio2 { get; set; }

        /// <summary>是否气管插管(0否 1是)</summary>
        public int? IsTracheaCannula { get; set; }

        /// <summary>气管插管深度(cm)</summary>
        public decimal? TracheaCannulaDepth { get; set; }

        /// <summary>是否上呼吸机(0否 1是)</summary>
        public int? UseBreathMachine { get; set; }

        /// <summary>呼吸机类型(1CPAP 2SIMV 3AC 4其他)</summary>
        public int? BreathMachineType { get; set; }

        /// <summary>呼吸机类型描述</summary>
        public string BreathMachineTypeDescr { get; set; }

        /// <summary>心音(1有力 2低钝 3弱)</summary>
        public int? HeartSounds { get; set; }

        /// <summary>心音描述</summary>
        public string HeartSoundsDescr { get; set; }

        /// <summary>心律(1齐 2不齐)</summary>
        public int? HeartRate { get; set; }

        /// <summary>心律描述</summary>
        public string HeartRateDescr { get; set; }

        /// <summary>杂音(0无 1有)</summary>
        public int? Noise { get; set; }

        /// <summary>杂音情况</summary>
        public string NoiseOther { get; set; }

        /// <summary>其他呼吸机类型</summary>
        public string BreathMachineOther { get; set; }

        /// <summary>MAP</summary>
        public string Map { get; set; }

        /// <summary>PIP(cmH_2 O)</summary>
        public string Pip { get; set; }

        /// <summary>PEEP(cmH_2 O)</summary>
        public string Peep { get; set; }

        /// <summary>Ti(sec)</summary>
        public string Ti { get; set; }

        /// <summary>腹部(1正常 2隆起)</summary>
        public int? Belly { get; set; }

        /// <summary>腹部描述</summary>
        public string BellyDescr { get; set; }

        /// <summary>四肢(1正常 2异常)</summary>
        public int? AllFours { get; set; }

        /// <summary>四肢描述</summary>
        public string AllFoursDescr { get; set; }

        /// <summary>正吸氧oxygen_FIO2（%）</summary>
        public decimal? OxygenFio2 { get; set; }

        /// <summary>肝脾(1正常 2大)</summary>
        public int? LiverSpleen { get; set; }

        /// <summary>肝脾描述</summary>
        public string LiverSpleenDescr { get; set; }

        /// <summary>肠鸣音(0无 1有)</summary>
        public int? Borborygmus { get; set; }

        /// <summary>四肢异常描述</summary>
        public string AllFoursAbnormal { get; set; }

        /// <summary>肌张力（1高 2低 3无）</summary>
        public int? MuscularTension { get; set; }

        /// <summary>肌张力描述</summary>
        public string  MuscularTensionDesc { get; set; }

        /// <summary>原始反射（1可引出 2弱 3无）</summary>
        public int? PrimitiveReflex { get; set; }

        /// <summary>原始反射描述</summary>
        public string PrimitiveReflexDescr { get; set; }

        /// <summary>参数呼吸频率(次/分)</summary>
        public decimal? RrParam { get; set; }

        /// <summary>参数其他</summary>
        public string OtherParam { get; set; }

        /// <summary>球结膜水肿左描述</summary>
        public string IsChemosisLeftText { get; set; }

        /// <summary>球结膜水肿右描述</summary>
        public string IsChemosisRightText { get; set; }

        /// <summary>有无鼻扇描述</summary>
        public string FlaringNaresText { get; set; }

        /// <summary>是否正吸氧描述</summary>
        public string OxygenText { get; set; }

        /// <summary>是否气管插管描述</summary>
        public string IsTracheaCannulaText { get; set; }

        /// <summary>是否上呼吸机描述</summary>
        public string BreathMachineText { get; set; }

        /// <summary>杂音描述</summary>
        public string NoiseText { get; set; }

        /// <summary>四肢描述</summary>
        public string AllFoursText { get; set; }

        /// <summary>肠鸣音描述</summary>
        public string BorborygmusDesc { get; set; }

        /// <summary>面色描述</summary>
        public string ComplexionText { get; set; }
    }
}
