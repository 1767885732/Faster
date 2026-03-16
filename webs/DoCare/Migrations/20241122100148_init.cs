using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoCare.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MED_EXAM_REPORT",
                columns: table => new
                {
                    EXAM_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    EXAM_CLASS = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    EXAM_SUB_CLASS = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    CLIN_DIAG = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    EXAM_MODE = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    DEVICE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PERFORMED_BY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    REQ_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    REQ_DEPT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    REQ_PHYSICIAN = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    REQ_MEMO = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    NOTICE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    EXAM_START_DATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    EXAM_END_DATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    REPORT_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    EXAM_PARA = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: true),
                    RECOMMENDATION = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    TECHNICIAN = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    REPORTER = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    RESULT_STATUS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    VERIFIED_BY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    VERIFIED_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_EXAM_REPORT", x => x.EXAM_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_LAB_MASTER",
                columns: table => new
                {
                    TEST_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    PRIORITY_INDICATOR = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    NAME = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    TEST_CAUSE = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    RELEVANT_CLINIC_DIAG = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    SPECIMEN = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    SPCM_RECEIVED_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    APPLY_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ORDERING_DEPT = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    ORDERING_PROVIDER = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    PERFORMED_BY = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    RESULT_STATUS = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    RESULTS_RPT_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    TRANSCRIPTIONIST = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    VERIFIED_BY = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_LAB_MASTER", x => x.TEST_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_LAB_RESULT",
                columns: table => new
                {
                    ITEM_NO = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: false),
                    TEST_NO = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    REPORT_ITEM_NAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    REPORT_ITEM_CODE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ABNORMAL_INDICATOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    RESULT = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    UNITS = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    RESULT_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    REFERENCE_RESULT = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_LAB_RESULT", x => x.ITEM_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_BLOOD_GAS_DETAIL",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: true),
                    BLOOD_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    REPORT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    TEST_ID = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    TEST_NAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    TEST_VALUE = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    UNIT = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    CONCLUSION = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_BLOOD_GAS_DETAIL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_ECG_INFO",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(40)", maxLength: 60, nullable: true),
                    EXAM_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    REPORT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DIAG_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DIAG_DESC = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    DIAG_SOURCE = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    ECG_PATH = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    ECG_URL = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    NAME = table.Column<string>(type: "VARCHAR2(60)", nullable: true),
                    SEX = table.Column<string>(type: "VARCHAR2(60)", nullable: true),
                    AGE = table.Column<string>(type: "VARCHAR2(60)", nullable: true),
                    ECG_PATIENT_ID = table.Column<string>(type: "VARCHAR2(60)", nullable: true),
                    ECG_DATA = table.Column<string>(type: "CLOB", nullable: true),
                    PDF_DATA = table.Column<string>(type: "CLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_ECG_INFO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_MATERECORD",
                columns: table => new
                {
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    IN_DEPT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    BED_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_HOS_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    IN_COMPLAINT = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    PREGANET_WEEK = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PREGANET_TIMES = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PRODUCT_TIMES = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_SENSE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_T = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_P = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_R = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_B_P = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_SPO2 = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_GLU = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    FETEL_HEART = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    UTER_HEIGHT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ABD_INDEX = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PAST_HISTORY = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    FAMILY_HISTORY = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    MENARCHE_AGE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MENSTRUAL_DAY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MENSTRUAL_CYCLE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MENSTRUAL_VOLUME = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DYSMENORRHEA = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    INITIAL_DIAGNOSIS = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    INITIAL_DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    INHOS_DIAGNOSIS = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    INHOS_DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    HIGH_RISK = table.Column<string>(type: "VARCHAR2(2000)", maxLength: 2000, nullable: true),
                    SPECIAL_TREATMENT = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    PRODUCTED_BLEED = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    INDICATION = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_MATERECORD", x => x.PATIENT_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_NEWBORNRECORD",
                columns: table => new
                {
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    MOTHER_NAME = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PERSON_SAY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    BIRTH_PARITY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DELIVERY_TIMES = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    GESTATIONAL_AGE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    BIRTH_WAY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    BIRTH_WEIGHT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    BIRTH_DEFECT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    AMNIOTIC_FLUID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    AMNIOTIC_FLUID_AMOUNT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PLACENTA = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    UMBILICAL_CORD = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PRENATAL_STEROID_HORMONES = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    APGAR_ONE_MINUTE = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    APGAR_FIVE_MINUTE = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    APGAR_TEN_MINUTE = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    CRITICAL_POINTS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_HOS_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    IN_HOS_DESC = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    MOTHER_BLOOD_TYPE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MOTHER_RH_BLOOD_TYPE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MOTHER_WHETHER_SMOKE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MOTHER_INFECTION_HISTORY = table.Column<string>(type: "VARCHAR2(2000)", maxLength: 2000, nullable: true),
                    MOTHER_MEDICATION_HISTORY = table.Column<string>(type: "VARCHAR2(2000)", maxLength: 2000, nullable: true),
                    MOTHER_ALLERGIES_HISTORY = table.Column<string>(type: "VARCHAR2(2000)", maxLength: 2000, nullable: true),
                    ASPHYXIA_RESUSCITATION_HISTORY = table.Column<string>(type: "VARCHAR2(2000)", maxLength: 2000, nullable: true),
                    MOTHER_AGE = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: true),
                    MOTHER_ID_CARD = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    FAMILY_ADDRESS = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    BORN_HOSPITAL = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_NEWBORNRECORD", x => x.PATIENT_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_OPERATION",
                columns: table => new
                {
                    APPLY_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SCHEDULED_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ANESTHESIA_METHOD = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SURGEON = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ANESTHESIA_DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DIAG_BEFORE_OPERATION = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: true),
                    DIAG_AFTER_OPERATION = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: true),
                    OPERATION_SCALE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    IN_DATE_TIME = table.Column<DateTime>(type: "DATE", maxLength: 60, nullable: true),
                    OUT_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ANES_START_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ANES_END_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    START_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    END_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    IN_PACU_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    OUT_PACU_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    BLOOD_LOSSED = table.Column<short>(type: "NUMBER(6)", nullable: true),
                    BLOOD_TRANSFERED = table.Column<short>(type: "NUMBER(6)", nullable: true),
                    OPERATION_NAME = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    OPERATION_PART = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    OPER_NURSE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SUPPLY_NURSE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_OPERATION", x => x.APPLY_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_ORDERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ORDER_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ORDER_SUB_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    CLINIC_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    INP_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    REPEAT_INDICATOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ORDER_CLASS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ORDER_TEXT = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    ORDER_CODE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DOSAGE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DOSAGE_UNITS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ADMINISTRATION = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    START_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    STOP_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DURATION = table.Column<DateTime>(type: "DATE", nullable: true),
                    DURATION_UNITS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    FREQUENCY = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ORDERING_DEPT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    STOP_DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    NURSE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    STOP_NURSE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ENTER_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    STOP_ORDER_DATE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ORDER_STATUS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    CURRENT_PRESC_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_ORDERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_RELATION_INFO",
                columns: table => new
                {
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    PHEP_ID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MISSION_ID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    CLINIC_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    EMERGENCY_ID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    REGIST_ID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    INP_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    INP_REGIST_ID = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MAC_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MOTHER_ID_CARD = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PATIENT_SOURCE = table.Column<string>(type: "VARCHAR2(10)", maxLength: 10, nullable: true),
                    CREATED_TIME = table.Column<DateTime>(type: "DATE", nullable: false),
                    MEMO = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    ORG_CODE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MR_NO = table.Column<string>(type: "VARCHAR2(60)", nullable: true),
                    OTHER_NO = table.Column<string>(type: "VARCHAR2(60)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_RELATION_INFO", x => x.PATIENT_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_SCORE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SCORE_TYPE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SCORE_SOURCE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SCORE_ITEM = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SCORE_RESULT = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    SCORE_DATE = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_SCORE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MED_PAT_TIMENODES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: false),
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    TAG_ID = table.Column<string>(type: "VARCHAR2(40)", maxLength: 60, nullable: true),
                    ITEM_NAME = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ITEM_VALUE = table.Column<DateTime>(type: "date", nullable: true),
                    BIND_TIME = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PAT_TIMENODES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MED_PATIENT_DETAILS_INFO",
                columns: table => new
                {
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    REGIST_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DEPT_CODE = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ARRIVED_DEPT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    TRIAGE_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ADMISSION_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DIAG_DOCTOR = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DIAG_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DIAG_DESC = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    COMPLAINT = table.Column<string>(type: "VARCHAR2(500)", maxLength: 100, nullable: true),
                    IS_EMG = table.Column<string>(type: "VARCHAR2(10)", maxLength: 10, nullable: true),
                    EMG_BED_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    TRANSFER_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    IS_GREEN_CHANNEL = table.Column<int>(type: "NUMBER(2)", nullable: false),
                    ICU_SENSE = table.Column<string>(type: "VARCHAR2(40)", nullable: true),
                    TEMP = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    BR = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    PLUS = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    HR = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    BP = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    SPO2 = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    IN_HOS_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    IN_DEPT_CODE = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    IN_WARD_CODE = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    IN_HOS_DIAGNOSIS = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    IN_DEPT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    BED_NO = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    TOAST_FLAG = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    OUT_HOS_DIAGNOSIS = table.Column<string>(type: "VARCHAR2(1000)", maxLength: 1000, nullable: true),
                    OUT_DEPT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    OUT_HOS_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    HOS_DAY = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    TOTAL_COST = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    DRUG_COST = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    EXAM_COST = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    OUT_WAY = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PATIENT_DETAILS_INFO", x => x.PATIENT_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PATIENT_REGIST_INFO",
                columns: table => new
                {
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    NAME = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "DATE", nullable: true),
                    AGE = table.Column<string>(type: "VARCHAR2(60)", nullable: true),
                    SEX = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    ID_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    CITIZEN_CARD = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PAT_JOB = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    MAIL_ADDRESS = table.Column<string>(type: "VARCHAR2(60)", maxLength: 60, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    COME_TYPE = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    ORG_CODE = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    DIAG_LEV = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    ARRIVE_TYPE_EXTENSION = table.Column<string>(type: "VARCHAR2(10)", nullable: true),
                    CONTACT_NAME = table.Column<string>(type: "VARCHAR2(20)", nullable: true),
                    PAY_TYPE = table.Column<string>(type: "VARCHAR2(40)", nullable: true),
                    CONTACT_PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PATIENT_REGIST_INFO", x => x.PATIENT_NO);
                });

            migrationBuilder.CreateTable(
                name: "MED_PHEP_PATIENT_INFO",
                columns: table => new
                {
                    PATIENT_NO = table.Column<string>(type: "VARCHAR2(60)", maxLength: 40, nullable: false),
                    NAME = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "DATE", nullable: true),
                    SEX = table.Column<string>(type: "VARCHAR2(10)", maxLength: 10, nullable: true),
                    ID_NO = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    CITIZEN_CARD = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    MAIL_ADDRESS = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    ATTACK_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ATTACK_ADDRESS = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    CALL_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ACCEPT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    DISPATCH_TYPE = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    DISPATCH_VEHICLETIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ARRIVED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    TOUCH_PAT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    LEFT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    ARRIVED_HOS_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    FMC_DOCTOR = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true),
                    FIRST_DIAGNOSIS = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    TO_HOSPITAL = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    KILLIP_LEVEL = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    COMPLAINT = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    SENSE = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    TEMP = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    BR = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    PLUS = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    HR = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    BP = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    SPO2 = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    BLOOD_SUGAR = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    SPECIAL_CHECK = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    RES1 = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    RES2 = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    SYMPTOM = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    NOTIFY_EMERGENCY = table.Column<DateTime>(type: "DATE", nullable: true),
                    DIAGNOSIS_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    PRESENT_ILLNESS = table.Column<string>(type: "VARCHAR2(500)", maxLength: 200, nullable: true),
                    ALLERGICHISTORY = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    PASTHISTORY = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    CONDITIONGRADE = table.Column<string>(type: "VARCHAR2(40)", maxLength: 40, nullable: true),
                    THERAPEUTIC_METHOD = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: true),
                    INFECTION_HISTORY = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    MEDICATION_HISTORY = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    RECOVERY_HISTORY = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: true),
                    NURSE_NAME = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PHEP_PATIENT_INFO", x => x.PATIENT_NO);
                });

            migrationBuilder.CreateTable(
                name: "VIEW_PATIENT_DIAGNOSIS",
                columns: table => new
                {
                    CLINIC_REGIST_NO = table.Column<string>(nullable: false),
                    CLINIC_NO = table.Column<string>(nullable: true),
                    INP_NO = table.Column<string>(nullable: true),
                    DIAGNOSIS_TYPE = table.Column<string>(nullable: true),
                    DIAGNOSIS_DESC = table.Column<string>(nullable: true),
                    DIAGNOSIS_DATE = table.Column<DateTime>(nullable: true),
                    DOCTOR_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIEW_PATIENT_DIAGNOSIS", x => x.CLINIC_REGIST_NO);
                });

            migrationBuilder.CreateTable(
                name: "ViewOutHosPatientInfo",
                columns: table => new
                {
                    CLINIC_REGISTER_NO = table.Column<string>(nullable: false),
                    CLINIC_NO = table.Column<string>(nullable: true),
                    PATIENT_ID = table.Column<string>(nullable: true),
                    NAME = table.Column<string>(nullable: true),
                    NATION = table.Column<string>(nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(nullable: true),
                    SEX = table.Column<string>(nullable: true),
                    ID_NO = table.Column<string>(nullable: true),
                    PAT_JOB = table.Column<string>(nullable: true),
                    CARD_ID = table.Column<string>(nullable: true),
                    MAIL_ADDRESS = table.Column<string>(nullable: true),
                    PHONE_NUMBER = table.Column<string>(nullable: true),
                    PATIENT_TYPE = table.Column<string>(nullable: true),
                    REGIST_TIME = table.Column<DateTime>(nullable: true),
                    DEPT_CODE = table.Column<string>(nullable: true),
                    ARRIVED_DEPT_TIME = table.Column<DateTime>(nullable: true),
                    TRIAGE_TIME = table.Column<DateTime>(nullable: true),
                    DOCTOR = table.Column<string>(nullable: true),
                    ADMISSTION_TIME = table.Column<DateTime>(nullable: true),
                    DIAG_DOCTOR = table.Column<string>(nullable: true),
                    DIAG_TIME = table.Column<DateTime>(nullable: true),
                    DIAG_DESC = table.Column<string>(nullable: true),
                    COMPLAINT = table.Column<string>(nullable: true),
                    TRANSFER_TIME = table.Column<DateTime>(nullable: true),
                    IS_USED = table.Column<string>(nullable: true),
                    REGISTER_TYPE = table.Column<string>(nullable: true),
                    HOSPITAL_AREA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewOutHosPatientInfo", x => x.CLINIC_REGISTER_NO);
                });

            migrationBuilder.CreateIndex(
                name: "TESTNO_ITEMNO",
                table: "MED_LAB_RESULT",
                columns: new[] { "TEST_NO", "ITEM_NO" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PATIENTNO_REPORTTIME_TESTID",
                table: "MED_PAT_BLOOD_GAS_DETAIL",
                columns: new[] { "PATIENT_NO", "REPORT_TIME", "TEST_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PatientNo_OrderNo_SubNo",
                table: "MED_PAT_ORDERS",
                columns: new[] { "PATIENT_NO", "ORDER_NO", "ORDER_SUB_NO" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PNo_ST_SS_SI",
                table: "MED_PAT_SCORE",
                columns: new[] { "PATIENT_NO", "SCORE_TYPE", "SCORE_SOURCE", "SCORE_ITEM" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PatientNo_ItemName_TagId",
                table: "MED_PAT_TIMENODES",
                columns: new[] { "PATIENT_NO", "ITEM_NAME", "TAG_ID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MED_EXAM_REPORT");

            migrationBuilder.DropTable(
                name: "MED_LAB_MASTER");

            migrationBuilder.DropTable(
                name: "MED_LAB_RESULT");

            migrationBuilder.DropTable(
                name: "MED_PAT_BLOOD_GAS_DETAIL");

            migrationBuilder.DropTable(
                name: "MED_PAT_ECG_INFO");

            migrationBuilder.DropTable(
                name: "MED_PAT_MATERECORD");

            migrationBuilder.DropTable(
                name: "MED_PAT_NEWBORNRECORD");

            migrationBuilder.DropTable(
                name: "MED_PAT_OPERATION");

            migrationBuilder.DropTable(
                name: "MED_PAT_ORDERS");

            migrationBuilder.DropTable(
                name: "MED_PAT_RELATION_INFO");

            migrationBuilder.DropTable(
                name: "MED_PAT_SCORE");

            migrationBuilder.DropTable(
                name: "MED_PAT_TIMENODES");

            migrationBuilder.DropTable(
                name: "MED_PATIENT_DETAILS_INFO");

            migrationBuilder.DropTable(
                name: "MED_PATIENT_REGIST_INFO");

            migrationBuilder.DropTable(
                name: "MED_PHEP_PATIENT_INFO");

            migrationBuilder.DropTable(
                name: "VIEW_PATIENT_DIAGNOSIS");

            migrationBuilder.DropTable(
                name: "ViewOutHosPatientInfo");
        }
    }
}
