-- Create table
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
