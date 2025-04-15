/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_pagosLetrasCobranza/*</nombre>*/

as

select 
T1.DocEntry as id_TratLetras
, 'Pago de Letras' as 'Tipo de Pago'
, T0.*
from 
gmi_vw_obtenerPagosNroOper T0
inner join "@VS_LTR_LTRBANCODET" T1 on T0.DocEntry = T1.U_VSPAGREC and T0.ObjType = 24
inner join "@VS_LTR_LTRBANCOCAB" T2 on T1.DocEntry = T2.DocEntry
where 
T1.U_VSSITUAC = '03'
and T1.U_VSTIPOPE in ('C', 'R')
and YEAR(T0.Fecha_Contab) = 2016

union all

select 
T1.DocEntry as id_TratLetras
, 'Comision' as 'Tipo de Pago'
, T0.*
from 
gmi_vw_obtenerPagosNroOper T0 
inner join "@VS_LTR_LTRBANCOCAB" T1 on T0.DocEntry = T1.U_VSPECOMI and T0.ObjType = 46
where 
YEAR(T0.Fecha_Contab) = 2016

union all

select 
T1.DocEntry as id_TratLetras
, 'Porte' as 'Tipo de Pago'
, T0.*
from 
gmi_vw_obtenerPagosNroOper T0 
inner join "@VS_LTR_LTRBANCOCAB" T1 on T0.DocEntry = T1.U_VSPEPORT and T0.ObjType = 46
where 
YEAR(T0.Fecha_Contab) = 2016

union all

select 
T1.DocEntry as id_TratLetras
, 'Interes' as 'Tipo de Pago'
, T0.*
from 
gmi_vw_obtenerPagosNroOper T0 
inner join "@VS_LTR_LTRBANCOCAB" T1 on T0.DocEntry = T1.U_VSPRINTE and T0.ObjType = 24
where 
YEAR(T0.Fecha_Contab) = 2016

order by
T1.DocEntry desc