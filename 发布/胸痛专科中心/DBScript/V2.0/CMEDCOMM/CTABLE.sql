rem=================================================
rem     作    者：研发部
rem	修改时间：2008-07-11
rem     说    明：公共部分对象
rem=================================================

prompt
prompt Creating table MED_BLOOD_GAS_DETAIL
prompt ===================================
prompt
create table MED_BLOOD_GAS_DETAIL
(
  DETAIL_ID VARCHAR2(30) not null,
  BLG_CODE  VARCHAR2(60) not null,
  BLG_VALUE VARCHAR2(20),
  OPERATOR  VARCHAR2(20),
  OP_DATE   DATE,
  ABNORMAL_INDICATOR VARCHAR2(2)
);
alter table MED_BLOOD_GAS_DETAIL
  add constraint BLG_DETAIL_PKEY primary key (DETAIL_ID, BLG_CODE);
grant select, insert, update, delete on MED_BLOOD_GAS_DETAIL to ROLE_DOCARE;

prompt
prompt Creating table MED_BLOOD_GAS_DICT
prompt =================================
prompt
create table MED_BLOOD_GAS_DICT
(
  BLG_CODE        VARCHAR2(20) not null,
  BLG_NAME        VARCHAR2(60) not null,
  BLG_SHOWID      NUMBER not null,
  BLG_UNIT        VARCHAR2(20),
  BLG_REFER_VALUE VARCHAR2(40),
  BLG_STATUS      VARCHAR2(2) not null,
  BLG_INPUT_CODE  VARCHAR2(20),
  BLG_ATTR_CODE   VARCHAR2(100),
  BLG_ITEM_ID     NUMBER(4)
);
alter table MED_BLOOD_GAS_DICT
  add constraint BLD_PRIMARY_KEY primary key (BLG_CODE, BLG_SHOWID, BLG_STATUS);
grant select, insert, update, delete on MED_BLOOD_GAS_DICT to ROLE_DOCARE;

prompt
prompt Creating table MED_BLOOD_GAS_MASTER
prompt ===================================
prompt
create table MED_BLOOD_GAS_MASTER
(
  PATIENT_ID  		VARCHAR2(20) not null,
  VISIT_ID    		NUMBER(2) not null,
  RECORD_DATE 		DATE not null,
  NURSE_MEMO1 		VARCHAR2(600),
  NURSE_MEMO2 		VARCHAR2(600),
  DETAIL_ID   		VARCHAR2(30) not null,
  OPERATOR    		VARCHAR2(20),
  OP_DATE     		DATE,
  SPECIMEN    VARCHAR2(100),
  EQUIPMENT   VARCHAR2(100),
  OPER_ID     NUMBER(2)
)partition by range (RECORD_DATE) 
	( 
		partition part_01 values less than(to_date('2014-01-01','yyyy-mm-dd')), 
		partition part_02 values less than(to_date('2015-01-01','yyyy-mm-dd')), 
		partition part_03 values less than(to_date('2016-01-01','yyyy-mm-dd')), 
		partition part_04 values less than(to_date('2017-01-01','yyyy-mm-dd')),
		partition part_05 values less than(to_date('2018-01-01','yyyy-mm-dd')),
		partition part_06 values less than(to_date('2019-01-01','yyyy-mm-dd')),
		partition part_07 values less than(to_date('2020-01-01','yyyy-mm-dd')),
		partition part_08 values less than(to_date('2021-01-01','yyyy-mm-dd')),
		partition part_09 values less than(to_date('2022-01-01','yyyy-mm-dd')),
		partition part_10 values less than(to_date('2023-01-01','yyyy-mm-dd')),
		partition part_11 values less than(to_date('2024-01-01','yyyy-mm-dd')),
		partition part_12 values less than(to_date('2025-01-01','yyyy-mm-dd')),
		partition part_13 values less than(to_date('2026-01-01','yyyy-mm-dd')),
		partition part_14 values less than(to_date('2027-01-01','yyyy-mm-dd')),
		partition part_15 values less than(to_date('2028-01-01','yyyy-mm-dd')),
		partition part_16 values less than(to_date('2029-01-01','yyyy-mm-dd')),
		partition part_17 values less than(to_date('2030-01-01','yyyy-mm-dd'))
	)
