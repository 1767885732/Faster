rem=====================================================
rem     作    者：研发部
rem     整理时间：2013-03-25
rem     说    明：创建表空间
rem=====================================================

connect &SystemConn

rem===================================
rem 	create tablespace 8i
rem		MedComm
rem===================================
create tablespace tsp_MedComm
	datafile '&datafile\MedComm.dbf'
	size 100M
	reuse
	autoextend on
	next 20M
	maxsize unlimited;

