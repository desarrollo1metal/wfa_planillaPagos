/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtCtasBanco/*</nombre>*/

as

select
'' as Account
, 'Todas las cuentas' as BankName

union all

select
T0.Account
, T0.Account + ' - ' + T2.ActCurr + ' - ' + T1.BankName as BankName
from
DSC1 T0 
left join ODSC T1 on T0.BankCode = T1.BankCode
left join OACT T2 on T0.GLAccount = T2.AcctCode