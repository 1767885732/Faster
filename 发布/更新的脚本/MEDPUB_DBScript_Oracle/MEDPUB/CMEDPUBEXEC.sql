rem=====================================================
rem     作    者：基础研发部接口组
rem     创建时间：2018-12-25
rem     说    明：创建接口中间表脚本
rem=====================================================

connect &SystemConn

start &sysPath\MEDPUB\CTSP.sql


rem start &sysPath\MEDPUB\CTSP8i.sql


start &sysPath\MEDPUB\CUSER.sql


connect &MedPubConn

start &sysPath\MEDPUB\CTABLE.sql


rem connect &SystemConn
rem start &sysPath\MEDPUB\CSYNONYMS.sql


rem connect &MedPubConn
rem start &sysPath\MEDPUB\CDATA.sql









