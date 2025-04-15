/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verExisteCuentaBancaria/*</nombre>*/

@cuenta as nvarchar(100)

as

declare @banco nvarchar(20)

select
@banco = T1.BankCode
from
DSC1 T0
inner join ODSC T1 on T0.BankCode = T1.BankCode
where
1 = 1
and ISNULL(T0.GLAccount, '') <> ''
and T0.Account like @cuenta

select ISNULL(@banco, '')