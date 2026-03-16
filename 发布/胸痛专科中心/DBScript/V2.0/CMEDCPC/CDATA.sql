set feedback off
set define off
prompt Loading MED_ANESTHESIA_COMM_DICT...
/* MED_APPLICATIONS */
insert into MEDCOMM.MED_APPLICATIONS (APP_ID, NAME, DESCRIPTION)
values ('DoCareCPC', '胸痛中心', '胸痛数据管理云平台');
commit;

set feedback on
set define on
prompt Done.

