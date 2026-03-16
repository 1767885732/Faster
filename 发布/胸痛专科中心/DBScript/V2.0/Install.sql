rem =====================================================
rem	
rem		  DataFile		数据文件存储路径	
rem       SystemConn    管理员用户连接字符
rem		  MedCommConn   公用部分连接字符
rem		  MedCpcConn	麻醉用户连接
rem     注    意: 
rem		1、DataFile定义为数据文件存储路径
rem		2、@docare为网路服务名
rem		3、SysPath路径为目录路径，建议拷贝到C:盘
rem		   根目录下执行
rem		4、根据选择执行脚本(通过rem注释)
rem=====================================================

define databasename = 'docarezkzx'
define databasename2 = 'orcl'
define SysPath	  	= 'D:\公司项目版本管理\胸痛项目\胸痛\Src\胸痛数据库_V2.0\DoCare.CPC.Web\DBScript\V2.0'
define DataFile   	= 'C:\app\15556\oradata\&databasename'
define SystemConn 	= 'system/orcl1234@&databasename2'
define MedCommConn  = 'medcomm/medcomm@&databasename'
define MedCpcConn	= 'medcpc/medcpc@&databasename'

connect &SystemConn
spool &sysPath\install.log

rem==================================
rem        创建公共角色
rem==================================

start &SysPath\CROLE\CROLE.sql

rem==================================
rem        创建公共部分
rem==================================

start &SysPath\CMEDCOMM\CMEDCOMMEXEC.sql

rem==================================
rem        创建胸痛部分
rem==================================

start &SysPath\CMEDCPC\CMEDCPCEXEC.sql




exit;

spool off;