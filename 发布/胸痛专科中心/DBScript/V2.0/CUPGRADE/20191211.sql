rem=====================================================
rem     作    者：于淼
rem     创建时间：2013-03-25
rem     说    明：基础设计(角色)
rem=====================================================

rem===================================
rem 	更新脚本
rem===================================

alter table CPC_ADMISSION add outpatient_sn VARCHAR2(36);
alter table CPC_ADMISSION add inpatient_sn VARCHAR2(36);