;
alter table MED_BLOOD_GAS_MASTER
  add constraint BLOD_PRIMARY_KEY primary key (DETAIL_ID);
grant select, insert, update, delete on MED_BLOOD_GAS_MASTER to ROLE_DOCARE;


prompt
prompt Creating table MED_APPLICATIONS
prompt ===============================
prompt
create table MED_APPLICATIONS
(
  APP_ID      VARCHAR2(36) not null,
  NAME        VARCHAR2(60) not null,
  DESCRIPTION VARCHAR2(160)
)
;

alter table MED_APPLICATIONS
  add constraint KEY_MED_APPLICATIONS primary key (APP_ID);
alter table MED_APPLICATIONS
  add constraint UNIQUE_MED_APPLICATIONS unique (NAME);
grant select, insert, update, delete on MED_APPLICATIONS to ROLE_DOCARE;


prompt
prompt Creating table MED_BED_REC
prompt ==========================
prompt
create table MED_BED_REC
(
  WARD_CODE         VARCHAR2(16) not null,
  BED_NO            VARCHAR2(20) not null,
  BED_LABEL         VARCHAR2(20),
  ROOM_NO           VARCHAR2(20),
  DEPT_CODE         VARCHAR2(16),
  BED_APPROVED_TYPE VARCHAR2(1),
  BED_SEX_TYPE      VARCHAR2(1),
  BED_CLASS         VARCHAR2(2),
  BED_STATUS        VARCHAR2(1),
  ICU_INDICATOR     NUMBER(1),
  MONITOR_LABEL     VARCHAR2(20),
  SERIAL_NO         NUMBER(3)
)
;

alter table MED_BED_REC
  add constraint PK_MED_BED_REC primary key (WARD_CODE, BED_NO);
create index IND_1_BED_REC on MED_BED_REC (DEPT_CODE);
grant select, insert, update, delete on MED_BED_REC to ROLE_DOCARE;

prompt
prompt Creating table MED_DEPT_DICT
prompt ============================
prompt
create table MED_DEPT_DICT
(
  SERIAL_NO  NUMBER(3),
  DEPT_CODE  VARCHAR2(16) not null,
  DEPT_NAME  VARCHAR2(40),
  INPUT_CODE VARCHAR2(8)
)
;

alter table MED_DEPT_DICT
  add constraint PK_MED_DEPT_DICT primary key (DEPT_CODE);
grant select, insert, update, delete on MED_DEPT_DICT to ROLE_DOCARE;


prompt
prompt Creating table MED_HIS_USERS
prompt ============================
prompt
create table MED_HIS_USERS
(
  USER_ID     VARCHAR2(36) not null,
  USER_NAME   VARCHAR2(30) not null,
  USER_DEPT   VARCHAR2(16),
  INPUT_CODE  VARCHAR2(8),
  USER_JOB    VARCHAR2(20),
  RESERVED01  VARCHAR2(50),
  CREATE_DATE DATE
)
;

alter table MED_HIS_USERS
  add constraint PK_MED_HIS_USERS primary key (USER_ID);
grant select, insert, update, delete on MED_HIS_USERS to ROLE_DOCARE;

prompt
prompt Creating table MED_PERMISSIONS
prompt ==============================
prompt
create table MED_PERMISSIONS
(
  PERMISSION_ID  VARCHAR2(36) not null,
  APP_ID         VARCHAR2(36) not null,
  NAME           VARCHAR2(100) not null,
  PERMISSION_KEY VARCHAR2(160) not null,
  SORT_ID        number(10),
  IS_VALID       VARCHAR2(1) default 'T' not null,
  DESCRIPTION    VARCHAR2(160)
)
;
alter table MED_PERMISSIONS
  add constraint KEY_MED_PERMISSIONS primary key (PERMISSION_ID);
