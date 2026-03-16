rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建卒中部分
rem=====================================================


prompt
prompt Creating table MED_ESTIMATE
prompt ===================================
prompt
create table MED_ESTIMATE
(
  id                      VARCHAR2(36) not null,
  regist_id				  VARCHAR2(36) not null,
  RELATION_ID			  VARCHAR2(36),
  Code					  VARCHAR2(36),
  Name				  	  VARCHAR2(16),
  Title					  VARCHAR2(80),  
  Description			  VARCHAR2(500),
  Doctor				  VARCHAR2(50),
  Time			  		  DATE,
  Value			  		  VARCHAR2(30),
  CREATE_NAME			  VARCHAR2(50),
  CREATE_DATE			  DATE,
  UPDATE_NAME			  VARCHAR2(50),
  UPDATE_DATE			  DATE,
  constraint MED_ESTIMATE_PK primary key (ID)
)
;
grant select, insert, update, delete on MED_ESTIMATE to ROLE_DOCARE;

prompt
prompt Creating table MED_ESTIMATE_ITEM
prompt ===================================
prompt
create table MED_ESTIMATE_ITEM
(
  id                      VARCHAR2(36) not null,
  ESTIMATE_ID			  VARCHAR2(36) not null,
  CODE			  		  VARCHAR2(36),
  Type				  	  VARCHAR2(16),
  Title					  VARCHAR2(80),  
  Description			  VARCHAR2(500),
  Content				  CLOB,
  Group_No		  		  VARCHAR2(36),
  Group_Name			  VARCHAR2(80),
  Order_No			      NUMBER(11),
  constraint MED_ESTIMATE_ITEM_PK primary key (ID)
)
;
grant select, insert, update, delete on MED_ESTIMATE_ITEM to ROLE_DOCARE;

prompt
prompt Creating table MED_DICTIONARY
prompt ===================================
prompt
create table MED_DICTIONARY
(
	CODE			VARCHAR2(64) not null,
	NAME			VARCHAR2(128) not null,
	KEY				VARCHAR2(32) not null,
	VALUE			VARCHAR2(128) not null,
	DESCRIPTION		VARCHAR2(512),
	PinYin			VARCHAR2(20),
	IS_DELETE		VARCHAR2(1),
	CUSTOM_CODE		VARCHAR2(20),
	ORDER_NO		NUMBER(11),
	constraint MED_DICTIONARY_PK primary key (CODE, KEY)
  );

grant select, insert, update, delete on MED_DICTIONARY to ROLE_DOCARE;

prompt
prompt Creating table CM_TIME_NODES
prompt ===================================
prompt
create table CM_TIME_NODES
(
  id          VARCHAR2(36) not null,
  name        VARCHAR2(64) not null,
  name_en     VARCHAR2(64) not null,
  description VARCHAR2(512),
  is_key      CHAR(1) default '0',
  sequence    NUMBER(5,1),
  constraint CM_TIME_NODES_PK primary key (ID)
)
;
grant select, insert, update, delete on CM_TIME_NODES to ROLE_DOCARE;

prompt
prompt Creating table MED_TIMELINES
prompt =======================================
prompt
create table MED_TIMELINES
(
  id          VARCHAR2(36) not null,
  time_id     VARCHAR2(64) not null,
  time_value  DATE,
  doc_id      VARCHAR2(36) not null,
  is_key_time CHAR(1) default '0',
  constraint MED_TIMELINES_PK primary key (ID)
)
;
grant select, insert, update, delete, alter on MED_TIMELINES to ROLE_DOCARE;

prompt
prompt Creating table CM_MENUS
prompt ===================================
prompt
create table CM_MENUS
(
  id             VARCHAR2(36) not null,
  name           VARCHAR2(100),
  action         VARCHAR2(250),
  icon           VARCHAR2(32),
  description    VARCHAR2(200),
  parent_id      VARCHAR2(36),
  permission_key VARCHAR2(100),
  sequence       NUMBER(4),
  enabled        CHAR(1),
  constraint CM_MENUS_PK primary key (ID)
)
;
grant select, insert, update, delete, alter on CM_MENUS to ROLE_DOCARE;


