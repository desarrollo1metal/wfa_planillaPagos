/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/view/*</tipoSQL>*/ 
/*<nombre>*/gmi_vw_ventas/*</nombre>*/

as

select 
T0.DocEntry
, T0.DocNum
, T0.CardCode as Codigo
, T0.CardName as Nombre
, 'Oferta de Venta' as TipoDoc
, T0.DocDate as Fec_Contab
, T0.DocDueDate as Fec_Venc
, T0.NumAtCard as Referencia
, T1.SlpName as Vendedor
, case T0.DocStatus when 'O' then 'Abierto' when 'C' then 'Cerrado' else T0.DocStatus end as EstadoDoc
, T0.DocCur as Moneda
, T0.DocTotal as Total
, T0.DocTotalFC as TotalFC
, T0.PaidSum as MontoPagado
, T0.PaidSumFC as MontoPagadoME
, case T0.CANCELED when 'Y' then 'Si' else 'No' end as Cancelado
, T0.ObjType
, T0.SlpCode
, T0.DocRate
from
OQUT T0 
inner join OSLP T1 on T0.SlpCode = T1.SlpCode

union all

select 
T0.DocEntry
, T0.DocNum
, T0.CardCode as Codigo
, T0.CardName as Nombre
, 'Orden de Venta' as TipoDoc
, T0.DocDate as Fec_Contab
, T0.DocDueDate as Fec_Venc
, T0.NumAtCard as Referencia
, T1.SlpName as Vendedor
, case T0.DocStatus when 'O' then 'Abierto' when 'C' then 'Cerrado' else T0.DocStatus end as EstadoDoc
, T0.DocCur as Moneda
, T0.DocTotal as Total
, T0.DocTotalFC as TotalFC
, T0.PaidSum as MontoPagado
, T0.PaidSumFC as MontoPagadoME
, case T0.CANCELED when 'Y' then 'Si' else 'No' end as Cancelado
, T0.ObjType
, T0.SlpCode
, T0.DocRate
from
ORDR T0 
inner join OSLP T1 on T0.SlpCode = T1.SlpCode

union all

select 
T0.DocEntry
, T0.DocNum
, T0.CardCode as Codigo
, T0.CardName as Nombre
, 'Entrega' as TipoDoc
, T0.DocDate as Fec_Contab
, T0.DocDueDate as Fec_Venc
, T0.NumAtCard as Referencia
, T1.SlpName as Vendedor
, case T0.DocStatus when 'O' then 'Abierto' when 'C' then 'Cerrado' else T0.DocStatus end as EstadoDoc
, T0.DocCur as Moneda
, T0.DocTotal as Total
, T0.DocTotalFC as TotalFC
, T0.PaidSum as MontoPagado
, T0.PaidSumFC as MontoPagadoME
, case T0.CANCELED when 'Y' then 'Si' else 'No' end as Cancelado
, T0.ObjType
, T0.SlpCode
, T0.DocRate
from
ODLN T0 
inner join OSLP T1 on T0.SlpCode = T1.SlpCode

union all

select 
T0.DocEntry
, T0.DocNum
, T0.CardCode as Codigo
, T0.CardName as Nombre
, 'Devolucion de Venta' as TipoDoc
, T0.DocDate as Fec_Contab
, T0.DocDueDate as Fec_Venc
, T0.NumAtCard as Referencia
, T1.SlpName as Vendedor
, case T0.DocStatus when 'O' then 'Abierto' when 'C' then 'Cerrado' else T0.DocStatus end as EstadoDoc
, T0.DocCur as Moneda
, T0.DocTotal as Total
, T0.DocTotalFC as TotalFC
, T0.PaidSum as MontoPagado
, T0.PaidSumFC as MontoPagadoME
, case T0.CANCELED when 'Y' then 'Si' else 'No' end as Cancelado
, T0.ObjType
, T0.SlpCode
, T0.DocRate
from
ORDN T0 
inner join OSLP T1 on T0.SlpCode = T1.SlpCode

union all

select 
T0.DocEntry
, T0.DocNum
, T0.CardCode as Codigo
, T0.CardName as Nombre
, 'Anticipo de Cliente' as TipoDoc
, T0.DocDate as Fec_Contab
, T0.DocDueDate as Fec_Venc
, T0.NumAtCard as Referencia
, T1.SlpName as Vendedor
, case T0.DocStatus when 'O' then 'Abierto' when 'C' then 'Cerrado' else T0.DocStatus end as EstadoDoc
, T0.DocCur as Moneda
, T0.DocTotal as Total
, T0.DocTotalFC as TotalFC
, T0.PaidSum as MontoPagado
, T0.PaidSumFC as MontoPagadoME
, case T0.CANCELED when 'Y' then 'Si' else 'No' end as Cancelado
, T0.ObjType
, T0.SlpCode
, T0.DocRate
from
ODPI T0 
inner join OSLP T1 on T0.SlpCode = T1.SlpCode

union all

select 
T0.DocEntry
, T0.DocNum
, T0.CardCode as Codigo
, T0.CardName as Nombre
, 'Factura de Cliente' as TipoDoc
, T0.DocDate as Fec_Contab
, T0.DocDueDate as Fec_Venc
, T0.NumAtCard as Referencia
, T1.SlpName as Vendedor
, case T0.DocStatus when 'O' then 'Abierto' when 'C' then 'Cerrado' else T0.DocStatus end as EstadoDoc
, T0.DocCur as Moneda
, T0.DocTotal as Total
, T0.DocTotalFC as TotalFC
, T0.PaidSum as MontoPagado
, T0.PaidSumFC as MontoPagadoME
, case T0.CANCELED when 'Y' then 'Si' else 'No' end as Cancelado
, T0.ObjType
, T0.SlpCode
, T0.DocRate
from
OINV T0 
inner join OSLP T1 on T0.SlpCode = T1.SlpCode