using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    [Table("MED_CLINIC_VIEW_PHY_VALUE")]
    public class ViewPhyValue : IEntity<string>
    {
        [Column("REG_ID")]
        public string Id { get; set; }

        [Column("PhyValue")]
        public string PhyValue { get; set; }
    }
}
