-- Create table
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
-- Create/Recreate primary, unique and foreign key constraints 
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
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_PHEP_PATIENT_INFO to ROLE_DOCARE;
