rem=====================================================
rem     作    者：于淼
rem     创建时间：2013-03-25
rem     说    明：基础设计(角色)
rem=====================================================

rem===================================
rem 	更新脚本
rem===================================

alter table CPC_ADMISSION add chief_complaint VARCHAR2(200);
alter table CPC_DIAGNOSIS_TEST add source VARCHAR2(1) default '1';
update med_ecg set category=replace(category, '#_', '_IF_');
update med_ecg set category=replace(category, '3#', '3_IF_') where category = '3#';