grant select, insert, update, delete on MED_PERMISSIONS to ROLE_DOCARE;


prompt
prompt Creating table MED_RELATIONSHIP_DICT
prompt ==============================
prompt
create table MED_RELATIONSHIP_DICT
(
  SERIAL_NO         NUMBER(2),
  RELATIONSHIP_CODE VARCHAR2(2) not null,
  RELATIONSHIP_NAME VARCHAR2(10),
  INPUT_CODE        VARCHAR2(8)
);
alter table MED_RELATIONSHIP_DICT
  add constraint PK_MED_RELATIONSHIP_DICT primary key (RELATIONSHIP_CODE);

grant select, insert, update, delete on MED_RELATIONSHIP_DICT to ROLE_DOCARE;


prompt
prompt Creating table MED_PRICE_ITEM_NAME_DICT
prompt =======================================
prompt
create table MED_PRICE_ITEM_NAME_DICT
(
  ITEM_CLASS    VARCHAR2(16) not null,
  ITEM_NAME     VARCHAR2(60) not null,
  ITEM_CODE     VARCHAR2(10),
  STD_INDICATOR NUMBER(1),
  INPUT_CODE    VARCHAR2(8),
  CUSTOM_CODE   VARCHAR2(8),
  INPUT_CODE_WB VARCHAR2(8),
  STOP_FLAG     VARCHAR2(2)
)
;
alter table MED_PRICE_ITEM_NAME_DICT
  add constraint PK_MED_PRICE_ITEM_NAME_DICT primary key (ITEM_CLASS, ITEM_NAME);
grant select, insert, update, delete on MED_PRICE_ITEM_NAME_DICT to ROLE_DOCARE;


prompt
prompt Creating table MED_ROLES
prompt ========================
prompt
create table MED_ROLES
(
  ROLE_ID     VARCHAR2(36) not null,
  APP_ID      VARCHAR2(36) not null,
  NAME        VARCHAR2(60) not null,
  DESCRIPTION VARCHAR2(160),
  CREATE_DATE DATE not null
)
;
alter table MED_ROLES
  add constraint KEY_MED_ROLES primary key (ROLE_ID);
grant select, insert, update, delete on MED_ROLES to ROLE_DOCARE;

prompt
prompt Creating table MED_ROLES_PERMISSIONS
prompt ====================================
prompt
create table MED_ROLES_PERMISSIONS
(
  ROLE_ID       VARCHAR2(36) not null,
  PERMISSION_ID VARCHAR2(36) not null
)
;
alter table MED_ROLES_PERMISSIONS
  add constraint KEY_MED_ROLES_PERMISSIONS primary key (ROLE_ID, PERMISSION_ID);
grant select, insert, update, delete on MED_ROLES_PERMISSIONS to ROLE_DOCARE;


prompt
prompt Creating table MED_USERS
prompt ========================
prompt
create table MED_USERS
(
  USER_ID     VARCHAR2(36) not null,
  LOGIN_NAME  VARCHAR2(36) not null,
  LOGIN_PWD   VARCHAR2(36) not null,
  USER_NAME   VARCHAR2(36),
  DEPT_ID     VARCHAR2(16),
  CREATE_DATE DATE,
  IS_VALID    VARCHAR2(1) default 'T' not null,
  MEMO        VARCHAR2(100),
  HOSPITAL_ID VARCHAR2(50)
)
;

alter table MED_USERS
  add constraint KEY_MED_USERS primary key (USER_ID);
alter table MED_USERS
  add constraint UNIQUE_MED_USERS unique (LOGIN_NAME);
grant select, insert, update, delete on MED_USERS to ROLE_DOCARE;

prompt
prompt Creating table MED_HOSPITALS_DICT
prompt ========================================
prompt
create table MED_HOSPITALS_DICT
(
  code				VARCHAR2(50) not null,
  name				VARCHAR2(70),
  gbcode            VARCHAR2(12),
  pinyin_code		VARCHAR2(20),
  is_enabled		CHAR(1) default '1',
  constraint MED_MED_HOSPITALS_DICT_PK primary key (code)
)
;

