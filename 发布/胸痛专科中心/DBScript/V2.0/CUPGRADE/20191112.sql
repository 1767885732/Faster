rem=====================================================
rem     作    者：于淼
rem     创建时间：2013-03-25
rem     说    明：基础设计(角色)
rem=====================================================

rem===================================
rem 	更新脚本
rem===================================
alter table CPC_DISCHARGE add education	VARCHAR2(60);
alter table CPC_DISCHARGE add is_malignant_tumor VARCHAR2(1);
alter table CPC_ADMISSION add is_stricken_insurance VARCHAR2(1);