-- Create table
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
-- Create/Recreate primary, unique and foreign key constraints 
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
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_EXAM_REPORT to ROLE_DOCARE;