prompt
prompt Creating table MED_MEDICAL_ACTIONS
prompt ========================================
prompt
create table MED_MEDICAL_ACTIONS
(
  id             VARCHAR2(36) not null,
  action         VARCHAR2(36),
  action_name    VARCHAR2(50),
  action_time    DATE,
  description    CLOB,
  event_id       VARCHAR2(36),
  patient_name   VARCHAR2(50),
  patient_age    VARCHAR2(10),
  patient_idcard VARCHAR2(18),
  patient_phone  VARCHAR2(15),
  patient_sex    VARCHAR2(1),
  user_id        VARCHAR2(30),
  user_name      VARCHAR2(50),
  constraint MED_MEDICAL_ACTIONS_PK primary key (id)
)
;
grant select, insert, update, delete, alter on MED_MEDICAL_ACTIONS to ROLE_DOCARE;

prompt
prompt Creating table CM_SYSTEM_SETTINGS
prompt ===========================================
prompt
create table CM_SYSTEM_SETTINGS
(
  id             VARCHAR2(36) not null,
  setting        CLOB,
  constraint CM_SYSTEM_SETTINGS_PK primary key (id)
)
;
grant select, insert, update, delete on CM_SYSTEM_SETTINGS to ROLE_DOCARE;

prompt
prompt Creating table MED_MARKS
prompt ===========================================
prompt
CREATE TABLE MED_MARKS(
       KEY VARCHAR2(36),
       TYPE VARCHAR2(2),
       FLAG VARCHAR2(2),
       NG_COUNT NUMBER(6),
       OK_COUNT NUMBER(6),
       COUNT NUMBER(6),
       RELATION_KEY VARCHAR2(200),
       OK_MARK_TIME DATE,
       NG_MARK_TIME DATE,
       MARK_TIME DATE,
       MARK_DESC VARCHAR2(500),
       constraint MED_MARKS_PK primary key (KEY, TYPE)
)
;
grant select, insert, update, delete on MED_MARKS to ROLE_DOCARE;

prompt
prompt Creating table CVA_REGIST
prompt ========================================
prompt
create table CVA_REGIST
(
id                  VARCHAR2(36) not null,
  regist_date         DATE,
  patient_id          VARCHAR2(50),
  ehr_id              VARCHAR2(100),
  clinic_no           VARCHAR2(36),
  clinic_sn           VARCHAR2(36),
  mr_no               VARCHAR2(36),
  name_cn             VARCHAR2(50),
  gender              VARCHAR2(1),
  age                 VARCHAR2(10),
  name_en             VARCHAR2(50),
  idcard_no           VARCHAR2(18),
  citizen_card_no     VARCHAR2(50),
  coming_type         VARCHAR2(1),
  phone_no1           VARCHAR2(20),
  phone_no2           VARCHAR2(20),
  tel_no              VARCHAR2(20),
  org_code            VARCHAR2(18),
  org_name            VARCHAR2(80),
  disease_time        DATE,
  admission_time      DATE,
  discharge_time      DATE,
  phase_status        VARCHAR2(2),
  integrity           NUMBER(5,2),
  create_account      VARCHAR2(30),
  create_name         VARCHAR2(50),
  create_date         DATE,
  update_name         VARCHAR2(50),
  update_date         DATE,
  is_delete           CHAR(1) default '0',
  is_completed        CHAR(1) default '0',
  is_filing           CHAR(1) default '0',
  filing_date         DATE,
  followup_status     VARCHAR2(1) default '1',
  diagnosis_type      VARCHAR2(1),
  treatment_decision  VARCHAR2(2),
  is_report           VARCHAR2(1),
  is_outcome          CHAR(1),
  green_channel_type  CHAR(1),
  attack_address      VARCHAR2(100),
  followup_doc_status CHAR(1) default '0',
  followup_nur_status CHAR(1) default '0',
  constraint CVA_REGIST_PK primary key (ID)
);

grant select, insert, update, delete on CVA_REGIST to ROLE_DOCARE;

prompt
prompt Creating table CVA_FIRST_AID
prompt =====================================
prompt
create table CVA_FIRST_AID
(
  ID                            VARCHAR2(36) not null,
  REGIST_ID                     VARCHAR2(36),
  CALL_TIME						DATE,
  DEPARTURE_TIME                DATE,
  ARRIVE_PATIENT_SIDE_TIME		DATE,
  FMC_DOCTOR					VARCHAR2(100),
  FMC_TIME						DATE,
  FMC_PHEP_ECG_TIME				DATE,
  FMC_ECG_DIAGNOSIS_TIME		DATE,
  T             				NUMBER(3, 1),
  P                   			NUMBER(3),
  R                   			NUMBER(3),
  SP		  					NUMBER(3),
  DP	  						NUMBER(3),
  HR              				NUMBER(3),
  SPO							NUMBER(3),
  BS							NUMBER(4,2),
  sense            				VARCHAR2(60),
  FAST							VARCHAR2(100),
  constraint CVA_FIRST_AID_PK primary key (ID)
);

