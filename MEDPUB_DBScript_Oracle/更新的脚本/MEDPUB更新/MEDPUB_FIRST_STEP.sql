



create tablespace tsp_MedPub
  logging
  datafile '&DataFile\apMedPub.dbf'
  size 100M
  reuse
  autoextend on
  next 20M
  maxsize unlimited
  extent management local segment space management auto;
  
  
  create role role_docare not identified;
grant connect,resource to role_docare;
grant create database link to role_docare;
grant create synonym to role_docare;
grant create view to role_docare;
  
  
  
create user MEDPUB
	identified by MEDPUB
	default tablespace tsp_MedPub
	temporary tablespace temp
	quota unlimited on tsp_MedPub;
grant connect,resource to MEDPUB;
grant role_docare to MEDPUB;