-- Create table
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
-- Create/Recreate primary, unique and foreign key constraints 
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
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_LAB_MASTER to ROLE_DOCARE;
