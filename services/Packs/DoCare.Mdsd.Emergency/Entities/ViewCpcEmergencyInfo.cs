using FastFrame.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    [Table("VIEW_CPC_EMERGENCY_INFO")]
    public class ViewCpcEmergencyInfo : IEntity<string>
    {
        [Column("REG_ID")]
        public string Id { get; set; }

        [Column("TAG_ID")]
        public string TAG_ID { get; set; }

        [Column("COMINGTYPE")]
        public string COMINGTYPE { get; set; }

        [Column("DISPATCHTYPE")]
        public string DISPATCHTYPE { get; set; }

        [Column("DIAGNOSISTYPE")]
        public string DIAGNOSISTYPE { get; set; }

        [Column("DIAGNOSISDESC")]
        public string DIAGNOSISDESC { get; set; }

        [Column("TRANSDISPATCHTIME")]
        public string TRANSDISPATCHTIME { get; set; }

        [Column("ARRIVEDSCENETIME")]
        public string ARRIVEDSCENETIME { get; set; }

        [Column("ARRIVEDHOSPITALTIMEFORTRANS")]
        public string ARRIVEDHOSPITALTIMEFORTRANS { get; set; }

        [Column("ADMISSIONTIMEFORSELF")]
        public string ADMISSIONTIMEFORSELF { get; set; }

        [Column("ISBYPASSEMERGENCY")]
        public string ISBYPASSEMERGENCY { get; set; }

        [Column("BYPASSEMERGENCYTYPE")]
        public string BYPASSEMERGENCYTYPE { get; set; }

        [Column("ISBYPASSCCU")]
        public string ISBYPASSCCU { get; set; }

        [Column("ARRIVEDEMERGENCYTIME")]
        public string ARRIVEDEMERGENCYTIME { get; set; }

        [Column("ARRIVEDCCUTIME")]
        public string ARRIVEDCCUTIME { get; set; }

        [Column("DOCTOR")]
        public string DOCTOR { get; set; }

        [Column("PHEPECGTIME")]
        public string PHEPECGTIME { get; set; }

        [Column("TIME")]
        public string TIME { get; set; }

        [Column("INHOSPITALECGTIME")]
        public string INHOSPITALECGTIME { get; set; }

        [Column("ECGDIAGNOSISTIME")]
        public string ECGDIAGNOSISTIME { get; set; }

        [Column("ISREMOTEECGTRAN")]
        public string ISREMOTEECGTRAN { get; set; }

        [Column("TRANDATE")]
        public string TRANDATE { get; set; }

        [Column("ICUSENSE")]
        public string ICUSENSE { get; set; }

        [Column("PULSERATE")]
        public string PULSERATE { get; set; }

        [Column("HEARTRATE")]
        public string HEARTRATE { get; set; }

        [Column("RESPIRATIONRATE")]
        public string RESPIRATIONRATE { get; set; }

        [Column("BLOODPRESSURE")]
        public string BLOODPRESSURE { get; set; }

        [Column("TEMPERATURE")]
        public string TEMPERATURE { get; set; }

        [Column("BLOOD_OXYGEN")]
        public string BLOOD_OXYGEN { get; set; }

        [Column("KILLIPLEVEL")]
        public string KILLIPLEVEL { get; set; }

        [Column("SAMPLINGTIME")]
        public string SAMPLINGTIME { get; set; }

        [Column("REPORTTIME")]
        public string REPORTTIME { get; set; }

        [Column("CTNIVALUE")]
        public string CTNIVALUE { get; set; }

        [Column("CRVALUE")]
        public string CRVALUE { get; set; }

        [Column("TIMEFOROTHER")]
        public string TIMEFOROTHER { get; set; }

        [Column("DOCTORFORCP")]
        public string DOCTORFORCP { get; set; }

        [Column("ACSDELIVERYTIME")]
        public string ACSDELIVERYTIME { get; set; }

        [Column("ASPIRINDOSE")]
        public string ASPIRINDOSE { get; set; }

        [Column("ACSDRUGTYPE")]
        public string ACSDRUGTYPE { get; set; }

        [Column("ACSDRUGDOSE")]
        public string ACSDRUGDOSE { get; set; }

        [Column("SYSTOLIC_PRESSURE")]
        public decimal? SYSTOLIC_PRESSURE { get; set; }

        [Column("BREATHING_RATE")]
        public decimal? BREATHING_RATE { get; set; }

        [Column("OPEN_EYE")]
        public decimal? OPEN_EYE { get; set; }

        [Column("TALK")]
        public decimal? TALK { get; set; }

        [Column("SPORTS")]
        public decimal? SPORTS { get; set; }

        [Column("RESET_LEVEL")]
        public string RESETLEVEL { get; set; }

        [Column("PATIENT_SAY")]
        public string PATIENTSAY { get; set; }

        [Column("ARRIVE_TYPE")]
        public string ARRIVETYPE { get; set; }

        [Column("MDT_TYPE")]
        public string MDT_TYPE { get; set; }
    }
}
