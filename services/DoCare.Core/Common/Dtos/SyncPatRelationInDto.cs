using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Common.Dtos
{
    public class SyncPatRelationInDto
    {
        /// <summary>
        /// 病历号
        /// </summary>
        [Required]
        public string PatientNo { get; set; }

        /// <summary>
        /// 关联号类型(0:急救号(missionid|phepid);1:急诊号;2:门诊号;3:住院号;4:妇幼保健号)
        /// </summary>
        [Required, Range(0, 4, ErrorMessage = "关联号类型超出有效范围")]
        public int RelationNoType { get; set; }

        /// <summary>
        /// 关联号
        /// </summary>
        [Required]
        public string RelationNo { get; set; }
    }
}
