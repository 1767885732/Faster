CREATE OR REPLACE VIEW CPC_REPORT AS
SELECT R.ID,
       R.NAME,
       R.RECEPTION_TIME,
       R.HOSPITAL_ID,
       R.IS_DELETED,
       R.IS_COMPLETED,
       R.IS_FILED,
       A.COMING_TYPE,
       A.ATTACK_TIME,
       A.ARRIVED_HOSPITAL_TIME,
       A.IS_TRANS_HOSPITAL,
       A.FMC_TIME,
       A.ADMISSION_TIME,
       A.ZCYY_TYPE,
       A.ZCYY_VISIT_TIME,
       A.ZCYY_LEAVE_TIME,
       A.RS_SCREEN AS YQ_RS_SCREEN,
       A.IS_RS AS YQ_IS_RS,
       A.IS_RS_DIRECT AS YQ_IS_RS_DIRECT,
       A.RS_START_TIME AS YQ_RS_START_TIME,
       A.RS_END_TIME AS YQ_RS_END_TIME,
       A.IS_RS_RECANALIZATION AS YQ_IS_RS_RECANALIZATION,
       A.RS_RADIOGRAPHY_TIME AS YQ_RS_RADIOGRAPHY_TIME,
       D.IS_ECG,
       D.ECG_TIME,
       D.ECG_DIAGNOSIS_TIME,
       D.ECG_TRANSMISSION AS ECG_TRANS_IN,
       D.ECG_TRANSMISSION_TIME AS ECG_TRANS_IN_TIME,
       D.ECG_TRANSMISSION_TYPE AS ECG_TRANS_IN_TYPE,
       D.IS_CTN AS IS_PRIMARY_CTN,
       D.CTN_SAMPLING_TIME,
       D.CTN_REPORT_TIME,
       D.DIAGNOSIS AS PRIMARY_DIAGNOSIS,
       D.DIAGNOSIS_TIME AS PRIMARY_DIAGNOSIS_TIME,
       D.IS_BYPASS_EMERGENCY,
       D.IS_BYPASS_CCU,
       D.GRACE_HAZARD_LEVEL,
       D.GRACE_REHAZARD_LEVEL,
       D.GRACE_REHAZARD_TIME,
       D.IS_ECT,
       D.CT_NOTICE_TIME,
       D.CT_FINISH_TIME,
       T.IS_ANTIPLATELET AS IS_PRIMARY_ANTIPLATELET,
       T.ASPIRIN_TIME,
       T.CLOPIDOGREL_TIME,
       T.TIGRILLO_TIME,
       T.IS_ANTICOAGULATION AS IS_PRIMARY_ANTICOAGULATION,
       T.ANTICOAGULATION_TIME,
       T.IS_REPERFUSION,
       T.REPERFUSION_MEASURE,
       T.REPERFUSION_MEASURE_RS,
       CASE
         WHEN T.IS_REPERFUSION = '1' AND (T.REPERFUSION_MEASURE = '5' OR
              (T.REPERFUSION_MEASURE = '2' AND
              T.REPERFUSION_MEASURE_RS = '23')) THEN
          '1'
         ELSE
          '0'
       END AS IS_TRANS_PCI,
       T.STRATEGY,
       T.INVASIVE_STRATEGY,
       T.INTERLAYER_TYPE,
       T.ECG_TRANSMISSION AS ECG_TRANS_OUT,
       T.IS_DIRECT_TO_CR,
       T.RS_SCREEN,
       T.IS_RS,
       T.IS_RS_DIRECT,
       T.RS_START_TIME,
       T.RS_END_TIME,
       T.IS_RS_RECANALIZATION,
       P.START_CONDUIT_TIME,
       P.ACTIVATE_CONDUIT_TIME,
       P.START_PUNCTURE_TIME,
       P.START_RADIOGRAPHY_TIME,
       P.GW_PASSAGE_TIME,
       O.DIAGNOSIS,
       O.DIAGNOSIS_TIME,
       O.IS_HEART_FAILURE,
       O.HOD,
       O.TOTAL_COST,
       O.OUTCOME_CODE,
       O.LEAVE_TIME
  FROM CPC_REGIST R
  LEFT JOIN CPC_ADMISSION A
    ON R.ID = A.REGIST_ID
  LEFT JOIN CPC_DIAGNOSIS D
    ON R.ID = D.REGIST_ID
  LEFT JOIN CPC_TREATMENT T
    ON R.ID = T.REGIST_ID
  LEFT JOIN CPC_TREATMENT_PCI P
    ON R.ID = P.REGIST_ID
  LEFT JOIN CPC_DISCHARGE O
    ON R.ID = O.REGIST_ID
