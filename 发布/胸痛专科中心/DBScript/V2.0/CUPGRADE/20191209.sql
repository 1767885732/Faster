rem=====================================================
rem     作    者：于淼
rem     创建时间：2013-03-25
rem     说    明：基础设计(角色)
rem=====================================================

rem===================================
rem 	更新脚本
rem===================================

alter table CPC_ADMISSION add is_rs_taboo VARCHAR2(1);
alter table CPC_TREATMENT add is_rs_taboo VARCHAR2(1);
alter table CPC_TREATMENT modify no_reperfusion_measure VARCHAR2(30);
alter table CPC_DISCHARGE_DRUGS add rate_time VARCHAR2(1);
alter table CPC_DISCHARGE_DRUGS add rate_count VARCHAR2(1);
alter table CPC_DISCHARGE_DRUGS modify code VARCHAR2(32) null;
