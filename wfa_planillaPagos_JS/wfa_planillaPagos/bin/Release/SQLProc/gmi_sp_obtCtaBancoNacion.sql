/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtCtaBancoNacion/*</nombre>*/

as

select 
T0.Account
from 
DSC1 T0 
inner join ODSC T1 on T0.BankCode = T1.BankCode
where
T1.BankName like '%nacion%'