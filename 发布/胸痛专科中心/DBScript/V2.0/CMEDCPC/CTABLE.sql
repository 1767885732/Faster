rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建专科-胸痛部分
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
  constraint MED_TIMELINES_PK primary key (ID),
  constraint MED_TIMELINES_U unique (TIME_ID, DOC_ID)
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
prompt Creating table MED_ECG
prompt 心电图信息表
prompt =====================================
prompt
create table MED_ECG
(
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36) not null,
  category					VARCHAR2(60) not null,
  name						VARCHAR2(50),
  gender					VARCHAR2(1),
  age						VARCHAR2(20),
  check_time				DATE,
  report_time				DATE,
  diagnosis_time			DATE,
  file_name					VARCHAR2(256),
  file_type					VARCHAR2(36),
  file_content				CLOB,
  constraint MED_ECG_PK primary key (ID),
  constraint MED_ECG_U unique (regist_id,category)
);

grant select, insert, update, delete on MED_ECG to ROLE_DOCARE;

prompt
prompt Creating table MED_FILES
prompt 附加文件信息表
prompt =====================================
prompt
create table MED_FILES
(
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36) not null,
  category					VARCHAR2(20) not null,  
  file_name					VARCHAR2(256) not null,  
  file_path					VARCHAR2(256),  
  file_type					VARCHAR2(36),
  upload_time				DATE,
  constraint MED_FILES_PK primary key (ID),
  constraint MED_FILES_U unique (regist_id, category, file_name)
);

grant select, insert, update, delete on MED_FILES to ROLE_DOCARE;

prompt
prompt Creating table CPC_REGIST
prompt ========================================
prompt
create table CPC_REGIST
(
  id                      VARCHAR2(36) not null,
  patient_no              VARCHAR2(64),
  name                    VARCHAR2(50),
  gender                  VARCHAR2(1),
  age                     VARCHAR2(10),
  birthday				  DATE,
  nationality			  VARCHAR2(4),
  marriage				  VARCHAR2(2),	
  job					  VARCHAR2(2),	
  culture_degree		  VARCHAR2(2),	
  id_card				  VARCHAR2(1),
  id_card_no              VARCHAR2(20),	
  phone_number            VARCHAR2(30),
  wristband_no			  VARCHAR2(36),
  height				  NUMBER(5,2),	
  weight				  NUMBER(5,2),
  diagnosis				  VARCHAR2(2),
  reception_time		  DATE,
  discharge_time		  DATE,
  integrity				  NUMBER(5,2),
  hospital_id			  VARCHAR2(36),
  hospital_name			  VARCHAR2(60),
  create_name			  VARCHAR2(50),
  create_time			  DATE,
  update_name             VARCHAR2(50),
  update_time			  DATE,
  report_status			  VARCHAR2(1) default '0',
  report_time			  DATE,
  report_id				  VARCHAR2(36),
  is_deleted              VARCHAR2(1) default '0',
  delete_name			  VARCHAR2(50),
  delete_time			  DATE,
  is_completed			  CHAR(1) default '0',
  complete_name			  VARCHAR2(50),
  complete_time			  DATE,
  is_filed				  VARCHAR2(1) default '0',
  filing_name			  VARCHAR2(50),
  filing_time			  DATE,
  is_migration			  VARCHAR2(1),
  constraint CPC_REGIST_PK primary key (ID)
);

grant select, insert, update, delete on CPC_REGIST to ROLE_DOCARE;


