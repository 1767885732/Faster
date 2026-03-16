using System;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMPatient
    {
        /// <summary>主键</summary>
        public string PatientId { get; set; }

        /// <summary>医保号</summary>
        public string HealthInsuranceId { get; set; }

        /// <summary>出生年月</summary>
        public DateTime? Birthday { get; set; }

        /// <summary>急诊ID</summary>
        public string FromEmergencyId { get; set; }

        /// <summary>年龄</summary>
        public string Age { get; set; }

        /// <summary>患者MPI</summary>
        public string Mpi { get; set; }

        /// <summary>身份证</summary>
        public string IdNo { get; set; }

        /// <summary>是否激活高危工作库0 未激活1 激活</summary>
        public decimal? IsActivatedHr { get; set; }

        /// <summary>腕带</summary>
        public string TagId { get; set; }

        /// <summary>健康档案号</summary>
        public string HealthRecordId { get; set; }

        /// <summary>姓名</summary>
        public string Name { get; set; }

        /// <summary>性别</summary>
        public string Gender { get; set; }


        /// <summary>国家</summary>
        public string State { get; set; }
        /// <summary>国家名称</summary>
        public string StateName { get; set; }

        /// <summary>民族</summary>
        public string Nation { get; set; }
        /// <summary>民族描述</summary>
        public string NationText { get; set; }

        /// <summary>体重</summary>
        public string Weight { get; set; }

        /// <summary>体重单位（1克 2公斤）</summary>
        public int? WeightUnit { get; set; }

        /// <summary>体重单位描述</summary>
        public string WeightUnitDescr { get; set; }

        /// <summary>其他</summary>
        public string Other { get; set; }

        /// <summary>体重描述（数量+单位）</summary>
        public string WeightText { get; set; }
    }
}
