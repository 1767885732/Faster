rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建表空间
rem=====================================================

rem===================================
rem 	create tablespace 9i or 10g
rem		MedCva
rem===================================
create tablespace tsp_MedCva
	logging
	datafile '&datafile\MedCva.dbf'
	size 300M
	reuse
	autoextend on
	next 20M
	maxsize unlimited
	extent management local segment space management auto;


