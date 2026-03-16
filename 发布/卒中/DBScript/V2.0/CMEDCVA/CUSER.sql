rem=====================================================
rem     作    者：研发部
rem     整理时间：2008-07-11
rem     说    明：创建用户
rem=====================================================

rem===================================
rem 	create owner
rem===================================

create user medcva
	identified by medcva
	default tablespace tsp_MedCva
	temporary tablespace temp
	quota unlimited on tsp_MedCva;
grant connect,resource to medcva;
grant role_docare to medcva;