create unique index CVA_FIRST_AID_IDX on CVA_FIRST_AID(REGIST_ID);

grant select, insert, update, delete on CVA_FIRST_AID to ROLE_DOCARE; 

prompt
prompt Creating table CVA_EMERGENCY
prompt =====================================
prompt
create table CVA_EMERGENCY
(
  id								VARCHAR2(36) not null,
  regist_id							VARCHAR2(36),
  salvage_time						DATE,
  notice_ct							CHAR(1),
  notice_ct_arrivetime				DATE,
  notice_neurology					CHAR(1),
  registration_time					DATE,
  examine_open_time					DATE,
  examine_result_time				DATE,
  ecg_time							DATE,
  ecg_diagnosis_time							DATE,
  ecg_result						VARCHAR2(100),
  stroke_team_admission_time		DATE,
  emergency_doctor					VARCHAR2(100),
  ct_open_time						DATE,
  ct_start_time						DATE,
  ct_end_time						DATE,
  ct_result_time					DATE,
  ct_is_hemorrhage					CHAR(1),
  ct_result							VARCHAR2(4000),
  blood_clotting_result_time		DATE,
  fingertip_blood_sugar				VARCHAR2(20),
  arrived_hospital_time				DATE,
  open_vein_time					DATE,
  platelet_count					VARCHAR2(20),
  pt_result							VARCHAR2(20),
  aptt_result						VARCHAR2(20),
  inr_result						VARCHAR2(20),
  chief_complaint					VARCHAR2(100),
  history_present_illness			VARCHAR2(100),
  past_medical_history				VARCHAR2(50),
  salvage_end_time					DATE,
  fmc_inhospital_ecg_time			DATE,
  blood_clot_open_time				DATE,
  diagnosis_time					DATE,
  emergency_doctor_arrival_time		DATE,
  diagnosis_type					CHAR(1),
  outhos_method						VARCHAR2(2),
  personal_history					VARCHAR2(50),
  nihss_score						VARCHAR2(1),
  nihss_score_value					number(3),
  nihss_score_id					VARCHAR2(36),
  mrs_score_id						VARCHAR2(36),
  mrs_score_value					number(3),
  ct_doc_arrival_time				DATE,
  sn_doc_arrival_time				DATE,
  blood_collection_time				DATE,
  second_mrs_start_time				DATE,			
  second_mrs_end_time				DATE,		
  mrs_end_time						DATE,		
  mrs_start_time					DATE,			
  second_ct_start_time				DATE,			
  second_ct_end_time				DATE,			
  is_antiplatelet					VARCHAR2(10),
  not_use_reason_antiplatelet		VARCHAR2(500),
  is_anticoagulation				VARCHAR2(10),
  not_use_reason_anticoagulation	VARCHAR2(500),
  is_antihypertensive				VARCHAR2(10),
  not_use_reason_antihyperten		VARCHAR2(500),
  is_hypoglycemic					VARCHAR2(10),
  not_use_reason_hypoglycemic		VARCHAR2(500),
  is_lipidmodulation				VARCHAR2(10),
  not_use_reason_lipidmodulation	VARCHAR2(500),
  is_otherdrugs						VARCHAR2(10),
  not_use_reason_otherdrugs			VARCHAR2(500),
  constraint CVA_EMERGENCY_PK primary key (ID)
);
create unique index CVA_EMERGENCY_IDX on CVA_EMERGENCY(REGIST_ID);

grant select, insert, update, delete on CVA_EMERGENCY to ROLE_DOCARE;


prompt
prompt Creating table CVA_DISEASE_HISTORY
prompt =====================================
prompt
create table CVA_DISEASE_HISTORY
(
  id                VARCHAR2(36) not null,
  regist_id         VARCHAR2(36),
  patient_id        VARCHAR2(50),
  idcard            VARCHAR2(18),
  formerly_type     VARCHAR2(6),
  disease_code      VARCHAR2(18),
  disease_name      VARCHAR2(100),
  other_desc        VARCHAR2(100),
  confirmation_date DATE,
  outcome_code      VARCHAR2(1),
  constraint CVA_DISEASE_HISTORY_PK primary key (ID)
);

