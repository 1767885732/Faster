rem=====================================================
rem     作    者：研发部
rem     整理时间：2013-03-25
rem     说    明：创建MEDCOMM表空间
rem=====================================================

connect &SystemConn

rem===================================
rem 	create tablespace 9i or 10g
rem		MedComm
rem===================================
create tablespace tsp_MedComm
	logging
	datafile '&datafile\MedComm.dbf'
	size 100M
	reuse
	autoextend on
	next 20M
	maxsize unlimited
	extent management local segment space management auto;
