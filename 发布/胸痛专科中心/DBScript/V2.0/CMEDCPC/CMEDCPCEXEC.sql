rem=====================================================
rem     作    者：于淼
rem     创建时间：2013-03-25
rem     说    明：创建胸痛脚本
rem=====================================================

connect &SystemConn

start &sysPath\CMEDCPC\CTSP.sql

start &sysPath\CMEDCPC\CUSER.sql

connect &MedCpcConn

start &sysPath\CMEDCPC\CTABLE.sql

connect &MedCpcConn

start &sysPath\CMEDCPC\CVIEW.sql

connect &MedCpcConn

start &sysPath\CMEDCPC\CDATA.sql
















