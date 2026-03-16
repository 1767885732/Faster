rem=====================================================
rem     作    者：基础研发部接口组
rem     创建时间：2018-12-25
rem     说    明：创建用户
rem=====================================================

rem===================================
rem 	create owner
rem===================================

create user MEDPUB
	identified by MEDPUB
	default tablespace tsp_MedPub
	temporary tablespace temp
	quota unlimited on tsp_MedPub;
grant connect,resource to MEDPUB;
grant role_docare to MEDPUB;