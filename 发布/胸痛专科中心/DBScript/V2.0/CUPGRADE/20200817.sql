
alter table CPC_ADMISSION add IS_DIRECT_CONDUIT_ROOM varchar2(1);
alter table CPC_DISCHARGE add IS_ARNI varchar2(1);
alter table CPC_DISCHARGE add ARNI_DOSE varchar2(10);
alter table CPC_DISCHARGE add ARNI_FREQUENCY varchar2(1);
alter table CPC_DISCHARGE add ARNI_FREQUENCY_RANGE varchar2(1);
alter table CPC_DISCHARGE add IS_JTYW varchar2(1);
alter table CPC_DISCHARGE add JTYWNAME varchar2(2);
alter table CPC_DISCHARGE add IS_KNYW varchar2(1);
alter table CPC_DISCHARGE add KNYWNAME varchar2(2);
alter table CPC_DISCHARGE add IS_PCSK9 varchar2(1);
alter table CPC_DISCHARGE add PCSK9_NAME varchar2(2);
alter table CPC_DISCHARGE add PCSK9_DOSE varchar2(10);
alter table CPC_TREATMENT_PCI add PREOPERATIVE_TIMI_LEVEL varchar2(1);