rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建用户
rem=====================================================

connect &SystemConn

rem===================================
rem 	create owner
rem===================================
create user medcomm
	identified by medcomm
	default tablespace tsp_MedComm
	temporary tablespace temp
	quota unlimited on tsp_MedComm;
grant connect,resource to medcomm;
grant role_docare to medcomm;
