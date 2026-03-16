using System;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMMedicalRecord
    {
        /// <summary>病历ID</summary>
        public string RecordId { get; set; }

        /// <summary>指导用药时间</summary>
        public DateTime? MedicationTime { get; set; }

        /// <summary>来自科室</summary>
        public string FromDept { get; set; }

        /// <summary>驳回时间</summary>
        public DateTime? RejectTime { get; set; }
  
        /// <summary>过敏史</summary>
        public string AllergicHistory { get; set; }

        /// <summary>既往史</summary>
        public string PastHistory { get; set; }

        /// <summary>归档人</summary>
        public string ArchiveUser { get; set; }

        /// <summary>现场诊断时间</summary>
        public DateTime? SceneDiagnosisTime { get; set; }

        /// <summary>是否通知急诊科(默认0 未通知 1 通知)</summary>
        public decimal? IsNotifyEmergency { get; set; }

        /// <summary>病历编号</summary>
        public string Code { get; set; }

        /// <summary>转到医生</summary>
        public string ToDoctor { get; set; }

        /// <summary>特殊病历标识 0 否  是</summary>
        public decimal? IsSpecialRecord { get; set; }

        /// <summary>驳回人</summary>
        public string RejectUser { get; set; }

        /// <summary>送往地址类型</summary>
        public string SentLocationType { get; set; }

        /// <summary>计划到达医院时间</summary>
        public DateTime? PlanToHosTime { get; set; }

        /// <summary>出诊医生</summary>
        public string DoctorName { get; set; }

        /// <summary>病史提供者</summary>
        public string HistoryProvider { get; set; }

        /// <summary>联系电话</summary>
        public string ContactNumber { get; set; }

        /// <summary>联系人</summary>
        public string Contact { get; set; }

        /// <summary>离开抢救室时间</summary>
        public DateTime? LeftRescueTime { get; set; }

        /// <summary>到达抢救室时间</summary>
        public DateTime? ArrivedRescueTime { get; set; }

        /// <summary>驾驶员姓名</summary>
        public string DriverName { get; set; }

        /// <summary>提交时间</summary>
        public DateTime? SubmitTime { get; set; }

        /// <summary>接诊地址</summary>
        public string Address { get; set; }

        /// <summary>高危病种预警</summary>
        public string HrDiseaseAlarm { get; set; }

        /// <summary>来自医生</summary>
        public string FromDoctor { get; set; }

        /// <summary>监护仪开启时间</summary>
        public DateTime? CustodyStartTime { get; set; }

        /// <summary>主诉</summary>
        public string ChiefComplaint { get; set; }

        /// <summary>转到科室</summary>
        public string ToDept { get; set; }

        /// <summary>首次心电图时间</summary>
        public DateTime? FirstEcgTime { get; set; }

        /// <summary>MPDS</summary>
        public string Mpds { get; set; }

        /// <summary>特殊病历标识内容</summary>
        public string SpecialMemo { get; set; }

        /// <summary>现病史</summary>
        public string PresentIllness { get; set; }

        /// <summary>初步诊断</summary>
        public string Diagnosis { get; set; }

        /// <summary>通知急诊科时间</summary>
        public DateTime? NotifyEmergencyTime { get; set; }

        /// <summary>心电图诊断时间</summary>
        public DateTime? EcgDiagnoise { get; set; }

        /// <summary>现场评估时间</summary>
        public DateTime? SceneAssessmentTime { get; set; }

        /// <summary>呼救原因</summary>
        public string ReasonType { get; set; }

        /// <summary>出诊护士</summary>
        public string NurseName { get; set; }

        /// <summary>疾病类别：其他</summary>
        public string DiseaseCategoryOther { get; set; }
      
        /// <summary>其他治疗措施</summary>
        public string OthreTreatment { get; set; }

        /// <summary>提交人</summary>
        public string SubmitUser { get; set; }
        
        /// <summary>归档时间</summary>
        public DateTime? ArchiveTime { get; set; }

        /// <summary>院内救治结果</summary>
        public string HosTreatResult { get; set; }

        /// <summary>病历归档状态：0未提交 1被驳回  2已提交  3已归档</summary>
        public decimal? ArchiveStatus { get; set; }


        /// <summary>病情判断</summary>
        public string ConditionGrade { get; set; }
        /// <summary>病情判断描述</summary>
        public string ConditionGradeText { get; set; }

        /// <summary>患者配合程度</summary>
        public string CoordinateDegree { get; set; }
        public string CoordinateDegreeText { get; set; }

        /// <summary>病情转归死亡现场(1 现场  2途中)</summary>
        public int? RescueDeadScene { get; set; }
        /// <summary>患者配合程度描述</summary>
        public string RescueDeadSceneText { get; set; }

        /// <summary>病情转归</summary>
        public string RescueChangeResult { get; set; }
        /// <summary>病情转归</summary>
        public string RescueChangeResultText { get; set; }

        /// <summary>疾病类别</summary>
        public string DiseaseCategory { get; set; }
        /// <summary>疾病类别描述</summary>
        public string DiseaseCategoryText { get; set; }

        /// <summary>病历状态： 0 未填写 1 填写中  2 填写不完整 3 填写完整</summary>
        public decimal? RecordStatus { get; set; }
        /// <summary>病历状态描述</summary>
        public string RecordStatusText { get; set; }

        /// <summary>来自医院</summary>
        public string FromHospital { get; set; }
        public string FromHospitalName { get; set; }

        /// <summary>送往医院ID</summary>
        public string ToHospial { get; set; }
        /// <summary>送往医院</summary>
        public string ToHospialName { get; set; } 
        
        /// <summary>初诊结果</summary>
        public string TreatmentResult { get; set; }
        /// <summary>初诊结果描述</summary>
        public string TreatmentResultText { get; set; }
        
        /// <summary>病史提供类型</summary>
        public string MedicalHisProType { get; set; }
        /// <summary>病史提供类型描述</summary>
        public string MedicalHisProTypeText { get; set; }

        /// <summary>现场</summary>
        public string Scene { get; set; }
        /// <summary>现场描述</summary>
        public string SceneText { get; set; }

        /// <summary>病历最后更新时间</summary>
        public DateTime? UpdateTime { get; set; }


        /// <summary>是否睡眠中发病</summary>
        public int? IsSleepOnset { get; set; }

        /// <summary>卒中时间（如果睡眠中发病，则为最后表现正常时间，否则为卒中发病时间）</summary>
        public DateTime? StrokeTime { get; set; }

        /// <summary>是否在外院行颅脑CT</summary>
        public int? IsBrainCt { get; set; }

        /// <summary>溶栓禁忌症筛查</summary>
        public string ThrombolysisContraindications { get; set; }

        /// <summary>溶栓禁忌症筛查描述</summary>
        public string ThrombolysisContraindicationsText { get; set; }

        /// <summary>病历类型（1普通病历 2卒中病历）</summary>
        public int? RecordType { get; set; }

        /// <summary>病历类型描述</summary>
        public string RecordTypeText { get; set; }

        /// <summary>转诊原因</summary>
        public string TransferReason { get; set; }

        /// <summary>发热最高体温（℃）</summary>
        public decimal? HighestTemperature { get; set; }

        /// <summary>发热是否惊厥(0否 1是)</summary>
        public int? IsConvulsions { get; set; }

        /// <summary>惊厥体温</summary>
        public decimal? ConvulsionsTemperature { get; set; }

        /// <summary>惊厥表现</summary>
        public string ConvulsionsManifestation { get; set; }

        /// <summary>是否有惊厥史(0否 1是)</summary>
        public int? IsConvulsionsHistory { get; set; }

        /// <summary>抽搐时间(分钟)</summary>
        public string TicTime { get; set; }

        /// <summary>具体表现</summary>
        public string TicManifestation { get; set; }

        /// <summary>咳嗽(0否 1是)</summary>
        public int? IsCough { get; set; }

        /// <summary>喘(0否 1是)</summary>
        public int? IsPant { get; set; }

        /// <summary>呼吸困难(0否 1是)</summary>
        public int? IsDyspnea { get; set; }

        /// <summary>休克(0否 1是)</summary>
        public int? IsShock { get; set; }

        /// <summary>呕吐(0否 1是)</summary>
        public int? IsEmesis { get; set; }

        /// <summary>腹泻(0否 1是)</summary>
        public int? IsDiarrhea { get; set; }

        /// <summary>尿量(1正常 2少 3多)</summary>
        public int? UrineVolume { get; set; }

        /// <summary>尿量描述</summary>
        public string UrineVolumeDescr { get; set; }

        /// <summary>大便(1正常 2稀 3干燥)</summary>
        public int? Excrement { get; set; }

        /// <summary>大便描述</summary>
        public string ExcrementDescr { get; set; }

        /// <summary>其他</summary>
        public string Other { get; set; }

        /// <summary>当地治疗及用药</summary>
        public string LocalTreatmentMedication { get; set; }

        /// <summary>其他用药</summary>
        public string OtherDrug { get; set; }

        /// <summary>感染史（0无 1有）</summary>
        public int? InfectionHistory { get; set; }

        /// <summary>感染史描述</summary>
        public string InfectionHistoryDescr { get; set; }

        /// <summary>用药史</summary>
        public string MedicationHistory { get; set; }

        /// <summary>最高体温描述</summary>
        public string HighestTemperatureText { get; set; }
    }
}