grant select, insert, update, delete on CVA_DISEASE_HISTORY to ROLE_DOCARE;

prompt
prompt Creating table CVA_DRUG_HISTORY
prompt =====================================
prompt
create table CVA_DRUG_HISTORY
(
  id                    VARCHAR2(36) not null,
  regist_id             VARCHAR2(36),
  patient_id            VARCHAR2(50),
  drug_use_frequency    VARCHAR2(20),
  drug_use_dose_unit    VARCHAR2(6),
  drug_use_state        VARCHAR2(20),
  drug_use_route_code   VARCHAR2(3),
  start_date            DATE,
  stop_date             DATE,
  drug_type_code        VARCHAR2(18),
  drug_type_name        VARCHAR2(50),
  drug_generic_code     VARCHAR2(18),
  drug_generic_name     VARCHAR2(50),
  medication_compliance VARCHAR2(1),
  constraint CVA_DRUG_HISTORY_PK primary key (ID)
);

grant select, insert, update, delete on CVA_DRUG_HISTORY to ROLE_DOCARE;

prompt
prompt Creating table CVA_DRUG_USE
prompt =====================================
prompt
create table CVA_DRUG_USE
(
  id                    VARCHAR2(36) not null,
  regist_id             VARCHAR2(36),
  patient_id            VARCHAR2(50),
  cm_type               VARCHAR2(2),
  drug_type_code        VARCHAR2(18),
  drug_type_name        VARCHAR2(50),
  drug_generic_name     VARCHAR2(50),
  drug_trade_name       VARCHAR2(50),
  drug_use_days         NUMBER(4),
  drug_use_frequency    VARCHAR2(20),
  drug_use_dose_unit    VARCHAR2(6),
  drug_use_total_dose   NUMBER(12),
  drug_use_route_code   VARCHAR2(3),
  start_date            DATE,
  stop_date             DATE,
  drug_specifications   VARCHAR2(20),
  doctor_advice         VARCHAR2(200),
  medication_compliance VARCHAR2(1),
  production_unit_name  VARCHAR2(50),
  is_execute_flag       CHAR(1),
  execute_frequency     VARCHAR2(200),
  execute_result        VARCHAR2(200),
  unit_price            NUMBER(5),
  subtotal              NUMBER(8),
  remarks               VARCHAR2(200),
  quantity              NUMBER(6),
  quantity_unit         VARCHAR2(20),
  drug_generic_code     VARCHAR2(10),
  drug_use_category     VARCHAR2(18),
  drug_use_dose         NUMBER(8,3),
  constraint CVA_DRUG_USE_PK primary key (ID)
);

grant select, insert, update, delete on CVA_DRUG_USE to ROLE_DOCARE;

prompt
prompt Creating table CVA_PATIENT_BED
prompt ===================================
prompt
create table CVA_PATIENT_BED
(
  id                      VARCHAR2(50) not null,
  patient_id              VARCHAR2(50) not null,
  patient_name            VARCHAR2(50),
  enter_time              Date,
  leave_time              Date,
  bed_no				  VARCHAR2(50),
  constraint CVA_PATIENT_BED_PK primary key (ID)
);

grant select, insert, update, delete on CVA_PATIENT_BED to ROLE_DOCARE;

