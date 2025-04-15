/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/view/*</tipoSQL>*/ 
/*<nombre>*/gmi_vw_obtenerPagosNroOper/*</nombre>*/

as

select 
'Pago Recibido' as Tipo
, T0.DocNum as Nro_SAP
, T0.CardCode as Cod_Cliente
, T0.CardName as Nom_Cliente
, T0.DocDate as Fecha_Contab
, T0.Comments as Comentarios
, T0.DocCurr as Moneda
, T0.DocTotal as Total
, T2.AcctCode as Cuenta
, T2.FormatCode as Formato
, T1.Account as Cta_Banco
, T1.BankCode as Codigo_Banco
, T2.AcctName as Nombre_Cuenta
, T0.U_GMI_RENDICION as Nro_Operacion
, T0.TrsfrAcct as Cta_transfer
, T0.CashAcct as Cta_Cash
, T0.datasource as Origen
, T0.Canceled as Cancelado
, T0.ObjType
, T0.DocEntry
from
ORCT T0 
inner join DSC1 T1 on T0.TrsfrAcct = T1.GLAccount or T0.CashAcct = T1.GLAccount
inner join OACT T2 on T1.GLAccount = T2.AcctCode
where 
T0.Canceled = 'N'

UNION ALL

select 
'Pago Efectuado' as Tipo
, T0.DocNum as Nro_SAP
, T0.CardCode as Cod_Cliente
, T0.CardName as Nom_Cliente
, T0.DocDate as Fecha_Contab
, T0.Comments as Comentarios
, T0.DocCurr as Moneda
, T0.DocTotal as Total
, T2.AcctCode as Cuenta
, T2.FormatCode as Formato
, T1.Account as Cta_Banco
, T1.BankCode as Codigo_Banco
, T2.AcctName as Nombre_Cuenta
, T0.U_GMI_RENDICION as Nro_Operacion
, T0.TrsfrAcct as Cta_transfer
, T0.CashAcct as Cta_Cash
, T0.datasource as Origen
, T0.Canceled as Cancelado
, T0.ObjType
, T0.DocEntry
from
OVPM T0 
inner join DSC1 T1 on T0.TrsfrAcct = T1.GLAccount or T0.CashAcct = T1.GLAccount
inner join OACT T2 on T1.GLAccount = T2.AcctCode
where 
T0.Canceled = 'N'