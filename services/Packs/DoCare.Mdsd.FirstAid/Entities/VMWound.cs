
namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>急救患者格拉斯哥创伤评分</summary>
    public class VMWound
    {
        /// <summary></summary>
        public string WoundId { get; set; }

        /// <summary>总分</summary>
        public int? TotalValue { get; set; }

        /// <summary>受伤类型</summary>
        public string WoundType { get; set; }

        /// <summary>受伤机制</summary>
        public string WoundMechnisam { get; set; }

        /// <summary>受伤部位</summary>
        public string WoundPart { get; set; }

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

        /// <summary></summary>
        public string CustomField6 { get; set; }

        /// <summary></summary>
        public string CustomField7 { get; set; }

        /// <summary></summary>
        public string CustomField8 { get; set; }

        /// <summary></summary>
        public string CustomField9 { get; set; }

        /// <summary></summary>
        public string CustomField10 { get; set; }
       

        /// <summary>睁眼(E)</summary>
        public int? EValue { get; set; }
        public string EText { get; set; } 
        
        /// <summary>语言(V)</summary>
        public int? VValue { get; set; }
        public string VText { get; set; } 

        /// <summary>运动(M)</summary>
        public int? MValue { get; set; }
        public string MText { get; set; } 

    }
}
