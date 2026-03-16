prompt Dropping MED_EXAM_REPORT...
drop table MED_EXAM_REPORT cascade constraints;
prompt Dropping MED_LAB_MASTER...
drop table MED_LAB_MASTER cascade constraints;
prompt Dropping MED_LAB_RESULT...
drop table MED_LAB_RESULT cascade constraints;
prompt Dropping MED_PATIENT_DETIALS_INFO...
drop table MED_PATIENT_DETIALS_INFO cascade constraints;
prompt Dropping MED_PATIENT_REGIST_INFO...
drop table MED_PATIENT_REGIST_INFO cascade constraints;
prompt Dropping MED_PAT_BLOOD_GAS_DETAIL...
drop table MED_PAT_BLOOD_GAS_DETAIL cascade constraints;
prompt Dropping MED_PAT_MATERECORD...
drop table MED_PAT_MATERECORD cascade constraints;
prompt Dropping MED_PAT_NEWBORNRECORD...
drop table MED_PAT_NEWBORNRECORD cascade constraints;
prompt Dropping MED_PAT_OPERATION...
drop table MED_PAT_OPERATION cascade constraints;
prompt Dropping MED_PAT_ORDERS...
drop table MED_PAT_ORDERS cascade constraints;
prompt Dropping MED_PAT_RELATION_INFO...
drop table MED_PAT_RELATION_INFO cascade constraints;
prompt Dropping MED_PAT_SCORE...
drop table MED_PAT_SCORE cascade constraints;
prompt Dropping MED_PAT_TIMENODES...
drop table MED_PAT_TIMENODES cascade constraints;
prompt Dropping MED_PHEP_PATIENT_INFO...
drop table MED_PHEP_PATIENT_INFO cascade constraints;
prompt Creating MED_EXAM_REPORT...
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
;
alter table MED_EXAM_REPORT
  add constraint PK_MED_EXAM_REPORT primary key (EXAM_NO);
grant select, insert, update, delete on MED_EXAM_REPORT to ROLE_DOCARE;

prompt Creating MED_LAB_MASTER...
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
;
alter table MED_LAB_MASTER
  add constraint PK_MED_LAB_MASTER primary key (TEST_NO);
grant select, insert, update, delete on MED_LAB_MASTER to ROLE_DOCARE;

prompt Creating MED_LAB_RESULT...
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
  reference_result   VARCHAR2(40),
  id                 VARCHAR2(40)
)
;
alter table MED_LAB_RESULT
  add constraint PK_MED_LAB_RESULT primary key (TEST_NO, ITEM_NO);
grant select, insert, update, delete on MED_LAB_RESULT to ROLE_DOCARE;

