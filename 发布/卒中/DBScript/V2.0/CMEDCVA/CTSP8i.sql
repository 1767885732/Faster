rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建表空间
rem=====================================================

connect &SystemConn


rem===================================
rem 	create tablespace 8i
rem		MedCva
rem===================================
create tablespace tsp_MedCva
	datafile '&datafile\MedCva.dbf'
	size 300M
	reuse
	autoextend on
	next 20M
	maxsize unlimited;

