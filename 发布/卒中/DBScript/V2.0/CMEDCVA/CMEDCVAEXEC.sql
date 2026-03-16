rem=====================================================
rem     作    者：于淼
rem     创建时间：2013-03-25
rem     说    明：创建卒中脚本
rem=====================================================

connect &SystemConn

start &sysPath\CMEDCVA\CTSP.sql


rem start &sysPath\CMEDCVA\CTSP8i.sql

start &sysPath\CMEDCVA\CUSER.sql

connect &MedCvaConn

start &sysPath\CMEDCVA\CTABLE.sql

connect &MedCvaConn

start &sysPath\CMEDCVA\CVIEW.sql

connect &MedCvaConn

start &sysPath\CMEDCVA\CDATA.sql
