prompt
prompt Creating table CVA_TREATMENT
prompt ===================================
prompt
create table CVA_TREATMENT
(
  id								VARCHAR2(36) not null,
  regist_id							VARCHAR2(36),
  diagnosis_type					CHAR(1),
  diagnosis_time					DATE,
  diagnosis_doctor					VARCHAR2(50),
  outhos_method						VARCHAR2(2),
  treatment_decision				VARCHAR2(2),
  inclusion_criteria				VARCHAR2(16),
  exclusion_criteria				VARCHAR2(16),
  is_throm							CHAR(1),
  rel_exclusion_criteria_3h			VARCHAR2(16),
  rel_exclusion_criteria_4_5h		VARCHAR2(16),
  throm_agree_sign_time				DATE,
  rt_pa_open_time					DATE,
  throm_vein_time					DATE,
  weight							NUMBER(5),
  rt_pa_dose						NUMBER(5,2),
  rt_pa_dosage_unit					VARCHAR2(20),
  rt_pa_sp_dose						NUMBER(5),
  rt_pa_id_dose						NUMBER(5),
  no_throm_reasons					VARCHAR2(50),
  illness_state_in24h				VARCHAR2(1),
  throm_vein_end_time				DATE,
  throm_dose						VARCHAR2(50),
  is_other_drug						VARCHAR2(1),
  other_drug						VARCHAR2(50),
  iat_treatment						VARCHAR2(50),
  iat_embolectomy_device			VARCHAR2(16),
  iat_is_stenting					VARCHAR2(1),
  iat_stenting_time					DATE,
  is_rt_pa							VARCHAR2(1),
  is_urokinase						VARCHAR2(6),
  iat_destination_after_pci			VARCHAR2(2),
  urokinase_dose					NUMBER(5),
  throm_dose_other					NUMBER(3,1),
  no_throm_other_reasons			VARCHAR2(50),
  iat_no_throm_reasons				VARCHAR2(1),
  iat_is_mc							VARCHAR2(1),
  iat_is_mc_cta						VARCHAR2(1),
  iat_is_mc_ctp						VARCHAR2(1),
  iat_is_mc_mri_mra					VARCHAR2(1),
  iat_is_mc_pwi						VARCHAR2(1),
  iat_is_mc_vep						VARCHAR2(16),
  iat_other_embolectomy_device		VARCHAR2(100),
  iat_failure_thrombectomy			CHAR(1),
  iat_failure_thrombectomy_count	NUMBER(5),
  iat_mc_start_time					DATE,
  iat_mc_end_time					DATE,
  iat_reach_cr_time					DATE,
  iat_fas_end_time					DATE,
  iat_archtographic_time			DATE,
  iat_itr_time						DATE,
  iat_throm_end_time				DATE,
  is_allergy						CHAR(1),
  is_hemorrhagic_transformation		CHAR(1),
  IAT_EMBOLECTOMY_DEVICE_OTHER		VARCHAR2(50),
  iat_reach_cr_end_time				DATE,
  artery_throm_time					DATE,
  artery_is_other_drug				VARCHAR2(1),
  artery_other_drug					VARCHAR2(50),
  artery_is_allergy					CHAR(1),
  artery_is_transformation			CHAR(1),
  artery_is_urokinase				VARCHAR2(6),
  artery_urokinase_dose				NUMBER(5),
  iat_start_cr_time					date,
  iat_activation_cr_time			date,
  iat_start_puncture_time			date,
  iat_success_puncture_time			date,
  iat_end_archtographic_time		date,
  try_embolectomy1_time				date,
  try_embolectomy2_time				date,
  try_embolectomy3_time				date,
  try_embolectomy4_time				date,
  try_embolectomy5_time				date,
  try_embolectomy6_time				date,
  other_artery_urokinase			VARCHAR2(50),
  other_urokinase					VARCHAR2(50),
  thrombolytic_termination_time		date,
  thrombolytic_end_time				date,
  artery_rt_pa_dose					NUMBER(5,2),
  thrombolytic_reaction				VARCHAR2(10),
  is_embolectomy					VARCHAR2(1),
  constraint CVA_TREATMENT_PK primary key (ID)
);

create unique index CVA_TREATMENT_IDX on CVA_TREATMENT(REGIST_ID);

grant select, insert, update, delete on CVA_TREATMENT to ROLE_DOCARE;

