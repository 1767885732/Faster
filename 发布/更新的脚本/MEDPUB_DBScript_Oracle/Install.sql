rem=====================================================
rem     作    者：基础研发部接口组
rem     创建时间：2018-12-24
rem		修改时间：
rem     说    明：系统脚本
rem	
rem		DataFile	  	数据文件存储路径	
rem     SystemConn      管理员用户连接字符
rem
rem     注    意: 
rem		1、DataFile定义为数据文件存储路径
rem		2、@docare为网路服务名
rem		3、SysPath路径为目录路径，建议拷贝到C:盘
rem		   根目录下执行
rem		4、根据选择执行脚本(通过rem注释)
rem=====================================================

define databasename     = 'orcl'
define password     	= 'docare'
define SysPath	  		= 'C:\Users\Administrator\Desktop\MEDPUB_DBScript_Oracle'
define DataFile   		= 'D:\app\Administrator\oradata\orcl'
define SystemConn 		= 'system/&password as sysdba'
define MedPubConn   	= 'MEDPUB/MEDPUB'

connect &SystemConn

spool &SysPath\Install.log

rem==================================
rem        创建公共角色
rem==================================

start &SysPath\CROLE\CROLE.sql


rem==================================
rem        创建接口中间表部分
rem==================================

start &SysPath\MEDPUB\CMEDPUBEXEC.sql

exit;

spool off;