prompt Creating MED_PATIENT_DETIALS_INFO...
create table MED_PATIENT_DETIALS_INFO
(
  patient_no        VARCHAR2(40) not null,
  regist_time       DATE,
  dept_code         VARCHAR2(40),
  arrived_dept_time DATE,
  triage_time       DATE,
  doctor            VARCHAR2(40),
  admisstion_time   DATE,
  diag_doctor       VARCHAR2(40),
  diag_time         DATE,
  diag_desc         VARCHAR2(100),
  complaint         VARCHAR2(100),
  is_emg            VARCHAR2(10),
  emg_bed_no        VARCHAR2(40),
  transfer_time     DATE,
  is_green_channel  NUMBER(2) not null,
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
;
alter table MED_PATIENT_DETIALS_INFO
  add constraint PK_MED_PATIENT_DETIALS_INFO primary key (PATIENT_NO);
grant select, insert, update, delete on MED_PATIENT_DETIALS_INFO to ROLE_DOCARE;

prompt Creating MED_PATIENT_REGIST_INFO...
create table MED_PATIENT_REGIST_INFO
(
  patient_no            VARCHAR2(40) not null,
  name                  VARCHAR2(40),
  date_of_birth         DATE,
  age                   VARCHAR2(10),
  sex                   VARCHAR2(10),
  id_no                 VARCHAR2(20),
  pat_job               VARCHAR2(40),
  citizen_card          VARCHAR2(40),
  mail_address          VARCHAR2(100),
  phone_number          VARCHAR2(40),
  come_type             VARCHAR2(40),
  res1                  VARCHAR2(100),
  res2                  VARCHAR2(100),
  org_code              VARCHAR2(40),
  diag_lev              VARCHAR2(20),
  arrive_type_extension VARCHAR2(10)
)
;
alter table MED_PATIENT_REGIST_INFO
  add constraint PK_MED_PATIENT_REGIST_INFO primary key (PATIENT_NO);
grant select, insert, update, delete on MED_PATIENT_REGIST_INFO to ROLE_DOCARE;

prompt Creating MED_PAT_BLOOD_GAS_DETAIL...
create table MED_PAT_BLOOD_GAS_DETAIL
(
  id          VARCHAR2(40) not null,
  patient_no  VARCHAR2(60),
  blood_time  DATE,
  report_time DATE,
  test_id     VARCHAR2(100),
  test_name   VARCHAR2(100),
  test_value  VARCHAR2(100),
  unit        VARCHAR2(100),
  conclusion  VARCHAR2(100)
)
;
alter table MED_PAT_BLOOD_GAS_DETAIL
  add constraint PK_MED_PAT_BLOOD_GAS_DETAIL primary key (ID);

prompt Creating MED_PAT_MATERECORD...
create table MED_PAT_MATERECORD
(
  patient_no        VARCHAR2(40) not null,
  in_dept           VARCHAR2(40),
  bed_no            VARCHAR2(40),
  in_hos_time       DATE,
  in_complaint      VARCHAR2(200),
  preganet_week     VARCHAR2(50),
  preganet_times    VARCHAR2(50),
  product_times     VARCHAR2(50),
  in_sense          VARCHAR2(20),
  in_t              VARCHAR2(20),
  in_p              VARCHAR2(20),
  in_r              VARCHAR2(20),
  in_bp             VARCHAR2(20),
  in_spo2           VARCHAR2(20),
  in_glu            VARCHAR2(20),
  fetel_heart       VARCHAR2(20),
  uter_height       VARCHAR2(20),
  abd_index         VARCHAR2(20),
  past_history      VARCHAR2(1000),
  family_history    VARCHAR2(1000),
  menarche_age      VARCHAR2(10),
  menstrual_day     VARCHAR2(10),
  menstrual_cycle   VARCHAR2(10),
  menstrual_volume  VARCHAR2(10),
  dysmenorrhea      VARCHAR2(10),
  initial_diagnosis VARCHAR2(1000),
  initial_doctor    VARCHAR2(20),
  inhos_diagnosis   VARCHAR2(1000),
  inhos_doctor      VARCHAR2(20),
  high_risk         VARCHAR2(2000),
  special_treatment VARCHAR2(200),
  producted_bleed   VARCHAR2(200),
  indication        VARCHAR2(200)
)
;
comment on column MED_PAT_MATERECORD.patient_no
  is '患者编号';
comment on column MED_PAT_MATERECORD.in_dept
  is '入院科室';
comment on column MED_PAT_MATERECORD.bed_no
  is '床位';
comment on column MED_PAT_MATERECORD.in_hos_time
  is '入院时间';
comment on column MED_PAT_MATERECORD.in_complaint
  is '主诉';
comment on column MED_PAT_MATERECORD.preganet_week
  is '孕周';
comment on column MED_PAT_MATERECORD.preganet_times
  is '孕次';
comment on column MED_PAT_MATERECORD.product_times
  is '产次';
comment on column MED_PAT_MATERECORD.in_sense
  is '意识';
comment on column MED_PAT_MATERECORD.in_t
  is '体温';
comment on column MED_PAT_MATERECORD.in_p
  is '脉搏';
comment on column MED_PAT_MATERECORD.in_r
  is '呼吸';
comment on column MED_PAT_MATERECORD.in_bp
  is '血压';
comment on column MED_PAT_MATERECORD.in_spo2
  is '血氧';
comment on column MED_PAT_MATERECORD.in_glu
  is '血糖';
comment on column MED_PAT_MATERECORD.fetel_heart
  is '胎心';
comment on column MED_PAT_MATERECORD.uter_height
  is '宫高';
comment on column MED_PAT_MATERECORD.abd_index
  is '腹围';
comment on column MED_PAT_MATERECORD.past_history
  is '既往史';
comment on column MED_PAT_MATERECORD.family_history
  is '家族史';
comment on column MED_PAT_MATERECORD.menarche_age
  is '初潮年龄';
comment on column MED_PAT_MATERECORD.menstrual_day
  is '经期天数';
comment on column MED_PAT_MATERECORD.menstrual_cycle
  is '经期周期';
comment on column MED_PAT_MATERECORD.menstrual_volume
  is '月经量';
comment on column MED_PAT_MATERECORD.dysmenorrhea
  is '痛经';
comment on column MED_PAT_MATERECORD.initial_diagnosis
  is '初步诊断';
comment on column MED_PAT_MATERECORD.initial_doctor
  is '初步诊断医生';
comment on column MED_PAT_MATERECORD.inhos_diagnosis
  is '入院诊断';
comment on column MED_PAT_MATERECORD.inhos_doctor
  is '入院诊断医生';
comment on column MED_PAT_MATERECORD.high_risk
  is '高危因素';
comment on column MED_PAT_MATERECORD.special_treatment
  is '特殊治疗措施';
alter table MED_PAT_MATERECORD
  add constraint PK_MATERERODE primary key (PATIENT_NO);
grant select, insert, update, delete on MED_PAT_MATERECORD to ROLE_DOCARE;

prompt Creating MED_PAT_NEWBORNRECORD...
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
;
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
  add constraint PK_NEWBORNRECORD primary key (PATIENT_NO);
grant select, insert, update, delete on MED_PAT_NEWBORNRECORD to ROLE_DOCARE;

prompt Creating MED_PAT_OPERATION...
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
  operation_part        VARCHAR2(40),
  oper_nurse            VARCHAR2(40),
  supply_nurse          VARCHAR2(40)
)
;
alter table MED_PAT_OPERATION
  add constraint PK_OPERATION primary key (APPLY_NO);
grant select, insert, update, delete on MED_PAT_OPERATION to ROLE_DOCARE;

prompt Creating MED_PAT_ORDERS...
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
  current_presc_no     VARCHAR2(40),
  id                   VARCHAR2(60)
)
;
alter table MED_PAT_ORDERS
  add constraint PK_MED_PAT_ORDERS primary key (PATIENT_NO, ORDER_NO, ORDER_SUB_NO);
