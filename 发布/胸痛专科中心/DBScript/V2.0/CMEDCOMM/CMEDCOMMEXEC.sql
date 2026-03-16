rem=====================================================
rem     作    者：于淼
rem     整理时间：2013-03-25
rem     说    明：创建公共部分
rem=====================================================


connect &SystemConn

start &sysPath\CMEDCOMM\CTSP.sql


rem start &sysPath\CMEDCOMM\CTSP8i.sql

start &sysPath\CMEDCOMM\CUSER.sql

connect &MedCommConn

start &sysPath\CMEDCOMM\CTABLE.sql


connect &SystemConn

start &sysPath\CMEDCOMM\CSYNONYMS.sql


rem connect &MedCommConn

rem start &sysPath\CMEDCOMM\CVersionData.sql

