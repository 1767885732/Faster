-- Create table
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
-- Create/Recreate primary, unique and foreign key constraints 
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
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_PAT_OPERATION to ROLE_DOCARE;
