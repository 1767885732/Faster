using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    [Table("VIEW_CPC_PATIENT_INFO")]
    public class ViewCpcPatientInfo : IEntity<string>
    {
        [Column("REG_ID")]
        public string Id { get; set; }

        [Column("CLINICNO")]
        public string CLINICNO { get; set; }

        [Column("PHEPPATIENTID")]
        public string PHEPPATIENTID { get; set; }

        /// <summary>
        /// 院前任务号
        /// </summary>
        [Column("PHEPMISSIONID")]
        public string PHEPMISSIONID { get; set; }

        [Column("PATIENTID")]
        public string PATIENTID { get; set; }

        [Column("VISITID")]
        public string VISITID { get; set; }

        [Column("IDCARDNO")]
        public string IDCARDNO { get; set; }

        [Column("CITIZEN_CARD")]
        public string CITIZEN_CARD { get; set; }

        [Column("CARTEVITALNO")]
        public string CARTEVITALNO { get; set; }

        [Column("NAME")]
        public string NAME { get; set; }

        [Column("BIRTHDAY")]
        public string BIRTHDAY { get; set; }

        [Column("AGE")]
        public string AGE { get; set; }

        [Column("NATION")]
        public string NATION { get; set; }

        [Column("GENDER")]
        public string GENDER { get; set; }

        [Column("OCCUPATION")]
        public string OCCUPATION { get; set; }

        [Column("MARRIAGE")]
        public string MARRIAGE { get; set; }

        [Column("EDUCATION")]
        public string EDUCATION { get; set; }

        [Column("PHONE")]
        public string PHONE { get; set; }

        [Column("ADDRESS")]
        public string ADDRESS { get; set; }

        [Column("REGISTTIME")]
        public string REGISTTIME { get; set; }

        [Column("ATTACKADDRESS")]
        public string ATTACKADDRESS { get; set; }

        [Column("ATTACKTIME")]
        public string ATTACKTIME { get; set; }

        [Column("CALLTIME")]
        public string CALLTIME { get; set; }

        [Column("ISCALLHELP")]
        public string ISCALLHELP { get; set; }

        [Column("CALLSOURCE")]
        public string CALLSOURCE { get; set; }

        [Column("GREEN_CHANNEL")]
        public string GREENCHANNEL { get; set; }

        [Column("ISDOCARE")]
        public string ISDOCARE { get; set; }

        [Column("PATIENT_SAY")]
        public string PATIENTSAY { get; set; }

        [Column("WRISTBARCODE")]
        public string WRISTBARCODE { get; set; }

        [Column("TAG_ID")]
        public string TAG_ID { get; set; }

        [Column("MDT_TYPE")]
        public string MDT_TYPE { get; set; }
    }
}