prompt
prompt Creating table CPC_ADMISSION
prompt 胸痛接诊表
prompt =====================================
prompt
create table CPC_ADMISSION
(
  id							  VARCHAR2(36) not null,
  regist_id						  VARCHAR2(36) not null, 
  out_regist_no					  VARCHAR2(36),     
  outpatient_no					  VARCHAR2(36),  
  outpatient_sn					  VARCHAR2(36),  
  inpatient_no					  VARCHAR2(36),  
  inpatient_sn					  VARCHAR2(36),  
  insurance						  VARCHAR2(1),
  insurance_no					  VARCHAR2(18),	 
  is_stricken_insurance			  VARCHAR2(1),
  is_attack_uncertain			  VARCHAR2(1),
  attack_time					  DATE,
  attack_zone					  VARCHAR2(1),
  attack_province_code			  VARCHAR2(12),
  attack_province				  VARCHAR2(60),
  attack_city_code				  VARCHAR2(12),
  attack_city					  VARCHAR2(60),
  attack_area_code				  VARCHAR2(12),
  attack_area					  VARCHAR2(60),
  attack_address				  VARCHAR2(120),
  chief_complaint				  VARCHAR2(200),
  distress_case					  VARCHAR2(2),
  distress_case_detail			  VARCHAR2(60),
  coming_type					  VARCHAR2(1),
  ambulance_department			  VARCHAR2(1),
  help_time						  DATE,
  arrived_hospital_time			  DATE,
  is_trans_hospital				  VARCHAR2(1),
  fmc_time						  DATE,
  admission_time				  DATE,  
  admission_doctor				  VARCHAR2(50),
  zcyy_type						  VARCHAR2(1),
  zcyy_hospital_name			  VARCHAR2(80),
  zcyy_visit_time				  DATE,
  zcyy_decision_time			  DATE,	
  zcyy_leave_time				  DATE,	
  attack_dept					  VARCHAR2(80),
  cwys_contact_time				  DATE,	
  leave_dept_time				  DATE,	
  consciousness_type			  VARCHAR2(50),
  respiration_rate				  VARCHAR2(6),
  pulse_rate					  VARCHAR2(6),
  heart_rate					  VARCHAR2(6),
  blood_pressure				  VARCHAR2(20),
  temperature					  VARCHAR2(20),
  rs_screen						  VARCHAR2(1),  
  is_rs							  VARCHAR2(1), 
  is_rs_taboo					  VARCHAR2(1),
  is_rs_direct					  VARCHAR2(1),
  rs_place						  VARCHAR2(1),
  rs_start_agree_time			  DATE,	
  rs_sign_agree_time			  DATE,	
  rs_start_time					  DATE,	
  rs_end_time					  DATE,	
  rs_radiography_time			  DATE,	
  rs_drug_type					  VARCHAR2(1),
  rs_drug_code					  VARCHAR2(1),   
  is_rs_recanalization			  VARCHAR2(1),
  is_remedy_pci					  VARCHAR2(1),
  actual_pci_time				  DATE,
  pci_place						  VARCHAR2(1),
  patient_remarks				  VARCHAR2(500),
  constraint CPC_ADMISSION_PK primary key (ID),
  constraint CPC_ADMISSION_U unique (regist_id)
);

grant select, insert, update, delete on CPC_ADMISSION to ROLE_DOCARE;

prompt
prompt Creating table CPC_DIAGNOSIS
prompt 胸痛诊断表
prompt =====================================
prompt
create table CPC_DIAGNOSIS
(
  id							  VARCHAR2(36) not null,
  regist_id						  VARCHAR2(36) not null,  
  is_ecg						  VARCHAR2(1),
  no_ecg_reason					  VARCHAR2(100),
  ecg_time						  DATE,
  ecg_diagnosis_time			  DATE,
  ecg_transmission				  VARCHAR2(1),
  ecg_transmission_time			  DATE,		
  ecg_transmission_type			  VARCHAR2(1),
  is_ctn						  VARCHAR2(1),
  ctn_type						  VARCHAR2(20),
  ctn_value						  VARCHAR2(20),
  ctn_sampling_time				  DATE,
  ctn_report_time				  DATE,
  is_scr						  VARCHAR2(1),
  scr_value						  VARCHAR2(20),
  is_ddimer						  VARCHAR2(1),
  ddimer_value					  VARCHAR2(20),
  is_bnp						  VARCHAR2(1),
  bnp_value						  VARCHAR2(20),
  is_nt_probnp					  VARCHAR2(1),
  nt_probnp_value				  VARCHAR2(20),
  is_myo						  VARCHAR2(1),
  myo_value						  VARCHAR2(20),
  is_ckmb						  VARCHAR2(1),
  ckmb_value					  VARCHAR2(20),
  is_consultation				  VARCHAR2(1),
  consultation_type				  VARCHAR2(1),
  consultation_notify_time	      DATE,
  consultation_time				  DATE,
  diagnosis						  VARCHAR2(2),
  diagnosis_time				  DATE,
  diagnosis_doctor				  VARCHAR2(50),
  is_giveup_treatment			  VARCHAR2(1),	
  killip_level					  VARCHAR2(1),
  is_bypass_emergency			  VARCHAR2(1),
  is_bypass_ccu				      VARCHAR2(1),  
  grace_risk_factors			  VARCHAR2(15),
  grace_vhr_conditions			  VARCHAR2(30),
  grace_score					  NUMBER(3),
  grace_hazard_level			  VARCHAR2(1),
  grace_rehazard				  VARCHAR2(2),
  grace_rehazard_level			  VARCHAR2(2),
  grace_rehazard_time			  DATE,
  is_ect						  VARCHAR2(1),					  
  ct_notice_time				  DATE,	
  ct_finish_time				  DATE,
  ct_scan_time					  DATE,	
  ct_report_time				  DATE,	
  is_cdu						  VARCHAR2(1),	
  notice_cdu_time				  DATE,	
  cdu_check_time				  DATE,		
  check_result_time				  DATE,		
  is_mri						  VARCHAR2(1),
  csc_notice_time				  DATE,	
  csc_time						  DATE,	
  nacs_type						  VARCHAR2(90),
  nccp_type						  VARCHAR2(30),
  handle_way					  VARCHAR2(2),
  patient_remarks				  VARCHAR2(500),
  constraint CPC_DIAGNOSIS_PK primary key (ID),
  constraint CPC_DIAGNOSIS_U unique (regist_id)
);

