rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建用户
rem=====================================================

rem===================================
rem 	create owner
rem===================================

create user medcpc
	identified by medcpc
	default tablespace tsp_MedCpc
	temporary tablespace temp
	quota unlimited on tsp_MedCpc;
grant connect,resource to medcpc;
grant role_docare to medcpc;
