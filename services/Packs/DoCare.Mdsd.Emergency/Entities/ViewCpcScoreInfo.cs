using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    [Table("VIEW_CPC_SCORE_INFO")]
    public class ViewCpcScoreInfo : IEntity<string>
    {
        [Column("REG_ID")]
        public string Id { get; set; }

        [Column("VULNUS_TYPE")]
        public string VULNUS_TYPE { get; set; }

        [Column("OPERATOR_TIME")]
        public DateTime? OPERATOR_TIME { get; set; }

        [Column("PLACE")]
        public string PLACE { get; set; }

        [Column("DEGREE")]
        public decimal? DEGREE { get; set; }
    }
}
