
namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMEmergencyOrder
    {
        /// <summary>主键</summary>
        public string OrderId { get; set; }

        /// <summary>剂量</summary>
        public decimal? Dose { get; set; }

        /// <summary>用量描述</summary>
        public string DoseDesc { get; set; } 

        /// <summary>用法主键</summary>
        public string UsageId { get; set; }

        /// <summary>用法名称</summary>
        public string UsageText { get; set; }

        /// <summary>药品ID</summary>
        public string DrugId { get; set; }

        /// <summary>药品代码</summary>
        public string DrugCode { get; set; }

        /// <summary>药品名称（列表展示用）</summary>
        public string DrugName { get; set; }

        /// <summary>规格</summary>
        public string Specifications { get; set; }
    }
}