grant select, insert, update, delete on MED_HOSPITALS_DICT to ROLE_DOCARE;

prompt
prompt Creating table MED_USERS_DEPTS
prompt ==============================
prompt
create table MED_USERS_DEPTS
(
  USER_ID VARCHAR2(36) not null,
  DEPT_ID VARCHAR2(16) not null
)
;

alter table MED_USERS_DEPTS
  add constraint KEY_MED_USERS_DEPTS primary key (USER_ID, DEPT_ID);
grant select, insert, update, delete on MED_USERS_DEPTS to ROLE_DOCARE;

prompt
prompt Creating table MED_USERS_ROLES
prompt ==============================
prompt
create table MED_USERS_ROLES
(
  USER_ID VARCHAR2(36) not null,
  ROLE_ID VARCHAR2(36) not null
)
;

alter table MED_USERS_ROLES
  add constraint KEY_MED_USERS_ROLES primary key (USER_ID, ROLE_ID);
grant select, insert, update, delete on MED_USERS_ROLES to ROLE_DOCARE;


-- Create table
create table MED_AUTO_UPDATER
(
  ID        VARCHAR2(36) NOT NULL,
  VERSION_NO VARCHAR2(30) NOT NULL,
  APP_ID     VARCHAR2(36) NOT NULL,
  UPLOAD_TIME DATE not null
);
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_AUTO_UPDATER
  add constraint PK_MED_AUTO_UPDATER primary key (ID);
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_AUTO_UPDATER to ROLE_DOCARE;


-- Create table
create table MED_AUTO_UPDATER_ITEMS
(
  ID        VARCHAR2(36) NOT NULL,
  VERSION_NO VARCHAR2(30) NOT NULL,
  CONTENT    BLOB NOT NULL,
  APP_ID     VARCHAR2(36) NOT NULL,
  UPLOAD_TIME DATE NOT NULL
);
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_AUTO_UPDATER_ITEMS
  add constraint PK_MED_AUTO_UPDATER_ITEMS primary key (ID);
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_AUTO_UPDATER_ITEMS to ROLE_DOCARE;

-- Create table
create table MED_PERMISSIONS_PROVIDER
(
  PERMISSION_ID        VARCHAR2(36) not null,
  PARENT_PERMISSION_ID VARCHAR2(36) not null,
  APP_ID               VARCHAR2(36) not null
);
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_PERMISSIONS_PROVIDER
  add constraint KEY_MED_PERMISSIONS_PROVIDER primary key (PERMISSION_ID);
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_PERMISSIONS_PROVIDER to ROLE_DOCARE;


create table MED_WRISTBIND_BINDING
(
  ID			VARCHAR2(36) not null,
  WRISTBIND_NO	VARCHAR2(36) not null,
  BINDING_TIME	DATE,
  APP_ID        VARCHAR2(36) not null
);

alter table MED_WRISTBIND_BINDING
  add constraint KEY_MED_WRISTBIND_BINDING primary key (ID);
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_WRISTBIND_BINDING to ROLE_DOCARE;


create table MED_MESSAGES
(
  id          VARCHAR2(36) not null,
  subject     VARCHAR2(256),
  type        VARCHAR2(36),
  priority    VARCHAR2(1),
  sender      VARCHAR2(36),
  receiver    VARCHAR2(36),
  send_time   DATE,
  content     VARCHAR2(1024),
  is_delete   CHAR(1) default '0',
  delete_time DATE,
  read_status VARCHAR2(1) default '0',
  read_time   DATE,
  link_url    VARCHAR2(512),
  is_prompt   VARCHAR2(1) default '0',
  prompt_time DATE
);

alter table MED_MESSAGES
  add constraint KEY_MED_MESSAGES primary key (ID);
-- Grant/Revoke object privileges 
grant select, insert, update, delete on MED_MESSAGES to ROLE_DOCARE;