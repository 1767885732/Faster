
alter table CPC_DISCHARGE add COVID19 varchar2(1);

insert into med_dictionary (CODE, NAME, KEY, VALUE, DESCRIPTION, PINYIN, IS_DELETE, CUSTOM_CODE, ORDER_NO)
values ('CPC.5003', 'COVID-19', '1', '经筛查后排除', 'COVID-19', null, null, null, 1);

insert into med_dictionary (CODE, NAME, KEY, VALUE, DESCRIPTION, PINYIN, IS_DELETE, CUSTOM_CODE, ORDER_NO)
values ('CPC.5003', 'COVID-19', '2', '经筛查后确诊', 'COVID-19', null, null, null, 2);

insert into med_dictionary (CODE, NAME, KEY, VALUE, DESCRIPTION, PINYIN, IS_DELETE, CUSTOM_CODE, ORDER_NO)
values ('CPC.5003', 'COVID-19', '3', '未经筛查', 'COVID-19', null, null, null, 3);