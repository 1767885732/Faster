
namespace DoCare.Mdsd.FirstAid.Entities
{
    /// <summary>急救患者卒中信息</summary>
    public class VMStroke
    {
        /// <summary></summary>
        public string StrokeId { get; set; }

        /// <summary>言语不清</summary>
        public decimal? Speach { get; set; }

        /// <summary>肢体无力</summary>
        public decimal? Arm { get; set; }

        /// <summary>迅速求助(0 无 1 有)</summary>
        public decimal? Times { get; set; }

        /// <summary>面瘫/口角歪斜(0 无 1 有)</summary>
        public decimal? Face { get; set; }



        /// <summary>发病时间(分钟)</summary>
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
    }
}
