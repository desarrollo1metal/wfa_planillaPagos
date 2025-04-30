/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_estadoCuenta/*</nombre>*/

as

select 
T0.id
, T0.Fecha
, T0.Descripcion as Descripcion
, T0.Monto
, T0.Operacion
, T0.Cuenta
, T2.ActCurr as Moneda
, T3.BankName as Banco
, T2.AcctCode as Cta_Contable
, T0.Ruc
from 
GMI_TmpOprBnc T0 
left join DSC1 T1 on T0.Cuenta = T1.Account
left join OACT T2 on T1.GLAccount = T2.AcctCode
left join ODSC T3 on T1.BankCode = T3.BankCode