prompt
prompt Creating table CVA_INHOSPITAL
prompt ===================================
prompt
create table CVA_INHOSPITAL
(
  id                        VARCHAR2(36) not null,
  regist_id                 VARCHAR2(36),
  inpatient_no              VARCHAR2(36),
  inpatient_sn              VARCHAR2(36),
  inhos_date                DATE,
  inhos_state               VARCHAR2(100),
  other_symptom             VARCHAR2(16),
  OCSP_TYPE					VARCHAR2(2),
  TOAST_TYPE				VARCHAR2(2),
  CISS_TYPE					VARCHAR2(2),
  responsible_vessel		VARCHAR2(16),
  NIHSS_SCORE_ID			VARCHAR2(36),
  NIHSS_SCORE_VALUE			VARCHAR2(20),
  GCS_SCORE_ID				VARCHAR2(36),
  GCS_SCORE_VALUE			VARCHAR2(20),
  ABCD2_SCORE_ID			VARCHAR2(36),
  ABCD2_SCORE_VALUE			VARCHAR2(20),
  CHADS2_VASC_SCORE_ID		VARCHAR2(36),
  CHADS2_VASC_SCORE_VALUE	VARCHAR2(20),
  MRS_SCORE_VALUE			VARCHAR2(36),
  MRS_SCORE_ID				VARCHAR2(20),
  IS_USE_AD                 VARCHAR2(1),
  NOT_USE_AD_REASON         VARCHAR2(500),
  IS_USE_AFD                VARCHAR2(1),
  NOT_USE_AFD_REASON        VARCHAR2(500),
  IS_USE_LRD                VARCHAR2(1),
  NOT_USE_LRD_REASON        VARCHAR2(500),
  IS_USE_SD                 VARCHAR2(1),
  NOT_USE_SD_REASON         VARCHAR2(500),
  IS_USE_ICCD               VARCHAR2(1),
  NOT_USE_ICCD_REASON       VARCHAR2(500),
  constraint CVA_INHOSPITAL_PK primary key (ID)
);

create unique index CVA_INHOSPITAL_IDX on CVA_INHOSPITAL(REGIST_ID);

grant select, insert, update, delete on CVA_INHOSPITAL to ROLE_DOCARE;

prompt
prompt Creating table CVA_FOLLOWUP_CALENDAR
prompt =====================================
prompt
CREATE TABLE CVA_FOLLOWUP_CALENDAR
(
  id                    VARCHAR2(36) not null,
  regist_id             VARCHAR2(36) not null,
  plan_date             DATE,
  event_name            VARCHAR2(64),
  event_desc            VARCHAR2(512),
  followup_date         DATE,
  followup_by           VARCHAR2(50),
  followup_info         CLOB,
  followup_type         VARCHAR2(2) not null,
  is_again_hospitalized CHAR(1) default '0',
  url                   VARCHAR2(500),
  is_event_completed    CHAR(1) default '0',
  is_die                CHAR(1) default '0',
  CONSTRAINT CVA_FOLLOWUP_CALENDAR_PK PRIMARY KEY (ID)
);


--prompt
--prompt Creating table CVA_FOLLOWUP
--prompt =====================================
--prompt
--create table CVA_FOLLOWUP_INFO
--(
--  ID						VARCHAR2(36) not null,
--  REGIST_ID					VARCHAR2(36) not null,
--  FOLLOWUP_CALENDAR_ID		VARCHAR2(36) not null,
--);


prompt
prompt Creating table CVA_OUTCOME
prompt =====================================
prompt
create table CVA_OUTCOME
(
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36),
  discharge_time			DATE,
  diagnosis					VARCHAR2(100),
  other_diagnosis			VARCHAR2(100),
  is_ce						CHAR(1),
  outhos_method				VARCHAR2(2),
  total_amount				NUMBER(10),
  drug_expense				NUMBER(10),
  examine_expense			NUMBER(10),
  hod						NUMBER(6),
  Antiplatelet_Used			VARCHAR2(1),
  Antiplatelet_Reason		VARCHAR2(500),	
  Anticoagulation_Used		VARCHAR2(1),	
  Anticoagulation_Reason	VARCHAR2(500),	
  Antihypertensive_Used		VARCHAR2(1),	
  Antihypertensive_Reason	VARCHAR2(500),	
  Hypoglycemic_Used			VARCHAR2(1),
  Hypoglycemic_Reason		VARCHAR2(500),	
  Lipidmodulation_Used		VARCHAR2(1),
  Lipidmodulation_Reason	VARCHAR2(500),	
  NIHSS_SCORE_ID			VARCHAR2(36),
  NIHSS_SCORE_VALUE			VARCHAR2(20),
  WST_SCORE_ID				VARCHAR2(36),
  WST_SCORE_VALUE			VARCHAR2(20),
  IS_QUIT_SMOKING			VARCHAR2(2),
  constraint CVA_OUTCOME_PK primary key (ID)
);

create unique index CVA_OUTCOME_IDX on CVA_OUTCOME(REGIST_ID);

grant select, insert, update, delete on CVA_OUTCOME to ROLE_DOCARE;
