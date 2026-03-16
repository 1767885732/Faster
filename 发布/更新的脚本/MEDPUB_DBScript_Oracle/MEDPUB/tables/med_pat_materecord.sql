-- Create table
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
-- Add comments to the columns 
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
  is '高危因素
';
comment on column MED_PAT_MATERECORD.SPECIAL_TREATMENT
  is '特殊治疗措施';
-- Create/Recreate primary, unique and foreign key constraints 
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
