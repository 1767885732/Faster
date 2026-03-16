rem=====================================================
rem     作    者：基础研发部接口组
rem     整理时间：2018-12-25
rem     说    明：创建接口中间表部分
rem=====================================================

prompt
prompt Creating table MED_LAB_RESULT
prompt ===================================
prompt
create table MED_LAB_RESULT
(
  test_no            VARCHAR2(40) not null,
  item_no            VARCHAR2(40) not null,
  report_item_name   VARCHAR2(40),
  report_item_code   VARCHAR2(40),
  abnormal_indicator VARCHAR2(40),
  result             VARCHAR2(40),
  units              VARCHAR2(40),
  result_date_time   DATE,
  reference_result   VARCHAR2(40)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
 
alter table MED_LAB_RESULT
  add constraint PK_MED_LAB_RESULT primary key (TEST_NO, ITEM_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_LAB_RESULT to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_ECG_INFO
prompt ===================================
prompt
create table MED_PAT_ECG_INFO
(
  patient_no  VARCHAR2(40) not null,
  exam_time   DATE not null,
  report_time DATE,
  diag_time   DATE,
  diag_desc   VARCHAR2(200),
  diag_source VARCHAR2(40),
  ecg_path    VARCHAR2(200),
  ecg_url     VARCHAR2(200)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255;

alter table MED_PAT_ECG_INFO
  add constraint PK_MED_PAT_ECG_INFO primary key (PATIENT_NO, EXAM_TIME)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255;

grant select, insert, update, delete on MED_PAT_ECG_INFO to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_MATERECORD
prompt ===================================
prompt
create table MED_PAT_MATERECORD
(
  PATIENT_NO        VARCHAR2(40) not null,
  IN_DEPT           VARCHAR2(40),
  BED_NO            VARCHAR2(40),
  IN_HOS_TIME       DATE,
  IN_COMPLAINT      VARCHAR2(200),
  PREGANET_WEEK     VARCHAR2(50),
  PREGANET_TIMES    VARCHAR2(50),
  PRODUCT_TIMES     VARCHAR2(50),
  IN_SENSE          VARCHAR2(20),
  IN_T              VARCHAR2(20),
  IN_P              VARCHAR2(20),
  IN_R              VARCHAR2(20),
  IN_BP             VARCHAR2(20),
  IN_SPO2           VARCHAR2(20),
  IN_GLU            VARCHAR2(20),
  FETEL_HEART       VARCHAR2(20),
  UTER_HEIGHT       VARCHAR2(20),
  ABD_INDEX         VARCHAR2(20),
  PAST_HISTORY      VARCHAR2(1000),
  FAMILY_HISTORY    VARCHAR2(1000),
  MENARCHE_AGE      VARCHAR2(10),
  MENSTRUAL_DAY     VARCHAR2(10),
  MENSTRUAL_CYCLE   VARCHAR2(10),
  MENSTRUAL_VOLUME  VARCHAR2(10),
  DYSMENORRHEA      VARCHAR2(10),
  INITIAL_DIAGNOSIS VARCHAR2(1000),
  INITIAL_DOCTOR    VARCHAR2(20),
  INHOS_DIAGNOSIS   VARCHAR2(1000),
  INHOS_DOCTOR      VARCHAR2(20),
  HIGH_RISK         VARCHAR2(2000),
  SPECIAL_TREATMENT VARCHAR2(200)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 8
    minextents 1
    maxextents unlimited
  );

comment on column MED_PAT_MATERECORD.PATIENT_NO
  is '患者编号';
comment on column MED_PAT_MATERECORD.IN_DEPT
  is '入院科室';
comment on column MED_PAT_MATERECORD.BED_NO
  is '床位';
comment on column MED_PAT_MATERECORD.IN_HOS_TIME
  is '入院时间';
comment on column MED_PAT_MATERECORD.IN_COMPLAINT
  is '主诉';
comment on column MED_PAT_MATERECORD.PREGANET_WEEK
  is '孕周';
comment on column MED_PAT_MATERECORD.PREGANET_TIMES
  is '孕次';
comment on column MED_PAT_MATERECORD.PRODUCT_TIMES
  is '产次';
comment on column MED_PAT_MATERECORD.IN_SENSE
  is '意识';
comment on column MED_PAT_MATERECORD.IN_T
  is '体温';
comment on column MED_PAT_MATERECORD.IN_P
  is '脉搏';
comment on column MED_PAT_MATERECORD.IN_R
  is '呼吸';
comment on column MED_PAT_MATERECORD.IN_BP
  is '血压';
comment on column MED_PAT_MATERECORD.IN_SPO2
  is '血氧';
comment on column MED_PAT_MATERECORD.IN_GLU
  is '血糖';
comment on column MED_PAT_MATERECORD.FETEL_HEART
  is '胎心';
comment on column MED_PAT_MATERECORD.UTER_HEIGHT
  is '宫高';
comment on column MED_PAT_MATERECORD.ABD_INDEX
  is '腹围';
comment on column MED_PAT_MATERECORD.PAST_HISTORY
  is '既往史';
comment on column MED_PAT_MATERECORD.FAMILY_HISTORY
  is '家族史';
comment on column MED_PAT_MATERECORD.MENARCHE_AGE
  is '初潮年龄';
comment on column MED_PAT_MATERECORD.MENSTRUAL_DAY
  is '经期天数';
comment on column MED_PAT_MATERECORD.MENSTRUAL_CYCLE
  is '经期周期';
comment on column MED_PAT_MATERECORD.MENSTRUAL_VOLUME
  is '月经量';
comment on column MED_PAT_MATERECORD.DYSMENORRHEA
  is '痛经';
comment on column MED_PAT_MATERECORD.INITIAL_DIAGNOSIS
  is '初步诊断';
comment on column MED_PAT_MATERECORD.INITIAL_DOCTOR
  is '初步诊断医生';
comment on column MED_PAT_MATERECORD.INHOS_DIAGNOSIS
  is '入院诊断';
comment on column MED_PAT_MATERECORD.INHOS_DOCTOR
  is '入院诊断医生';
comment on column MED_PAT_MATERECORD.HIGH_RISK
  is '高危因素';
comment on column MED_PAT_MATERECORD.SPECIAL_TREATMENT
  is '特殊治疗措施';

alter table MED_PAT_MATERECORD
  add constraint PK_MATERERODE primary key (PATIENT_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
grant select, insert, update, delete on MED_PAT_MATERECORD to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_NEWBORNRECORD
prompt ===================================
prompt
create table MED_PAT_NEWBORNRECORD
(
  patient_no                     VARCHAR2(40) not null,
  mother_name                    VARCHAR2(40),
  phone_number                   VARCHAR2(40),
  person_say                     VARCHAR2(200),
  birth_parity                   VARCHAR2(10),
  delivery_times                 VARCHAR2(10),
  gestational_age                VARCHAR2(10),
  birth_way                      VARCHAR2(40),
  birth_weight                   NUMBER(10),
  birth_defect                   VARCHAR2(40),
  amniotic_fluid                 VARCHAR2(20),
  amniotic_fluid_amount          NUMBER(20),
  placenta                       VARCHAR2(10),
  umbilical_cord                 VARCHAR2(10),
  prenatal_steroid_hormones      VARCHAR2(10),
  apgar_one_minute               VARCHAR2(10),
  apgar_five_minute              VARCHAR2(10),
  apgar_ten_minute               VARCHAR2(10),
  critical_points                VARCHAR2(10),
  in_hos_time                    DATE,
  in_hos_desc                    VARCHAR2(1000),
  mother_blood_type              VARCHAR2(10),
  mother_rh_blood_type           VARCHAR2(10),
  mother_whether_smoke           VARCHAR2(10),
  mother_infection_history       VARCHAR2(200),
  mother_medication_history      VARCHAR2(200),
  mother_allergies_history       VARCHAR2(200),
  asphyxia_resuscitation_history VARCHAR2(200),
  mother_age                     VARCHAR2(10),
  mother_id_card                 VARCHAR2(50),
  family_address                 VARCHAR2(200),
  born_hospital                  VARCHAR2(100)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255;

comment on column MED_PAT_NEWBORNRECORD.patient_no
  is '患者编号';
comment on column MED_PAT_NEWBORNRECORD.mother_name
  is '母亲姓名';
comment on column MED_PAT_NEWBORNRECORD.phone_number
  is '联系电话';
comment on column MED_PAT_NEWBORNRECORD.person_say
  is '主诉';
comment on column MED_PAT_NEWBORNRECORD.birth_parity
  is '出生胎次';
comment on column MED_PAT_NEWBORNRECORD.delivery_times
  is '产次';
comment on column MED_PAT_NEWBORNRECORD.gestational_age
  is '胎龄（周）';
comment on column MED_PAT_NEWBORNRECORD.birth_way
  is '出生方式';
comment on column MED_PAT_NEWBORNRECORD.birth_weight
  is '出生体重';
comment on column MED_PAT_NEWBORNRECORD.birth_defect
  is '出生缺陷';
comment on column MED_PAT_NEWBORNRECORD.amniotic_fluid
  is '羊水';
comment on column MED_PAT_NEWBORNRECORD.amniotic_fluid_amount
  is '羊水量';
comment on column MED_PAT_NEWBORNRECORD.placenta
  is '胎盘';
comment on column MED_PAT_NEWBORNRECORD.umbilical_cord
  is '脐带';
comment on column MED_PAT_NEWBORNRECORD.prenatal_steroid_hormones
  is '产前类固醇激素';
comment on column MED_PAT_NEWBORNRECORD.apgar_one_minute
  is 'Apgar评分1分钟';
comment on column MED_PAT_NEWBORNRECORD.apgar_five_minute
  is 'Apgar评分5分钟';
comment on column MED_PAT_NEWBORNRECORD.apgar_ten_minute
  is 'Apgar评分10分钟';
comment on column MED_PAT_NEWBORNRECORD.critical_points
  is '危重评分';
comment on column MED_PAT_NEWBORNRECORD.in_hos_time
  is '入院时间';
comment on column MED_PAT_NEWBORNRECORD.in_hos_desc
  is '入院诊断';
comment on column MED_PAT_NEWBORNRECORD.mother_blood_type
  is '母亲血型';
comment on column MED_PAT_NEWBORNRECORD.mother_rh_blood_type
  is '母亲rh血型';
comment on column MED_PAT_NEWBORNRECORD.mother_whether_smoke
  is '是否吸烟';
comment on column MED_PAT_NEWBORNRECORD.mother_infection_history
  is '感染史';
comment on column MED_PAT_NEWBORNRECORD.mother_medication_history
  is '用药史';
comment on column MED_PAT_NEWBORNRECORD.mother_allergies_history
  is '过敏史';
comment on column MED_PAT_NEWBORNRECORD.asphyxia_resuscitation_history
  is '窒息复苏史';

alter table MED_PAT_NEWBORNRECORD
  add constraint PK_NEWBORNRECORD primary key (PATIENT_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255;
  
grant select, insert, update, delete on MED_PAT_NEWBORNRECORD to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_OPERATION
prompt ===================================
prompt
create table MED_PAT_OPERATION
(
  patient_no            VARCHAR2(40),
  apply_no              VARCHAR2(40) not null,
  scheduled_date_time   DATE,
  anesthesia_method     VARCHAR2(80),
  surgeon               VARCHAR2(20),
  anesthesia_doctor     VARCHAR2(20),
  diag_before_operation VARCHAR2(200),
  diag_after_operation  VARCHAR2(200),
  operation_scale       VARCHAR2(10),
  in_date_time          DATE,
  out_date_time         DATE,
  anes_start_time       DATE,
  anes_end_time         DATE,
  start_date_time       DATE,
  end_date_time         DATE,
  in_pacu_date_time     DATE,
  out_pacu_date_time    DATE,
  blood_lossed          NUMBER(6),
  blood_transfered      NUMBER(6),
  operation_name        VARCHAR2(200),
  operation_part        VARCHAR2(40)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PAT_OPERATION
  add constraint PK_OPERATION primary key (APPLY_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PAT_OPERATION to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_ORDERS
prompt ===================================
prompt
create table MED_PAT_ORDERS
(
  patient_no           VARCHAR2(40) not null,
  order_no             VARCHAR2(40) not null,
  order_sub_no         VARCHAR2(40) not null,
  clinic_no            VARCHAR2(40),
  inp_no               VARCHAR2(40),
  repeat_indicator     VARCHAR2(40),
  order_class          VARCHAR2(40),
  order_text           VARCHAR2(40),
  order_code           VARCHAR2(40),
  dosage               VARCHAR2(40),
  dosage_units         VARCHAR2(40),
  administration       VARCHAR2(40),
  start_date_time      DATE,
  stop_date_time       DATE,
  duration             VARCHAR2(40),
  duration_units       VARCHAR2(40),
  frequency            VARCHAR2(40),
  ordering_dept        VARCHAR2(40),
  doctor               VARCHAR2(40),
  stop_doctor          VARCHAR2(40),
  nurse                VARCHAR2(40),
  stop_nurse           VARCHAR2(40),
  enter_date_time      DATE,
  stop_order_date_time DATE,
  order_status         VARCHAR2(40),
  current_presc_no     VARCHAR2(40)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PAT_ORDERS
  add constraint PK_MED_PAT_ORDERS primary key (PATIENT_NO, ORDER_NO, ORDER_SUB_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PAT_ORDERS to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_RELATION_INFO
prompt ===================================
prompt
create table MED_PAT_RELATION_INFO
(
  patient_no     VARCHAR2(40) not null,
  phep_id        VARCHAR2(60),
  mission_id     VARCHAR2(40),
  regist_id      VARCHAR2(40),
  emergency_id   VARCHAR2(40),
  clinic_no      VARCHAR2(40),
  inp_no         VARCHAR2(40),
  mac_no         VARCHAR2(40),
  other_no       VARCHAR2(40),
  patient_source VARCHAR2(10),
  create_time    DATE not null,
  update_time    DATE,
  memo           VARCHAR2(100),
  res1           VARCHAR2(100),
  res2           VARCHAR2(100)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PAT_RELATION_INFO
  add constraint PK_MED_PAT_RELATION_INFO primary key (PATIENT_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PAT_RELATION_INFO to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_SCORE
prompt ===================================
prompt
create table MED_PAT_SCORE
(
  patient_no   VARCHAR2(40) not null,
  score_type   VARCHAR2(40) not null,
  score_souce  VARCHAR2(40) not null,
  score_item   VARCHAR2(40) not null,
  score_result VARCHAR2(40),
  score_date   DATE
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PAT_SCORE
  add constraint PK_MED_PAT_SCORE primary key (PATIENT_NO, SCORE_TYPE, SCORE_SOUCE, SCORE_ITEM)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PAT_SCORE to ROLE_DOCARE;

prompt
prompt Creating table MED_PAT_TIMENODES
prompt ===================================
prompt
create table MED_PAT_TIMENODES
(
  patient_no VARCHAR2(40) not null,
  tag_id     VARCHAR2(40) not null,
  item_name  VARCHAR2(60) not null,
  item_value DATE,
  bind_time  DATE
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
 
alter table MED_PAT_TIMENODES
  add constraint PK_MED_PAT_TIMENODES primary key (PATIENT_NO, TAG_ID, ITEM_NAME)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PAT_TIMENODES to ROLE_DOCARE;

prompt
prompt Creating table MED_PATIENT_DETIALS_INFO
prompt ===================================
prompt
create table MED_PATIENT_DETIALS_INFO
(
  patient_no        VARCHAR2(40) not null,
  regist_time       DATE,
  dept_code         VARCHAR2(40),
  arrvied_dept_time DATE,
  triage_time       DATE,
  doctor            VARCHAR2(40),
  admisstion_time   DATE,
  diag_doctor       VARCHAR2(40),
  diag_time         DATE,
  diag_desc         VARCHAR2(100),
  complaint         VARCHAR2(100),
  isemg             VARCHAR2(10),
  emg_bed_no        VARCHAR2(40),
  transfer_time     DATE,
  is_greenchannel   NUMBER(2) not null,
  temp              VARCHAR2(10),
  br                VARCHAR2(10),
  plus              VARCHAR2(10),
  hr                VARCHAR2(10),
  bp                VARCHAR2(10),
  spo2              VARCHAR2(10),
  in_hos_time       DATE,
  in_dept_code      VARCHAR2(40),
  in_ward_code      VARCHAR2(40),
  in_hos_diagnosis  VARCHAR2(200),
  in_dept_time      DATE,
  bed_no            VARCHAR2(40),
  toast_flag        VARCHAR2(40),
  out_dept_time     DATE,
  out_hos_diagnosis VARCHAR2(200),
  out_hos_time      DATE,
  hos_day           VARCHAR2(40),
  total_cost        VARCHAR2(40),
  drug_cost         VARCHAR2(40),
  exam_cost         VARCHAR2(40),
  out_way           VARCHAR2(40)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PATIENT_DETIALS_INFO
  add constraint PK_MED_PATIENT_DETIALS_INFO primary key (PATIENT_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PATIENT_DETIALS_INFO to ROLE_DOCARE;

prompt
prompt Creating table MED_PATIENT_REGIST_INFO
prompt ===================================
prompt
create table MED_PATIENT_REGIST_INFO
(
  patient_no    VARCHAR2(40) not null,
  name          VARCHAR2(40),
  date_of_birth DATE,
  age           VARCHAR2(10),
  sex           VARCHAR2(10),
  id_no         VARCHAR2(20),
  pat_job       VARCHAR2(40),
  citizen_card  VARCHAR2(40),
  mail_address  VARCHAR2(100),
  phone_numer   VARCHAR2(40),
  come_type     VARCHAR2(40),
  res1          VARCHAR2(100),
  res2          VARCHAR2(100)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PATIENT_REGIST_INFO
  add constraint PK_MED_PATIENT_REGIST_INFO primary key (PATIENT_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PATIENT_REGIST_INFO to ROLE_DOCARE;

prompt
prompt Creating table MED_PHEP_PATIENT_INFO
prompt ===================================
prompt
create table MED_PHEP_PATIENT_INFO
(
  patient_no           VARCHAR2(40) not null,
  name                 VARCHAR2(40),
  date_of_birth        DATE,
  sex                  VARCHAR2(10),
  id_no                VARCHAR2(20),
  citizen_card         VARCHAR2(40),
  mail_address         VARCHAR2(100),
  phone_numer          VARCHAR2(40),
  attack_time          DATE,
  attack_address       VARCHAR2(100),
  call_time            DATE,
  accept_time          DATE,
  dispatch_type        VARCHAR2(40),
  dispatch_vehicletime DATE,
  arrived_time         DATE,
  touch_pat_time       DATE,
  left_time            DATE,
  arrived_hos_time     DATE,
  fmc_doctor           VARCHAR2(10),
  first_diagnosis      VARCHAR2(100),
  to_hospital          VARCHAR2(40),
  killip_level         VARCHAR2(40),
  complaint            VARCHAR2(400),
  sense                VARCHAR2(40),
  temp                 VARCHAR2(40),
  br                   VARCHAR2(40),
  plus                 VARCHAR2(40),
  hr                   VARCHAR2(40),
  bp                   VARCHAR2(40),
  spo2                 VARCHAR2(40),
  res1                 VARCHAR2(100),
  res2                 VARCHAR2(100),
  symptom              VARCHAR2(40),
  notify_emergency     DATE,
  diagnosis_time       DATE,
  presentillness       VARCHAR2(100),
  allergichistory      VARCHAR2(100),
  pasthistory          VARCHAR2(100),
  conditiongrade       VARCHAR2(20),
  therapeuticmethod    VARCHAR2(1000),
  infectionhistory	   VARCHAR2(200),
  medicationhistory    VARCHAR2(200),
  recoveryhistory      VARCHAR2(200),
  nursename            VARCHAR2(20),
  bloodsugar           VARCHAR2(40),
  specialcheck         VARCHAR2(100)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_PHEP_PATIENT_INFO
  add constraint PK_MED_PHEP_PATIENT_INFO primary key (PATIENT_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_PHEP_PATIENT_INFO to ROLE_DOCARE;

prompt
prompt Creating table MED_EXAM_REPORT
prompt ===================================
prompt
create table MED_EXAM_REPORT
(
  exam_no            VARCHAR2(40) not null,
  patient_no         VARCHAR2(40),
  exam_class         VARCHAR2(100),
  exam_sub_class     VARCHAR2(100),
  clin_diag          VARCHAR2(60),
  exam_mode          VARCHAR2(40),
  device             VARCHAR2(40),
  performed_by       VARCHAR2(40),
  req_date_time      DATE,
  req_dept           VARCHAR2(40),
  req_physician      VARCHAR2(40),
  req_memo           VARCHAR2(100),
  notice             VARCHAR2(40),
  exam_start_date    DATE,
  exam_end_date      DATE,
  report_date_time   DATE,
  exam_para          VARCHAR2(100),
  description        VARCHAR2(500),
  recommendation     VARCHAR2(500),
  technician         VARCHAR2(40),
  reporter           VARCHAR2(40),
  result_status      VARCHAR2(40),
  verified_by        VARCHAR2(40),
  verified_date_time DATE
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_EXAM_REPORT
  add constraint PK_MED_EXAM_REPORT primary key (EXAM_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_EXAM_REPORT to ROLE_DOCARE;

prompt
prompt Creating table MED_LAB_MASTER
prompt ===================================
prompt
create table MED_LAB_MASTER
(
  test_no                 VARCHAR2(40) not null,
  priority_indicator      VARCHAR2(40),
  patient_no              VARCHAR2(40),
  name                    VARCHAR2(40),
  test_cause              VARCHAR2(80),
  relevant_clinic_diag    VARCHAR2(100),
  specimen                VARCHAR2(100),
  spcm_received_date_time DATE,
  apply_time              DATE,
  ordering_dept           VARCHAR2(40),
  ordering_provider       VARCHAR2(40),
  performed_by            VARCHAR2(40),
  result_status           VARCHAR2(40),
  results_rpt_date_time   DATE,
  transcriptionist        VARCHAR2(40),
  verified_by             VARCHAR2(40)
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

alter table MED_LAB_MASTER
  add constraint PK_MED_LAB_MASTER primary key (TEST_NO)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

grant select, insert, update, delete on MED_LAB_MASTER to ROLE_DOCARE;