grant select, insert, update, delete on CPC_DIAGNOSIS to ROLE_DOCARE;

prompt
prompt Creating table CPC_DIAGNOSIS_TEST
prompt 胸痛检验表
prompt =====================================
prompt
create table CPC_DIAGNOSIS_TEST
(
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36) not null,
  category					VARCHAR2(20) not null,
  sampling_time				DATE,	
  report_time				DATE,	
  result					VARCHAR2(20),
  unit						VARCHAR2(20),
  conclusion				VARCHAR2(20),  
  source					VARCHAR2(1) default '1', 
  is_first					VARCHAR2(1),
  order_no					NUMBER,
  constraint CPC_DIAGNOSIS_TEST_PK primary key (ID)
);

grant select, insert, update, delete on CPC_DIAGNOSIS_TEST to ROLE_DOCARE;

prompt
prompt Creating table CPC_TREATMENT
prompt 胸痛治疗表
prompt =====================================
prompt
create table CPC_TREATMENT (
  id							  VARCHAR2(36) not null,
  regist_id						  VARCHAR2(36) not null,  
  is_antiplatelet				  VARCHAR2(1),
  aspirin_dose					  VARCHAR2(20),
  aspirin_time					  DATE,  
  clopidogrel_dose			  	  VARCHAR2(20),
  clopidogrel_time				  DATE,
  tigrillo_dose					  VARCHAR2(20),
  tigrillo_time					  DATE,
  is_anticoagulation			  VARCHAR2(1),
  anticoagulation_time			  DATE,	
  anticoagulation_drug			  VARCHAR2(1),
  anticoagulation_dose			  VARCHAR2(20),
  anticoagulation_unit			  VARCHAR2(50),  
  is_statin						  VARCHAR2(1),
  is_beta_blocker				  VARCHAR2(1),
  is_reperfusion				  VARCHAR2(1),
  no_reperfusion_measure		  VARCHAR2(30),	
  reperfusion_measure			  VARCHAR2(2),
  reperfusion_measure_rs		  VARCHAR2(2),
  strategy						  VARCHAR2(2),
  invasive_strategy				  VARCHAR2(2),
  is_direct_to_cr				  VARCHAR2(1),
  ecg_transmission				  VARCHAR2(1),
  ecg_transmission_time			  DATE,		
  ecg_transmission_type			  VARCHAR2(1),
  decision_doctor				  VARCHAR2(50),
  decision_pci_time				  DATE,
  pci_start_agree_time			  DATE,
  pci_sign_agree_time			  DATE,	
  pci_actual_time				  DATE,	
  pci_transfer_type				  VARCHAR2(1),
  cabg_decision_time			  DATE,
  cabg_start_time				  DATE,
  cabg_end_time					  DATE,  
  interlayer_type				  VARCHAR2(1),
  risk_level					  VARCHAR2(1),
  rs_screen						  VARCHAR2(1),  
  is_rs							  VARCHAR2(1),
  is_rs_taboo					  VARCHAR2(1),
  is_rs_direct					  VARCHAR2(1),
  rs_place						  VARCHAR2(1),
  rs_start_agree_time			  DATE,	
  rs_sign_agree_time			  DATE,	
  rs_start_time					  DATE,	
  rs_end_time					  DATE,	
  is_rs_radiography				  VARCHAR2(1),
  rs_drug_type					  VARCHAR2(1),
  rs_drug_code					  VARCHAR2(1),   
  is_rs_recanalization			  VARCHAR2(1),
  constraint CPC_TREATMENT_PK primary key (ID),
  constraint CPC_TREATMENT_U unique (regist_id)
);

