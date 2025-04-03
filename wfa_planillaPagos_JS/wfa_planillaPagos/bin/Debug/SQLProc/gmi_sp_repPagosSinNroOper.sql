/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_repPagosSinNroOper/*</nombre>*/

@fDesde as datetime
, @fHasta as datetime

as

select 
T0.DocNum as Nro_SAP
, CONVERT(VARCHAR(10),T0.DocDate,103) as Fecha_Cont
, CONVERT(VARCHAR(10),T0.TaxDate,103) as Fecha_Doc
, CONVERT(VARCHAR(10),T0.DocDueDate,103) as Fecha_Ven
, T0.CardCode as Cod_Cliente
, T0.CardName as Nom_Cliente
, T0.DocCurr as Moneda
, T0.DocTotal as Total
, T0.U_GMI_RENDICION as Nro_Operacion
, T1.BankCode as Cod_Banco
, T2.id as Id_EC
from 
ORCT T0
inner join DSC1 T1 on isnull(T0.CashAcct, 'X') = isnull(T1.GLAccount, 'Y') or isnull(T0.TrsfrAcct, 'X') = isnull(T1.GLAccount, 'Y')
left join GMI_TmpOprBnc T2 on T1.Account = T2.Cuenta and dbo.udf_SupCerosYComilSim(T0.U_GMI_RENDICION) = dbo.udf_SupCerosYComilSim(T2.Operacion) 
where 
1 = 1
and isnull(T0.U_GMI_RENDICION, '') <> ''
and isnull(T2.id, '') = ''
and T0.DocDate between @fDesde and @fHasta
and ISNULL(T2.esPll, 'N') = 'Y'
order by
T0.DocDate desc