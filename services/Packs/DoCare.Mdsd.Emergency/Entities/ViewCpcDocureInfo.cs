using FastFrame.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    [Table("VIEW_CPC_DOCURE_INFO")]
    public class ViewCpcDocureInfo : IEntity<string>
    {
        [Column("REG_ID")]
        public string Id { get; set; }

        [Column("DOCURE_INROOMTIME")]
        public string DocureInRoomTime { get; set; }

        [Column("DOCURE_OUTROOMTIME")]
        public string DocureOutRoomTime { get; set; }

        [Column("TO_WHERE")]
        public string ToWhere { get; set; }
    }
}