grant select, insert, update, delete on MED_PAT_ORDERS to ROLE_DOCARE;

prompt Creating MED_PAT_RELATION_INFO...
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
  created_time   DATE not null,
  update_time    DATE,
  memo           VARCHAR2(100),
  res1           VARCHAR2(100),
  res2           VARCHAR2(100),
  org_code       VARCHAR2(60),
  inp_regist_id  VARCHAR2(60),
  mother_id_card VARCHAR2(60)
)
;
alter table MED_PAT_RELATION_INFO
  add constraint PK_MED_PAT_RELATION_INFO primary key (PATIENT_NO);
grant select, insert, update, delete on MED_PAT_RELATION_INFO to ROLE_DOCARE;

prompt Creating MED_PAT_SCORE...
create table MED_PAT_SCORE
(
  patient_no   VARCHAR2(40) not null,
  score_type   VARCHAR2(40) not null,
  score_souce  VARCHAR2(40) not null,
  score_item   VARCHAR2(40) not null,
  score_result VARCHAR2(40),
  score_date   DATE,
  id           VARCHAR2(40)
)
;
alter table MED_PAT_SCORE
  add constraint PK_MED_PAT_SCORE primary key (PATIENT_NO, SCORE_TYPE, SCORE_SOUCE, SCORE_ITEM);
grant select, insert, update, delete on MED_PAT_SCORE to ROLE_DOCARE;

prompt Creating MED_PAT_TIMENODES...
create table MED_PAT_TIMENODES
(
  patient_no VARCHAR2(40) not null,
  tag_id     VARCHAR2(40) not null,
  item_name  VARCHAR2(60) not null,
  item_value DATE,
  bind_time  DATE,
  id         VARCHAR2(60)
)
;
alter table MED_PAT_TIMENODES
  add constraint PK_MED_PAT_TIMENODES primary key (PATIENT_NO, TAG_ID, ITEM_NAME);
grant select, insert, update, delete on MED_PAT_TIMENODES to ROLE_DOCARE;

prompt Creating MED_PHEP_PATIENT_INFO...
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
  infectionhistory     VARCHAR2(200),
  medicationhistory    VARCHAR2(200),
  recoveryhistory      VARCHAR2(200),
  nursename            VARCHAR2(20),
  bloodsugar           VARCHAR2(40),
  specialcheck         VARCHAR2(100)
)
;
alter table MED_PHEP_PATIENT_INFO
  add constraint PK_MED_PHEP_PATIENT_INFO primary key (PATIENT_NO);
grant select, insert, update, delete on MED_PHEP_PATIENT_INFO to ROLE_DOCARE;

