-- Create table
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
-- Create/Recreate primary, unique and foreign key constraints 
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
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_PATIENT_DETIALS_INFO to ROLE_DOCARE;
