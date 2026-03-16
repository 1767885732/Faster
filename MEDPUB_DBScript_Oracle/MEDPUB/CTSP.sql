rem=====================================================
rem     作    者：基础研发部接口组
rem     整理时间：2018-12-25
rem     说    明：创建表空间
rem=====================================================

rem===================================
rem 	create tablespace 9i or 10g
rem		MedPub
rem===================================

create tablespace tsp_MedPub
  logging
  datafile '&DataFile\apMedPub.dbf'
  size 100M
  reuse
  autoextend on
  next 20M
  maxsize unlimited
  extent management local segment space management auto;