prompt Disabling triggers for MED_EXAM_REPORT...
alter table MED_EXAM_REPORT disable all triggers;
prompt Disabling triggers for MED_LAB_MASTER...
alter table MED_LAB_MASTER disable all triggers;
prompt Disabling triggers for MED_LAB_RESULT...
alter table MED_LAB_RESULT disable all triggers;
prompt Disabling triggers for MED_PATIENT_DETIALS_INFO...
alter table MED_PATIENT_DETIALS_INFO disable all triggers;
prompt Disabling triggers for MED_PATIENT_REGIST_INFO...
alter table MED_PATIENT_REGIST_INFO disable all triggers;
prompt Disabling triggers for MED_PAT_BLOOD_GAS_DETAIL...
alter table MED_PAT_BLOOD_GAS_DETAIL disable all triggers;
prompt Disabling triggers for MED_PAT_MATERECORD...
alter table MED_PAT_MATERECORD disable all triggers;
prompt Disabling triggers for MED_PAT_NEWBORNRECORD...
alter table MED_PAT_NEWBORNRECORD disable all triggers;
prompt Disabling triggers for MED_PAT_OPERATION...
alter table MED_PAT_OPERATION disable all triggers;
prompt Disabling triggers for MED_PAT_ORDERS...
alter table MED_PAT_ORDERS disable all triggers;
prompt Disabling triggers for MED_PAT_RELATION_INFO...
alter table MED_PAT_RELATION_INFO disable all triggers;
prompt Disabling triggers for MED_PAT_SCORE...
alter table MED_PAT_SCORE disable all triggers;
prompt Disabling triggers for MED_PAT_TIMENODES...
alter table MED_PAT_TIMENODES disable all triggers;
prompt Disabling triggers for MED_PHEP_PATIENT_INFO...
alter table MED_PHEP_PATIENT_INFO disable all triggers;
prompt Loading MED_EXAM_REPORT...
insert into MED_EXAM_REPORT (exam_no, patient_no, exam_class, exam_sub_class, clin_diag, exam_mode, device, performed_by, req_date_time, req_dept, req_physician, req_memo, notice, exam_start_date, exam_end_date, report_date_time, exam_para, description, recommendation, technician, reporter, result_status, verified_by, verified_date_time)
values ('202010240127', '01201216001', null, null, null, null, null, null, to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, '胸部CT扫描', to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), '胸部CT扫描', '胸廓双侧对称，两肺纹理增多，左上叶、两下叶模糊影。双侧胸腔可见积液。纵隔内未见明显肿大淋巴结。心脏不大。', '胸部CT扫描', null, null, null, null, to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'));
insert into MED_EXAM_REPORT (exam_no, patient_no, exam_class, exam_sub_class, clin_diag, exam_mode, device, performed_by, req_date_time, req_dept, req_physician, req_memo, notice, exam_start_date, exam_end_date, report_date_time, exam_para, description, recommendation, technician, reporter, result_status, verified_by, verified_date_time)
values ('20201027161146WOZCTH', '01201216001', null, null, null, null, null, null, to_date('24-10-2020 18:06:40', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, '头颅CT扫描', to_date('24-10-2020 18:06:40', 'dd-mm-yyyy hh24:mi:ss'), to_date('24-10-2020 18:06:40', 'dd-mm-yyyy hh24:mi:ss'), to_date('24-10-2020 18:06:40', 'dd-mm-yyyy hh24:mi:ss'), '头颅CT扫描', '双侧侧脑室旁可见斑点低密度影。脑室系统扩大。脑沟裂增宽。脑中线结构居中。', '头颅CT扫描', null, null, null, null, to_date('24-10-2020 18:06:40', 'dd-mm-yyyy hh24:mi:ss'));
insert into MED_EXAM_REPORT (exam_no, patient_no, exam_class, exam_sub_class, clin_diag, exam_mode, device, performed_by, req_date_time, req_dept, req_physician, req_memo, notice, exam_start_date, exam_end_date, report_date_time, exam_para, description, recommendation, technician, reporter, result_status, verified_by, verified_date_time)
values ('2020102716114677ZNAW', '01201216001', null, null, null, null, null, null, to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, '胸部CT扫描', to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'), '胸部CT扫描', '胸廓双侧对称，两肺纹理增多，左上叶、两下叶模糊影。双侧胸腔可见积液。纵隔内未见明显肿大淋巴结。心脏不大。', '胸部CT扫描', null, null, null, null, to_date('24-10-2020 18:07:40', 'dd-mm-yyyy hh24:mi:ss'));
insert into MED_EXAM_REPORT (exam_no, patient_no, exam_class, exam_sub_class, clin_diag, exam_mode, device, performed_by, req_date_time, req_dept, req_physician, req_memo, notice, exam_start_date, exam_end_date, report_date_time, exam_para, description, recommendation, technician, reporter, result_status, verified_by, verified_date_time)
values ('18259426                                ', '01201216002', '01201216002                             ', '01201216002                             ', '01201216002                             ', '01201216002                             ', '01201216002                             ', '1                                       ', to_date('08-12-2020', 'dd-mm-yyyy'), '1                                       ', '1                                       ', '1                                       ', '2                                       ', to_date('18-12-2202', 'dd-mm-yyyy'), to_date('18-12-2202', 'dd-mm-yyyy'), to_date('18-12-2202', 'dd-mm-yyyy'), '3                                       ', '3                                       ', '3                                       ', '3                                       ', '3                                       ', null, '3                                       ', to_date('18-12-2202', 'dd-mm-yyyy'));
commit;
prompt 4 records loaded
prompt Loading MED_LAB_MASTER...
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14457101_RMLIS', null, '01201216003', '袁德昌', '肝功能,,肾功能', null, null, to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14456500_RMLIS', null, '01201216003', '袁德昌', '输血前相容性检查', null, null, to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '乌宇波', '1', to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), null, '乌宇波');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455978_RMLIS', null, '01201216003', '袁德昌', '心肌标志物', null, null, to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '陶慧聪', '1', to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), null, '陶慧聪');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455981_RMLIS', null, '01201216003', '袁德昌', 'DIC', null, null, to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455904_RMLIS', null, '01201216003', '袁德昌', '电解质,,肝功能,,肾功能', null, null, to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455782_RMLIS', null, '01201216003', '袁德昌', '新冠抗体筛查', null, null, to_date('03-11-2020 15:55:34', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 15:55:34', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 15:55:34', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455902_RMLIS', null, '01201216003', '袁德昌', '心肌酶谱', null, null, to_date('03-11-2020 15:55:32', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 15:55:32', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 15:55:32', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455899_RMLIS', null, '01201216003', '袁德昌', '血糖', null, null, to_date('03-11-2020 15:55:27', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 15:55:27', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 15:55:27', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14455793_RMLIS', null, '01201216003', '袁德昌', '血常规CRP网织红SAA', null, null, to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '6923', 'ESB8117', '许志宇', '1', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), null, '许志宇');
insert into MED_LAB_MASTER (test_no, priority_indicator, patient_no, name, test_cause, relevant_clinic_diag, specimen, spcm_received_date_time, apply_time, ordering_dept, ordering_provider, performed_by, result_status, results_rpt_date_time, transcriptionist, verified_by)
values ('14016456_RMLIS', '1', '01201216002', '柑', '2', '24', '24', to_date('18-12-2020', 'dd-mm-yyyy'), to_date('18-12-2020', 'dd-mm-yyyy'), '23', '2', '24', '2', to_date('18-12-2020', 'dd-mm-yyyy'), '24', '25');
commit;
prompt 10 records loaded
prompt Loading MED_LAB_RESULT...
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14457101_RMLIS', '14457101_RMLIS_1', 'Liquid腺苷脱氨酶', 'ADA', 'M', '6', 'U/L', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '4.00-24.00', '611df1d0-69b3-400c-ab1e-ac9501065729');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14457101_RMLIS', '14457101_RMLIS_2', '血清胱抑素C', 'CysC', 'M', '0.84', 'mg/L', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '0.59-1.03', '9df3325d-c522-4908-9883-ac9501065729');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14457101_RMLIS', '14457101_RMLIS_3', 'α-L-岩藻糖苷酶', 'AFU', 'M', '36', 'U/L', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '0.00-40.00', 'ed72081e-9386-40e0-bf22-ac9501065729');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14457101_RMLIS', '14457101_RMLIS_4', '亮氨酸氨基转肽酶', 'LAP', 'M', '33.0', 'U/L', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '12.00-37.00', '0e8e237d-0e8c-4433-8413-ac9501065729');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14457101_RMLIS', '14457101_RMLIS_5', '甘胆酸', 'CG', 'M', '1.39', 'mg/L', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '0.00-2.70', '87a8f126-3c92-46b5-814c-ac9501065729');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14457101_RMLIS', '14457101_RMLIS_6', '视黄醇结合蛋白', 'RBP', 'M', '50.4', 'mg/L', to_date('03-11-2020 19:11:00', 'dd-mm-yyyy hh24:mi:ss'), '25.00-70.00', '282dbdcc-21c1-4ef0-a2e5-ac9501065729');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14456500_RMLIS', '14456500_RMLIS_1', 'ABO血型', 'ABO', 'M', 'B', null, to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.00', 'f0668e9b-d363-4b53-bd21-ac950106572d');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14456500_RMLIS', '14456500_RMLIS_2', 'RH血型', 'RH', 'M', '阳性', null, to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.00', 'd4904e72-ccb0-4c8a-8cfc-ac950106572d');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14456500_RMLIS', '14456500_RMLIS_3', '抗体筛选', 'KTSX', 'N', '阴性', null, to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.00', '59486145-93f6-47a7-98b1-ac950106572d');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14456500_RMLIS', '14456500_RMLIS_4', '直接抗球蛋白', 'ZKSY', 'N', '阴性', null, to_date('03-11-2020 17:50:35', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.00', 'c6387677-a4fd-4703-bb14-ac950106572d');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455978_RMLIS', '14455978_RMLIS_1', '肌钙蛋白 T', 'TNT', 'M', '0.017', 'ng/ml', to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), '0.01-0.02', '802133a8-3ab1-4728-b353-ac950106572e');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455978_RMLIS', '14455978_RMLIS_2', '肌红蛋白', 'MYO', 'H', '132.00', 'ng/ml', to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), '28.00-72.00', '5601a619-468b-4728-9fd3-ac950106572e');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455978_RMLIS', '14455978_RMLIS_3', 'CK-MB mass', 'CK', 'M', '2.11', 'ng/ml', to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), '0.00-4.87', '20714756-17a9-4500-91dd-ac950106572e');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455978_RMLIS', '14455978_RMLIS_4', 'pro BNP', 'BNP', 'M', '42.58', 'pg/mL', to_date('03-11-2020 16:36:10', 'dd-mm-yyyy hh24:mi:ss'), '0.00-100.00', '45b62bd9-566b-4488-971b-ac950106572e');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_1', '凝血酶原时间', 'PT', 'M', '11.8', null, to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '10.30-12.70', '75d61999-1565-45bb-ba83-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_2', '国际标准化比率', 'PT INR', 'M', '0.98', null, to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '0.92-1.15', '068a51af-308a-4402-8da9-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_3', '部分凝血活酶时间', 'APTT', 'M', '24.2', '秒', to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '20.30-32.30', 'dd610077-0862-4ee8-be78-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_4', '凝血酶时间', 'TT', 'M', '17.7', '秒', to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '14.00-21.00', 'ffd3a837-af34-4a19-8d86-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_5', '纤维蛋白原定量', 'FIB', 'M', '2.4', 'g/L', to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '1.80-3.50', '339fa415-8f19-467e-9521-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_6', 'D-二聚体', 'DDI', 'H', '1.35', null, to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.55', 'ea340101-bb2f-46ee-a6ea-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_7', '纤维蛋白原降解产物', 'FDP', 'M', '2.5', 'μg/ml', to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '0.00-5.00', '347f3f4f-4b0b-4c1a-8aef-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455981_RMLIS', '14455981_RMLIS_8', '抗凝血酶Ⅲ', 'AT', 'M', '98.9', '%', to_date('03-11-2020 15:59:44', 'dd-mm-yyyy hh24:mi:ss'), '75.00-125.00', '74001b2b-24a0-4641-9314-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_1', '血清钾（干式法）', 'K', 'M', '4.7', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '3.50-5.10', '3091a4eb-7555-40a4-a62c-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_2', '血清钠（干式法）', 'NA', 'M', '143', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '137.00-145.00', '65cad7da-f366-4e41-aea8-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_3', '血清氯（干式法）', 'CL', 'M', '103', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '98.00-107.00', '2b11acf0-4527-4a0f-8e41-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_4', '二氧化碳结合力（干式法）', 'CO2', 'L', '20.0', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '22.00-30.00', 'a78f734e-1c6d-4cb5-9553-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_5', '血清钙（干式法）', 'CA', 'M', '2.47', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '2.10-2.55', 'ce9bd439-019a-436d-9339-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_6', '血清磷（干式法）', 'PHO', 'M', '1.19', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '0.81-1.45', '3047c16f-7474-49e2-81d6-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_7', '丙氨酸转氨酶（干式法）', 'ALT', 'M', '28', 'U/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '13.00-69.00', 'f21f7912-6fb2-456a-8f50-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_8', '总胆红素（干式法）', 'TBIL', 'M', '7.6', 'μmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '3.00-22.00', '4fb7d8c7-c6dc-4133-aed7-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_9', '天冬氨酸转氨酶（干式法）', 'AST', 'M', '28', 'U/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '15.00-46.00', '91657435-7040-4c8b-94a1-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_10', '结合胆红素（干式法）', 'BC', 'M', '0.0', 'umol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '0.00-5.00', '504e6186-3beb-439c-a6c8-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_11', '非结合胆红素（干式法）', 'BU', 'M', '3.1', 'umol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '0.00-19.00', 'f94d992a-06ea-46ac-8616-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_12', '总蛋白（干式法）', 'TP', 'M', '78', 'g/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '63.00-82.00', '185cd5d4-e18c-4024-9ea7-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_13', '白蛋白（干式法）', 'ALB', 'H', '51', 'g/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '35.00-50.00', '801d304a-d7dc-4672-ac4b-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_14', '碱性磷酸酶（干式法）', 'ALP', 'M', '83', 'U/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '38.00-126.00', '8f34cb50-4741-450f-99e7-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_15', 'γ- 谷氨酰转移酶（干式法）', 'GGT', 'M', '42', 'U/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '12.00-58.00', '857126a6-568a-403c-b680-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_16', '尿素氮（干式法）', 'BUN', 'M', '4.7', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '3.20-7.10', 'de6becf1-35b5-4794-9975-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_17', '尿酸（干式法）', 'UA', 'H', '0.622', 'mmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '0.20-0.50', '345342a4-70ec-4d80-aada-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455904_RMLIS', '14455904_RMLIS_18', '肌酐（干式法）', 'CRE', 'M', '71', 'μmol/L', to_date('03-11-2020 15:57:36', 'dd-mm-yyyy hh24:mi:ss'), '58.00-110.00', '34094ee4-c9d7-4093-9fcc-ac950106572f');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455782_RMLIS', '14455782_RMLIS_1', '新型冠状病毒IgM(胶体金法)', '2019-nCoVIgM', 'N', '阴性(-)', null, to_date('03-11-2020 15:55:34', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.00', '0838288a-f39d-46a5-b6d0-ac9501065730');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455782_RMLIS', '14455782_RMLIS_2', '新型冠状病毒IgG(胶体金法)', '2019-nCoVIgG', 'N', '阴性(-)', null, to_date('03-11-2020 15:55:34', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.00', 'a419cdb4-46bc-4bc0-be58-ac9501065730');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455902_RMLIS', '14455902_RMLIS_1', '天冬氨酸转氨酶（干式法）', 'AST', 'M', '35', 'U/L', to_date('03-11-2020 15:55:32', 'dd-mm-yyyy hh24:mi:ss'), '15.00-46.00', '808ba13f-108d-4b5b-91f8-ac9501065730');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455902_RMLIS', '14455902_RMLIS_2', '乳酸脱氢酶（干式法）', 'LDH', 'H', '289', 'U/L', to_date('03-11-2020 15:55:32', 'dd-mm-yyyy hh24:mi:ss'), '125.00-247.00', '15fb4232-d060-4d20-a546-ac9501065730');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455902_RMLIS', '14455902_RMLIS_3', '肌酸激酶（干式法）', 'CK', 'M', '128', 'U/L', to_date('03-11-2020 15:55:32', 'dd-mm-yyyy hh24:mi:ss'), '55.00-170.00', '2b101664-9a89-4ebb-9253-ac9501065730');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455899_RMLIS', '14455899_RMLIS_1', '血糖（干式法）', 'GLU', 'M', '5.4', 'mmol/L', to_date('03-11-2020 15:55:27', 'dd-mm-yyyy hh24:mi:ss'), '4.10-5.90', 'fcf1cee1-8608-4383-8e70-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_1', '白细胞计数', 'WBC', 'M', '8.18', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '3.50-9.50', '3cc14c19-9dc2-4ebd-a31c-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_2', '中性粒细胞%', 'NEUT%', 'M', '71.9', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '40.00-75.00', '7fd3084d-3dc9-4695-88c8-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_3', '淋巴细胞%', 'LYMPH%', 'M', '20.5', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '20.00-50.00', 'da35f39d-3dba-4ccc-bf19-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_4', '单核细胞%', 'MONO%', 'M', '7.3', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '3.00-10.00', '96e9e500-f009-4cbd-b257-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_5', '嗜酸性粒细胞%', 'EO%', 'L', '0.1', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '0.40-8.00', '29f8907f-f0fc-443c-8103-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_6', '嗜碱性粒细胞%', 'BASO%', 'M', '0.2', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '0.00-1.00', 'f4bccecd-087c-44f8-ae94-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_7', '中性粒细胞绝对值', 'NEUT#', 'M', '5.89', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '1.80-6.30', '0717719a-65db-42f8-8247-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_8', '红细胞计数', 'RBC', 'M', '4.69', 'x10^12/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '4.30-5.80', 'a042339f-f702-42c9-b156-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_9', '血红蛋白', 'HGB', 'M', '139', 'g/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '130.00-175.00', 'b74907e1-5dbc-4667-be41-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_10', '红细胞压积', 'HCT', 'M', '40.2', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '40.00-50.00', '61551273-3fc1-4a29-a674-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_11', '平均红细胞体积', 'MCV', 'M', '85.8', 'fl', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '82.00-100.00', 'be25a343-746c-4cb8-ad0d-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_12', '平均红细胞血红蛋白量', 'MCH', 'M', '29.7', 'pg', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '27.00-34.00', 'cf91bfe3-5d03-470f-a635-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_13', '平均红细胞血红蛋白浓度', 'MCHC', 'M', '346', 'g/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '316.00-354.00', '3b89c9a0-d1be-49e3-9463-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_14', '红细胞分布宽度--变异系数', 'RDW-CV', 'M', '14.1', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '10.00-15.00', 'fec78e46-eb13-473f-817a-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_15', '血小板计数', 'PLT', 'M', '225', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '125.00-350.00', '73de819e-e1bd-41b4-9be6-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_16', '平均血小板体积', 'MPV', 'L', '8.2', 'fl', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '9.40-13.50', '066db34b-df50-4463-97c5-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_17', '大血小板比率', 'P-LCR', 'L', '14.5', '%', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '19.10-47.00', 'a59e41d5-f489-499e-ad42-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_18', '血小板分布宽度', 'PDW', 'M', '15.7', 'fl', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '9.00-18.10', '17998ca3-757c-4717-a15e-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_19', '红细胞分布宽度', 'RDW-SD', 'M', '43.1', 'fl', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '39.00-52.30', '5df33545-9e50-4be3-a154-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_20', '淋巴细胞绝对值', 'LYMPH#', 'M', '1.67', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '1.10-3.20', '6ebbff29-cf7a-45cb-839b-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_21', '单核细胞绝对值', 'MONO#', 'M', '0.59', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '0.10-0.60', '5b7e1572-cf7e-4467-8197-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_22', '嗜碱性粒细胞绝对值', 'BASO#', 'M', '0.02', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '0.00-0.06', 'a432215b-1faa-4241-993c-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_23', '嗜酸性粒细胞绝对值', 'EO#', 'L', '0.01', 'x10^9/L', to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '0.02-0.52', '453349cb-7f5f-4c4a-b33a-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_24', '中性淋巴比值', 'NLR', 'H', '3.5', null, to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '0.90-3.10', '9f93c351-3e57-4371-a7ae-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14455793_RMLIS', '14455793_RMLIS_25', '血小板淋巴比值', 'PLR', 'M', '134.7', null, to_date('03-11-2020 15:43:58', 'dd-mm-yyyy hh24:mi:ss'), '63.00-176.20', '18fa1d04-8742-46b7-8fec-ac9501065731');
insert into MED_LAB_RESULT (test_no, item_no, report_item_name, report_item_code, abnormal_indicator, result, units, result_date_time, reference_result, id)
values ('14016456_RMLIS', '14016456_RMLIS_1', '芙朴感冒胶囊', ' 24', '2', '24', '24', to_date('18-12-2020', 'dd-mm-yyyy'), '23', '7b12c508-4c1b-4906-9bd9-ac9501151971');
commit;
prompt 72 records loaded
prompt Loading MED_PATIENT_DETIALS_INFO...
insert into MED_PATIENT_DETIALS_INFO (patient_no, regist_time, dept_code, arrived_dept_time, triage_time, doctor, admisstion_time, diag_doctor, diag_time, diag_desc, complaint, is_emg, emg_bed_no, transfer_time, is_green_channel, temp, br, plus, hr, bp, spo2, in_hos_time, in_dept_code, in_ward_code, in_hos_diagnosis, in_dept_time, bed_no, toast_flag, out_dept_time, out_hos_diagnosis, out_hos_time, hos_day, total_cost, drug_cost, exam_cost, out_way)
values ('01201216001', to_date('01-07-2020 16:21:15', 'dd-mm-yyyy hh24:mi:ss'), null, null, to_date('01-07-2020 16:21:15', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, null, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
commit;
prompt 1 records loaded
prompt Loading MED_PATIENT_REGIST_INFO...
insert into MED_PATIENT_REGIST_INFO (patient_no, name, date_of_birth, age, sex, id_no, pat_job, citizen_card, mail_address, phone_number, come_type, res1, res2, org_code, diag_lev, arrive_type_extension)
values ('01201216002', '蔡晨旭', to_date('03-12-2007', 'dd-mm-yyyy'), null, '1', '340221200712032375', '00', '135401012148064', '江苏省泗洪县魏营镇前营居民委员会八组45号', '1233', '3', null, null, '123206004675412220', null, null);
insert into MED_PATIENT_REGIST_INFO (patient_no, name, date_of_birth, age, sex, id_no, pat_job, citizen_card, mail_address, phone_number, come_type, res1, res2, org_code, diag_lev, arrive_type_extension)
values ('01201216001', '郑雨莲', to_date('23-06-1956', 'dd-mm-yyyy'), null, '女', '330802195606232427', null, null, null, null, null, null, null, '123206004675412220', null, null);
commit;
prompt 2 records loaded
prompt Loading MED_PAT_BLOOD_GAS_DETAIL...
prompt Table is empty
prompt Loading MED_PAT_MATERECORD...
prompt Table is empty
prompt Loading MED_PAT_NEWBORNRECORD...
prompt Table is empty
prompt Loading MED_PAT_OPERATION...
prompt Table is empty
prompt Loading MED_PAT_ORDERS...
prompt Table is empty
prompt Loading MED_PAT_RELATION_INFO...
insert into MED_PAT_RELATION_INFO (patient_no, phep_id, mission_id, regist_id, emergency_id, clinic_no, inp_no, mac_no, other_no, patient_source, created_time, update_time, memo, res1, res2, org_code, inp_regist_id, mother_id_card)
values ('01201216003', null, null, '18259426', '15034308', '18183859', null, null, null, '1', to_date('16-12-2020 18:23:46', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, null, null, null, null);
insert into MED_PAT_RELATION_INFO (patient_no, phep_id, mission_id, regist_id, emergency_id, clinic_no, inp_no, mac_no, other_no, patient_source, created_time, update_time, memo, res1, res2, org_code, inp_regist_id, mother_id_card)
values ('01201216002', null, null, '18259426', null, '18183859', null, null, null, '2', to_date('16-12-2020 18:23:46', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, null, null, null, null);
insert into MED_PAT_RELATION_INFO (patient_no, phep_id, mission_id, regist_id, emergency_id, clinic_no, inp_no, mac_no, other_no, patient_source, created_time, update_time, memo, res1, res2, org_code, inp_regist_id, mother_id_card)
values ('01201216001', null, null, null, '202010240127', null, null, null, null, '1', to_date('16-12-2020 13:57:38', 'dd-mm-yyyy hh24:mi:ss'), null, null, null, null, '123206004675412220', null, null);
commit;
prompt 3 records loaded
prompt Loading MED_PAT_SCORE...
prompt Table is empty
prompt Loading MED_PAT_TIMENODES...
prompt Table is empty
prompt Loading MED_PHEP_PATIENT_INFO...
prompt Table is empty
prompt Enabling triggers for MED_EXAM_REPORT...
alter table MED_EXAM_REPORT enable all triggers;
prompt Enabling triggers for MED_LAB_MASTER...
alter table MED_LAB_MASTER enable all triggers;
prompt Enabling triggers for MED_LAB_RESULT...
alter table MED_LAB_RESULT enable all triggers;
prompt Enabling triggers for MED_PATIENT_DETIALS_INFO...
alter table MED_PATIENT_DETIALS_INFO enable all triggers;
prompt Enabling triggers for MED_PATIENT_REGIST_INFO...
alter table MED_PATIENT_REGIST_INFO enable all triggers;
prompt Enabling triggers for MED_PAT_BLOOD_GAS_DETAIL...
alter table MED_PAT_BLOOD_GAS_DETAIL enable all triggers;
prompt Enabling triggers for MED_PAT_MATERECORD...
alter table MED_PAT_MATERECORD enable all triggers;
prompt Enabling triggers for MED_PAT_NEWBORNRECORD...
alter table MED_PAT_NEWBORNRECORD enable all triggers;
prompt Enabling triggers for MED_PAT_OPERATION...
alter table MED_PAT_OPERATION enable all triggers;
prompt Enabling triggers for MED_PAT_ORDERS...
alter table MED_PAT_ORDERS enable all triggers;
prompt Enabling triggers for MED_PAT_RELATION_INFO...
alter table MED_PAT_RELATION_INFO enable all triggers;
prompt Enabling triggers for MED_PAT_SCORE...
alter table MED_PAT_SCORE enable all triggers;
prompt Enabling triggers for MED_PAT_TIMENODES...
alter table MED_PAT_TIMENODES enable all triggers;
prompt Enabling triggers for MED_PHEP_PATIENT_INFO...
alter table MED_PHEP_PATIENT_INFO enable all triggers;





-- Create table
create table MED_PAT_ECG_INFO
(
  patient_no     VARCHAR2(60) not null,
  exam_time      DATE not null,
  report_time    DATE,
  diag_time      DATE,
  diag_desc      VARCHAR2(200),
  diag_source    VARCHAR2(40),
  ecg_path       VARCHAR2(200),
  ecg_url        VARCHAR2(200),
  id             VARCHAR2(60),
  name           VARCHAR2(60),
  sex            VARCHAR2(60),
  age            VARCHAR2(60),
  ecg_patient_id VARCHAR2(60),
  ecg_data       CLOB
)
tablespace TSP_MEDPUB
  pctfree 10
  initrans 1
  maxtrans 255;
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_PAT_ECG_INFO
  add constraint PK_MED_PAT_ECG_INFO primary key (PATIENT_NO, EXAM_TIME)
  using index 
  tablespace TSP_MEDPUB
  pctfree 10
  initrans 2
  maxtrans 255;
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_PAT_ECG_INFO to ROLE_DOCARE;