WITH READ ONLY;

create or replace view CPC_EXPORT_VIEW 
AS
SELECT 
  R.id,
  R.name,
  R.gender,
  R.age,
  R.birthday,
  R.id_card,
  R.id_card_no,
  R.phone_number,
  R.reception_time,
  D.diagnosis,
  R.wristband_no,
  R.hospital_id,
  R.hospital_name,
  R.report_status,						
  R.is_deleted,              			
  R.is_completed,			  
  R.is_filed,
  R.name as 患者姓名,
  case R.gender
  when '1' then '男'
  when '2' then '女'
  else '' end as 患者性别,			
  R.age as 患者年龄,                  
  R.birthday as 出生日期,	
  case R.marriage
  when '01' then '未婚'
  when '02' then '已婚'
  when '03' then '丧偶'
  when '04' then '离异'
  when '09' then '不详'
  else '' end as 婚姻,		
  case R.job
  when '01' then '国家机关、党群组织、企事业单位负责人'
  when '02' then '各类专业技术人员'
  when '03' then '办事人员和有关人员'
  when '04' then '商业及服务型工作人员'
  when '05' then '农、林、牧、渔、水利业生产人员'
  when '06' then '生产工人，运输工人和相关人员'
  when '07' then '下岗或无业'
  when '08' then '离退休'
  when '09' then '学生'
  when '10' then '军人'
  when '11' then '不便分类的其他从业人员'
  else '' end as 职业,	    		
  case R.culture_degree
  when '01' then '没上过学'
  when '02' then '小学'
  when '03' then '初中'
  when '04' then '高中或中专（中技）'
  when '05' then '大专'
  when '06' then '本科及以上'
  when '09' then '不详'
  else '' end as 文化程度,	      				
  case R.id_card
  when '0' then '无'
  when '1' then '身份证'
  when '2' then '护照'
  else '' end as 证件类别,	
  R.id_card_no as 身份证号,            
  R.phone_number as 电话号码,          
  R.height as 身高,				
  R.weight as 体重,										  
  A.outpatient_no as 门诊号,  
  A.inpatient_no as 住院号,			
  A.insurance as 医保类型,				
  A.insurance_no as 医保卡号,			
  A.attack_time as 发病时间,						
  A.attack_address as 发病地址,			
  case A.distress_case
  when '1' then '持续性胸闷/胸痛'
  when '2' then '间歇性胸闷/胸痛'
  when '3' then '症状已缓解'
  else '' end as 病情评估,	
  case A.coming_type
  when '1' then '呼救（120或其他）出车'
  when '2' then '转院（包含任何医疗机构）'
  when '3' then '"自行来院"'
  when '4' then '"院内发病"'
  else '' end as 来院方式,
  A.help_time as 呼救时间,					
  A.arrived_hospital_time as 到达本院大门时间,		
  case A.is_trans_hospital 
  when '0' then '否'
  when '1' then '是'
  else '' end as 直接转送上级医院,			
  A.fmc_time as 首次医疗接触,				
  A.admission_time as 院内接诊时间,	
  A.admission_doctor as 医护人员,	
  A.zcyy_visit_time	as 转出医院入门时间,		
  A.zcyy_decision_time as 决定转院时间,		
  A.zcyy_leave_time as 转出医院出门时间,			
  A.attack_dept as 发病科室,				
  A.cwys_contact_time as 床位医生接触时间,		
  A.leave_dept_time as 离开科室时间,					
  case to_char(A.consciousness_type)
  when '1' then '"清醒"'
  when '2' then '"对语言有反应"'
  when '3' then '"对刺痛有反应"'
  when '4' then '"对任何刺激无反应"'
  else '' end as 意识,	
  A.respiration_rate as 呼吸,		
  A.pulse_rate as 脉搏,				
  A.heart_rate as 心率,				
  A.blood_pressure as 血压,			
  A.temperature as 体温,			
  case A.rs_screen 
  when '0' then '不合适'
  when '1' then '合适'
  when '2' then '未筛查'
  else '' end as 院前溶栓筛查,	  	
  case A.is_rs 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否院前溶栓,	
  case A.is_rs_direct 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否院前溶栓直达,	  	
  case A.rs_place 
  when '1' then '其他医院'
  when '2' then '救护车'
  else '' end as 院前溶栓场所,	  																	
  A.rs_start_agree_time as 院前溶栓知情同意时间,			
  A.rs_sign_agree_time as 院前溶栓签署知情同意时间,			
  A.rs_start_time as 院前溶栓开始时间,				
  A.rs_end_time as 院前溶栓结束时间,					
  A.rs_radiography_time as 院前溶栓后造影时间,						
  case A.rs_drug_type 
  when '1' then '第一代'
  when '2' then '第二代'
  when '3' then '第三代'
  else '' end as 院前溶栓药物,	
  case A.rs_drug_code 
  when '1' then '全量'
  when '2' then '半量'
  else '' end as 院前溶栓药物剂量,
  case A.is_rs_recanalization 
  when '0' then '否'
  when '1' then '是'
  else '' end as 院前是否溶栓再通,		
  case A.is_remedy_pci 
  when '0' then '否'
  when '1' then '是'
  else '' end as 院前是否补救PCI,					
  A.actual_pci_time as 院前实际PCI时间,			
  case D.is_ecg 
  when '0' then '无'
  when '1' then '有'
  else '' end as 有无心电图,						 
  D.no_ecg_reason as 未获得心电图原因,					 
  D.ecg_time as 首份心电图时间, 						 
  D.ecg_diagnosis_time as 首份心电图诊断时间,			 
  D.ecg_transmission as 是否接收心电图,					 
  D.ecg_transmission_time as 心电图传输时间, 	
  case D.is_ctn 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否肌钙蛋白检验,	  		 		  				 
  D.ctn_sampling_time as 肌钙蛋白抽血时间,				 
  D.ctn_report_time as 肌钙蛋白报告时间,	
  case D.is_scr 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否血清肌酐检验,	  		 
  case D.is_ddimer 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否D二聚体检验,	  
  case D.is_bnp 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否BNP检验,	  
  case D.is_nt_probnp 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否NTproBNP检验,	  
  case D.is_myo 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否Myo检验,	  	 
  case D.is_ckmb 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否CKMB检验,							 
  case D.is_consultation 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否心内科会诊,						 
  case D.consultation_type 
  when '1' then '现场会诊'
  when '2' then '远程会诊'
  else '' end as 会诊方式,				 		 
  D.consultation_notify_time as 通知会诊时间,	     
  D.consultation_time as 会诊时间,				 
  case D.diagnosis 
  when '1' then 'STEMI'
  when '2' then 'NSTEMI'
  when '3' then 'UA'
  when '4' then '主动脉夹层'
  when '5' then '肺动脉栓塞'
  when '6' then '非ACS心源性胸痛'
  when '7' then '其他非心源性胸痛'
  when '9'  then '待查'
  else '' end as 初步诊断,
  D.diagnosis_time as 诊断时间,				 
  D.diagnosis_doctor as 诊断医生,		
  case D.is_giveup_treatment 
  when '0' then '否'
  when '1' then '是'
  else '' end as 患者是否自愿放弃后续治疗,		 
  case D.is_bypass_emergency 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否绕行急诊,	
  case D.is_bypass_ccu 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否绕行CCU,						 			     		 
  D.grace_score	as GRACE分值,	
  case D.grace_rehazard 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否再次危险分层,	  			 			 		 
  D.grace_rehazard_time as 再次危险分层时间,			
  case D.is_ect 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否急诊CT,	  	 					 	  
  D.ct_notice_time as 通知CT室时间,			 
  D.ct_finish_time as CT室完成准备,				 
  D.ct_scan_time as CT扫描开始时间,					 
  D.ct_report_time as CT报告时间,	
  case D.is_cdu 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否彩超检查,			 
  D.notice_cdu_time as 通知彩超室时间,				 
  D.cdu_check_time as 彩超检查时间,				 
  D.check_result_time as 彩超出结果时间,	
  case D.is_mri 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否MRI检查,				 
  D.csc_notice_time as 通知心外科会诊,				 
  D.csc_time as 心外科会诊时间,	
  T.is_antiplatelet	as 是否抗血小板,			  
  T.aspirin_dose as 阿司匹林剂量,					  
  T.aspirin_time as 阿司匹林给药时间,					  
  T.clopidogrel_dose as 氯吡格雷剂量,			  	  
  T.clopidogrel_time as 氯吡格雷给药时间,				  
  T.tigrillo_dose as 替格瑞洛剂量,					  
  T.tigrillo_time as 替格瑞洛给药时间,					  
  T.is_anticoagulation as 是否抗凝给药,				  
  T.anticoagulation_time as 首次抗凝给药时间,				  
  T.anticoagulation_drug as 首次抗凝药物,				  
  T.anticoagulation_dose || T.anticoagulation_unit as 首次抗凝药物剂量,	
  case T.is_statin 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否他汀治疗,		
  case T.is_beta_blocker 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否β受体阻滞剂,			  				    
  case T.is_reperfusion 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否再灌注,			  		  			  
  T.decision_pci_time as 决定介入手术时间,				  
  T.pci_start_agree_time as 开始知情同意时间,			  
  T.pci_sign_agree_time as 签署知情同意时间,			  
  T.pci_actual_time as 实际介入手术时间,				  			  
  T.cabg_decision_time as 决定CABG时间,			  
  T.cabg_start_time as CABG开始时间,					  			  				  
  case T.rs_screen 
  when '0' then '不合适'
  when '1' then '合适'
  when '2' then '未筛查'
  else '' end as 院内溶栓筛查,	  	
  case T.is_rs 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否院内溶栓,	
  case T.is_rs_direct 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否院内溶栓直达,	  	
  case T.rs_place 
  when '1' then '本院急诊科'
  when '2' then '本院心内科'
  when '3' then '其他科室'
  else '' end as 院内溶栓场所,	  																	
  T.rs_start_agree_time as 院内溶栓知情同意时间,			
  T.rs_sign_agree_time as 院内溶栓签署知情同意时间,			
  T.rs_start_time as 院内溶栓开始时间,				
  T.rs_end_time as 院内溶栓结束时间,
  case T.rs_drug_type 
  when '1' then '第一代'
  when '2' then '第二代'
  when '3' then '第三代'
  else '' end as 院内溶栓药物,	
  case T.rs_drug_code 
  when '1' then '全量'
  when '2' then '半量'
  else '' end as 院内溶栓药物剂量,	 
  case T.is_rs_recanalization 
  when '0' then '否'
  when '1' then '是'
  else '' end as 院内是否溶栓再通,	 	  				   
  TP.start_conduit_time	as 启动导管室时间,	
  TP.activate_conduit_time as 导管室激活时间,	
  TP.patient_arrival_time as 患者到达导管室,		
  TP.intervention_person as 介入医师,	
  TP.start_puncture_time as 开始穿刺时间,		
  TP.gw_passage_time as	导丝通过时间,
  TP.start_radiography_time as 造影开始时间,	
  TP.anticoagulation_time as 抗凝给药时间,		
  TP.anticoagulation_drug as 抗凝药物,	
  TP.anticoagulation_dose || TP.anticoagulation_unit as 抗凝药物剂量, 	  	
  TP.end_operation_time as 手术结束时间, 
  case TP.is_delay 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否延误,									
  case TP.approach 
  when '1' then '桡动脉(左侧)'
  when '2' then '桡动脉(右侧)'
  when '3' then '股动脉'
  when '9' then '其他'
  else '' end as 入路, 	
  case TP.intracavity_image 
  when '0' then '未做'
  when '1' then 'IVUS'
  when '2' then 'OCT'
  when '9' then '其他'
  else '' end as 腔内影像,  
  case TP.function_test 
  when '0' then '未做'
  when '1' then 'FFR'
  when '2' then 'iFR'
  when '3' then 'IMR'
  when '9' then '其他'
  else '' end as 功能检测,  		
  TP.function_test_value as 功能检测值,	
  case TP.is_iabp 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否IABP,
  case TP.is_pacemaker 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否使用临时起搏器,			
  case TP.is_ecmo 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否ECMO,	
  case TP.is_auxiliary_device 
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否使用左心室辅助装置,
  O.diagnosis as 出院诊断,					
  O.diagnosis_time as 确诊时间,							
  case O.is_heart_failure
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否院内新发心力衰竭,	
  case O.is_hypertension
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否高血压,	
  case O.is_hyperlipidemia
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否高脂血症,	  			
  case O.is_diabetes
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否糖尿病,		
  case O.is_smoking
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否吸烟,				
  case O.smoking_situation
  when '1' then '已戒烟'
  when '2' then '当前仍吸烟'
  else '' end as 吸烟状况,				
  case O.is_obesity
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否肥胖,			
  case O.is_cvd_hr
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否早发CVD家族史,				
  case O.is_chd
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否冠心病,						
  case O.is_chd_hor
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否血运重建史,					
  case O.is_af
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否心房颤动,				
  case O.af_type
  when '1' then '阵发性'
  when '2' then '持续性'
  when '2' then '长程持续性'
  when '2' then '永久性'
  else '' end as 是否心房颤动分型,							
  case O.is_chf
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否慢性心力衰竭,					
  case O.chf_nyha
  when '1' then 'I'
  when '2' then 'II'
  when '3' then 'III'
  when '4' then 'IV'
  else '' end as NYHA分级,				
  case O.is_vhd
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否心脏瓣膜病,							
  case O.is_cvd
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否脑血管疾病,				
  case O.cvd_type
  when '1' then '缺血性'
  when '2' then '出血性'
  else '' end as 脑血管疾病类型,	
  case O.is_pad
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否外周动脉疾病,
  case O.is_aneurysm
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否主动脉瘤,
  case O.is_copd
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否COPD,  
  case O.is_ckd
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否慢性肾病,      				
  case O.is_anemia
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否贫血,
  case O.is_peptic_ulcer
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否消化性溃疡,
  case O.is_thyroid_dysfunction
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否甲状腺功能异常,  
  case O.is_troponin
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否72h内肌钙蛋白检查,   				
  case O.is_bnp
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否脑钠肽检查,   					
  case O.is_tc
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否总胆固醇检查,   
  case O.is_tg
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否甘油三酯检查,    
  case O.is_hdl_c
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否高密度脂蛋白检查,      
  case O.is_ldl_c
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否低密度脂蛋白检查,         					
  case O.is_ucc
  when '0' then '否'
  when '1' then '是'
  else '' end as 是否超声心动图检查,  								
  O.hod as 住院天数,						
  O.total_cost as 住院总费用,		
  case O.outcome_code
  when '1' then '出院'
  when '2' then '转送其它医院'
  when '3' then '转送其它科室'
  when '4' then '死亡'
  else '' end as 转归方式,  							
  O.leave_time as 出院时间,
  case O.treatment_result
  when '1' then '治愈'
  when '2' then '好转'
  when '3' then '自动离院'
  when '4' then '其它原因离院'
  else '' end as 治疗结果,  	
  case O.is_antiplatelet
  when '0' then '否'
  when '1' then '是'
  else '' end as 出院带抗血小板药物,  			
  case O.is_acei_or_arb
  when '0' then '否'
  when '1' then '是'
  else '' end as 出院带ACEI或ARB,  		
  case O.is_lipid_regulation
  when '0' then '否'
  when '1' then '是'
  else '' end as 出院带调脂药物, 					
  case O.is_beta_blocker
  when '0' then '否'
  when '1' then '是'
  else '' end as 出院带β受体阻滞剂, 									
  O.dept_transfer_time as 转科时间,				
  O.death_time as 死亡时间						 				 		 	