grant select, insert, update, delete on CPC_TREATMENT to ROLE_DOCARE;

prompt
prompt Creating table CPC_TREATMENT_PCI
prompt 胸痛介入手术表
prompt =====================================
prompt
create table CPC_TREATMENT_PCI
(
  id							VARCHAR2(36) not null,
  regist_id						VARCHAR2(36) not null,   
  start_conduit_time			DATE, 
  activate_conduit_time			DATE,
  patient_arrival_time			DATE,
  intervention_person			VARCHAR2(50), 
  start_puncture_time			DATE,  
  gw_passage_time				DATE,
  start_radiography_time		DATE,
  end_radiography_time			DATE,
  anticoagulation_time			DATE,	
  anticoagulation_drug			VARCHAR2(1),
  anticoagulation_dose			VARCHAR2(20),
  anticoagulation_unit			VARCHAR2(50),  
  puncture_ok_time				DATE,
  end_operation_time			DATE,
  is_delay						VARCHAR2(1),
  delay_reason					VARCHAR2(100),
  approach						VARCHAR2(1),
  cag_position					VARCHAR2(120),  
  intracavity_image				VARCHAR2(2),
  function_test					VARCHAR2(2),
  function_test_value			VARCHAR2(10),
  is_iabp						VARCHAR2(1),
  is_pacemaker					VARCHAR2(1),
  is_ecmo						VARCHAR2(1),
  is_auxiliary_device			VARCHAR2(1),
  complication					VARCHAR2(60),
  constraint CPC_TREATMENT_PCI_PK primary key (ID),
  constraint CPC_TREATMENT_PCI_U unique (regist_id)
);

grant select, insert, update, delete on CPC_TREATMENT_PCI to ROLE_DOCARE;

prompt
prompt Creating table CPC_TREATMENT_PCI_PART
prompt 胸痛介入手术部位表
prompt =====================================
prompt
create table CPC_TREATMENT_PCI_PART
(
  id							VARCHAR2(36) not null,
  regist_id						VARCHAR2(36) not null,     
  part_id						VARCHAR2(4),  
  narrow_level					VARCHAR2(2),
  timi_level					VARCHAR2(1),
  bracket_thrombus				VARCHAR2(1),
  branching_sick				VARCHAR2(1),
  is_cto						VARCHAR2(1),
  is_calcified_lesions			VARCHAR2(1),
  is_culprit_lesion				VARCHAR2(1),
  is_pci						VARCHAR2(1),
  is_handle_in_pci				VARCHAR2(1),
  handle_way					VARCHAR2(30),
  timi_level_post				VARCHAR2(1),
  gw_passage_time				DATE,
  bracket_count					VARCHAR2(2),
  bracket_type					VARCHAR2(2),
  constraint CPC_TREATMENT_PCI_PART_PK primary key (ID),
  constraint CPC_TREATMENT_PCI_PART_U unique (regist_id, part_id)
);

grant select, insert, update, delete on CPC_TREATMENT_PCI_PART to ROLE_DOCARE;

prompt
prompt Creating table CPC_DISCHARGE
prompt 胸痛出院表
prompt =====================================
prompt
create table CPC_DISCHARGE (
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36) not null,
  diagnosis					VARCHAR2(2),
  diagnosis_time			DATE,
  nacs_type					VARCHAR2(60),
  nccp_type					VARCHAR2(30),
  is_heart_failure			VARCHAR2(1),
  complication				VARCHAR2(60),
  is_hypertension			VARCHAR2(1),
  is_hyperlipidemia			VARCHAR2(1),
  is_diabetes				VARCHAR2(1),
  is_smoking				VARCHAR2(1),
  smoking_situation			VARCHAR2(1),
  is_obesity				VARCHAR2(1),
  is_cvd_hr					VARCHAR2(1),
  is_chd					VARCHAR2(1),
  is_chd_hor				VARCHAR2(1),
  is_af						VARCHAR2(1),
  af_type					VARCHAR2(1),
  is_chf					VARCHAR2(1),
  chf_nyha					VARCHAR2(1),
  is_vhd					VARCHAR2(1),
  is_cvd					VARCHAR2(1),  
  cvd_type					VARCHAR2(1),
  is_pad					VARCHAR2(1),
  is_aneurysm				VARCHAR2(1),
  is_copd					VARCHAR2(1),
  is_ckd					VARCHAR2(1),
  is_anemia					VARCHAR2(1),
  is_peptic_ulcer			VARCHAR2(1),
  is_thyroid_dysfunction	VARCHAR2(1),
  is_malignant_tumor		VARCHAR2(1),
  is_troponin				VARCHAR2(1),
  is_bnp					VARCHAR2(1),
  is_tc						VARCHAR2(1),
  is_tg						VARCHAR2(1),
  is_hdl_c					VARCHAR2(1),
  is_ldl_c					VARCHAR2(1),
  is_ucc					VARCHAR2(1),
  lvef						NUMBER(5,2),
  is_ventricular_aneurysm	VARCHAR2(1),
  is_rwma					VARCHAR2(1),
  hod						NUMBER(6),
  total_cost				NUMBER(10,2),
  outcome_code				VARCHAR2(1),
  leave_time				DATE,
  treatment_result			VARCHAR2(1),
  is_antiplatelet			VARCHAR2(1),
  is_acei_or_arb			VARCHAR2(1),
  is_lipid_regulation		VARCHAR2(1),
  is_beta_blocker			VARCHAR2(1),
  is_net_hospital			VARCHAR2(1),
  hospital_name				VARCHAR2(100),	
  dept_transfer_time		DATE,	
  admission_dept			varchar2(80),
  dept_transfer_reason		VARCHAR2(100),
  death_time				DATE,
  death_cause				VARCHAR2(1),
  death_cause_desc			VARCHAR2(500),
  education					VARCHAR2(60),
  patient_remarks			VARCHAR2(500),
  constraint CPC_DISCHARGE_PK primary key (ID),
  constraint CPC_DISCHARGE_U unique (regist_id)
);

grant select, insert, update, delete on CPC_DISCHARGE to ROLE_DOCARE;

prompt
prompt Creating table CPC_DISCHARGE_DRUGS
prompt 胸痛出院带药表
prompt =====================================
prompt
create table CPC_DISCHARGE_DRUGS
(
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36) not null,
  code						VARCHAR2(32),
  name						VARCHAR2(50),
  category					VARCHAR2(36) not null,  
  dose						VARCHAR2(20),
  unit						VARCHAR2(20),
  rate_time					VARCHAR2(1),
  rate_count				VARCHAR2(1),
  order_no					NUMBER,
  constraint CPC_DISCHARGE_DRUGS_PK primary key (ID)
);

grant select, insert, update, delete on CPC_DISCHARGE_DRUGS to ROLE_DOCARE;

prompt
prompt Creating table CPC_DISCHARGE_TEST
prompt 胸痛出院检查表
prompt =====================================
prompt
create table CPC_DISCHARGE_TEST
(
  id						VARCHAR2(36) not null,
  regist_id					VARCHAR2(36) not null,
  category					VARCHAR2(32) not null,
  result					VARCHAR2(20),
  unit						VARCHAR2(20),
  conclusion				VARCHAR2(20),  
  order_no					NUMBER,
  constraint CPC_DISCHARGE_TEST_PK primary key (ID),
  constraint CPC_DISCHARGE_TEST_U unique (regist_id, category)
);

grant select, insert, update, delete on CPC_DISCHARGE_TEST to ROLE_DOCARE;