FROM CPC_REGIST R LEFT JOIN CPC_ADMISSION A ON R.ID = A.REGIST_ID
				  LEFT JOIN CPC_DIAGNOSIS D ON R.ID = D.REGIST_ID	
				  LEFT JOIN CPC_TREATMENT T ON R.ID = T.REGIST_ID	
				  LEFT JOIN CPC_TREATMENT_PCI TP ON R.ID = TP.REGIST_ID	
				  LEFT JOIN CPC_DISCHARGE O ON R.ID = O.REGIST_ID	
WITH READ ONLY;


CREATE OR REPLACE VIEW CPC_REGIST_VIEW
AS 
SELECT 
	ID,                  
	PATIENT_NO,
	NAME,
	GENDER,
	AGE,
	BIRTHDAY,
	NATIONALITY,
	MARRIAGE,  
	JOB,  
	CULTURE_DEGREE,  
	ID_CARD,
	ID_CARD_NO,
	PHONE_NUMBER,
	WRISTBAND_NO,
	HEIGHT,  
	WEIGHT,
	DIAGNOSIS,
	RECEPTION_TIME,
	DISCHARGE_TIME,
	INTEGRITY,
	HOSPITAL_ID,
	HOSPITAL_NAME
FROM CPC_REGIST 
WHERE NVL(IS_DELETED, '0') = '0' AND NVL(IS_COMPLETED, '0') = '0'